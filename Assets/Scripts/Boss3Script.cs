using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss3Script : MonoBehaviour
{
    public GameObject BulletPrefab; // Prefab del proyectil para disparar
    public GameObject Paco; // Referencia al objeto del jugador (Paco)
    public int Health = 15; // Vida del jefe
    public float detectionRange = 5f; // Rango para detectar a Paco
    public float runSpeed = 2.0f; // Velocidad al correr
    public float jumpForce = 5f; // Fuerza del salto
    public GameObject gameFinishCanvas;

    private Vector3[] trashPositions = new Vector3[]
    {
        new Vector3(38.105f, 0.275f, 0), // Primera posición
        new Vector3(42.975f, 0.18f, 0),  // Segunda posición
        new Vector3(47.15f, 0.18f, 0),   // Última posición
    };

    private float LastShoot;
    private bool isRunning = false;
    private bool isIdle = true;
    private bool reachedSecondPosition = false; // Nuevo estado para controlar la segunda posición
    private Animator animator;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private int nextTrashIndex = 0;
    private float jumpTimer = 5f;
    private PacoMovement pacoMovement; // Referencia al script de movimiento de Paco

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        if (Paco != null)
    {
        pacoMovement = Paco.GetComponent<PacoMovement>();
    }
    else
    {
        Debug.LogError("Paco no está asignado en el Boss3Script.");
    }

        // Inicializa los parámetros del Animator
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
    }

    private void Update()
    {
        if (Paco == null) return;

        // Salto cada 5 segundos si está en estado idle (disparando)
        if (isIdle)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                Jump();
                jumpTimer = 5f;
            }
        }

        // Comportamiento basado en estado
        if (isRunning)
        {
            MoveToTrash(); // Movimiento hacia la posición objetivo
        }
        else if (isIdle)
        {
            HandleAttacking(); // Disparar y atacar al jugador
        }
    }
private void MoveToTrash()
{
    if (nextTrashIndex < trashPositions.Length)
    {
        Vector3 targetPosition = trashPositions[nextTrashIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Activar animación de correr solo mientras se mueve
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false); // No disparar mientras corre

        // Orientar hacia la dirección del objetivo
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        rb.velocity = new Vector2(direction.x * runSpeed, rb.velocity.y);

        // Verificar si llegó a la posición objetivo
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            rb.velocity = Vector2.zero; // Detener el movimiento
            animator.SetBool("isRunning", false); // Detener animación de correr

            if (Mathf.Approximately(targetPosition.x, 38.105f) && Mathf.Approximately(targetPosition.y, 0.275f))
            {
                Jump(); // Realizar salto en la primera posición
            }
            else if (Mathf.Approximately(targetPosition.x, 42.975f) && Mathf.Approximately(targetPosition.y, 0.18f))
            {
                // Segunda posición alcanzada, esperar hasta perder 5 vidas más
                isRunning = false;
                isIdle = true;
                reachedSecondPosition = true;
                return; // Detener movimiento hasta que se cumpla la condición de vidas
            }
            else if (Mathf.Approximately(targetPosition.x, 47.15f) && Mathf.Approximately(targetPosition.y, 0.18f))
            {
                // Última posición alcanzada
                isRunning = false;
                isIdle = true;
                animator.SetBool("isAttacking", true); // Activar animación de disparo
                return;
            }

            nextTrashIndex++;
        }
    }
}

// Método Hit actualizado para manejar la transición desde la segunda posición
public void Hit()
{
    Health--;
    Debug.Log("Boss recibió daño. Vida restante: " + Health);

    if (Health <= 0)
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        if (pacoMovement != null)
            {
                pacoMovement.AddScore(500);
                pacoMovement.Heal(1); // Paco recupera 1 de salud cuando mata al Boss
            }
        Debug.Log("Boss destruido.");
        ScoreManager.SaveScore(currentLevelName, pacoMovement.GetScore());//Se guarda el puntaje
        gameFinishCanvas.SetActive(true);
        Destroy(gameObject);
    }
    else if (Health == 10) // Comienza a correr hacia la segunda posición
    {
        Debug.Log("El jefe comienza a correr porque tiene 10 de vida.");
        isIdle = false; // Deja de estar en modo idle
        isRunning = true; // Activa el modo de correr
        animator.SetBool("isRunning", true); // Activa la animación de correr
    }
    else if (Health == 5 && reachedSecondPosition) // Salta y se dirige a la última posición
    {
        Debug.Log("El jefe comienza a moverse hacia la última posición después de perder 5 vidas más.");
        isIdle = false; // Cambiar de modo idle a movimiento
        isRunning = true;
        Jump(); // Realiza un salto desde la segunda posición
        nextTrashIndex = 2; // Apunta a la última posición
    }
}

    private void HandleAttacking()
    {
        Vector3 direction = Paco.transform.position - transform.position;

        // Ajustar orientación hacia Paco
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        float distance = Mathf.Abs(Paco.transform.position.x - transform.position.x);

        if (distance <= detectionRange)
        {
            animator.SetBool("isAttacking", true);

            if (distance < 0.9f && Time.time > LastShoot + 0.5f)
            {
                Shoot();
                LastShoot = Time.time;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

 
    private void Shoot()
    {
        Vector3 direction = transform.localScale.x == originalScale.x ? Vector2.right : Vector2.left;
        Vector3 spawnPosition = transform.position + direction * 0.2f;

        GameObject bullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
