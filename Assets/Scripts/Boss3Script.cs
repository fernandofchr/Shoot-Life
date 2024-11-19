using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Script : MonoBehaviour
{
    // Variables públicas configurables en Unity
    public GameObject BulletPrefab; // Prefab del proyectil para disparar
    public GameObject Paco; // Referencia al objeto del jugador (Paco)
    public int Health = 5; // Vida del jefe
    public float detectionRange = 5f; // Rango para detectar a Paco
    public float runSpeed = 2.0f; // Velocidad al correr

    // Variables privadas internas
    private float LastShoot; // Control del tiempo entre disparos
    private bool isRunning = false; // Controla si el jefe está en modo "correr"
    private Animator animator; // Controlador de animaciones del jefe
    private Vector3 originalScale; // Guarda la escala inicial del jefe
    private Rigidbody2D rb; // Componente de física 2D para mover al jefe

    private void Start()
    {
        // Inicializamos el Animator y Rigidbody2D
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Guardamos la escala inicial del jefe
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Si no hay jugador (Paco), no hace nada
        if (Paco == null) return;

        // Calculamos la dirección hacia el jugador
        Vector3 direction = Paco.transform.position - transform.position;

        // Ajustamos la orientación del jefe hacia el jugador
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Calculamos la distancia entre el jefe y el jugador
        float distance = Mathf.Abs(Paco.transform.position.x - transform.position.x);

        if (!isRunning)
        {
            // Si el jefe NO está corriendo, sigue las mecánicas de ataque normales
            HandleAttacking(distance);
        }
        else
        {
            // Si el jefe está corriendo, se mueve hacia el jugador
            MoveTowardsPaco(direction);
        }
    }

    private void HandleAttacking(float distance)
    {
        // Si Paco está dentro del rango de detección, activa la animación de ataque
        if (distance <= detectionRange)
        {
            animator.SetBool("isAttacking", true);

            // Si está muy cerca y ha pasado el tiempo de disparo, dispara un proyectil
            if (distance < 0.9f && Time.time > LastShoot + 0.5f)
            {
                Shoot();
                LastShoot = Time.time; // Actualiza el tiempo del último disparo
            }
        }
        else
        {
            // Si Paco está fuera del rango, el jefe entra en estado "idle" (sin acción)
            animator.SetBool("isAttacking", false);
        }
    }

    private void MoveTowardsPaco(Vector3 direction)
    {
        // Mueve al jefe hacia Paco, pero solo horizontalmente
        rb.velocity = new Vector2(direction.normalized.x * runSpeed, rb.velocity.y);
    }

    private void Shoot()
    {
        // Determinamos la dirección del disparo según la orientación del jefe
        Vector3 direction = transform.localScale.x == originalScale.x ? Vector2.right : Vector2.left;

        // Calculamos la posición inicial del proyectil para que salga del borde del jefe
        Vector3 spawnPosition = transform.position + direction * 0.2f;

        // Instanciamos el proyectil y le asignamos su dirección
        GameObject bullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        // Reduce la vida del jefe cada vez que recibe daño
        Health--;
        Debug.Log("Boss recibió daño. Vida restante: " + Health);

        // Si la vida llega a 0, destruye al jefe
        if (Health <= 0)
        {
            Debug.Log("Boss destruido.");
            Destroy(gameObject);
        }
        else if (Health <= 3 && !isRunning) // Si la vida está a la mitad o menos, activa el modo "correr"
        {
            isRunning = true;
            animator.SetBool("isRunning", true); // Cambia la animación a "correr"
        }
    }
}
