using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public GameObject BulletPrefab; // Prefab del proyectil
    public GameObject Paco; // Referencia al objeto de Paco
    private float LastShoot; // Control de tiempo entre disparos
    public int Health = 5; // Vida del Boss
    public float detectionRange = 5f; // Rango de detección
    private Animator animator;
    private PacoMovement pacoMovement; // Referencia al script de movimiento de Paco
    private Vector3 originalScale; // Escala original del Boss

    private void Start()
    {
        // Inicializa el Animator y busca el script de movimiento de Paco
        animator = GetComponent<Animator>();
        if (Paco != null)
        {
            pacoMovement = Paco.GetComponent<PacoMovement>();
        }

        // Guarda la escala original del Boss
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Paco == null) return;

        // Dirección y orientación hacia Paco
        Vector3 direction = Paco.transform.position - transform.position;

        // Ajusta la orientación del Boss sin afectar el tamaño original
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Calcula la distancia entre el Boss y Paco
        float distance = Mathf.Abs(Paco.transform.position.x - transform.position.x);

        // Activa la animación de ataque si Paco está dentro del rango de detección
        if (distance <= detectionRange)
        {
            animator.SetBool("isAttacking", true);

            // Si está dentro de un rango cercano y ha pasado el tiempo de disparo, dispara
            if (distance < 0.9f && Time.time > LastShoot + 0.5f)
            {
                Shoot();
                LastShoot = Time.time;
            }
        }
        else
        {
            // Si Paco está fuera del rango, cambia a la animación idle
            animator.SetBool("isAttacking", false);
        }
    }

    private void Shoot()
    {
        // Determina la dirección del disparo basado en la orientación del Boss
        Vector3 direction = transform.localScale.x == originalScale.x ? Vector2.right : Vector2.left;

        // Ajusta la posición de aparición de la bala para que esté justo en el borde del personaje
        Vector3 spawnPosition = transform.position + direction * 0.2f;

        // Crea y lanza la bala en la dirección correspondiente
        GameObject bullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--; // Reduce la salud del Boss
        if (Health == 0)
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            if (pacoMovement != null)
            {
                pacoMovement.Heal(1);
                pacoMovement.AddScore(250); // Paco recupera 1 de salud cuando mata al Boss
                ScoreManager.SaveScore(currentLevelName, pacoMovement.GetScore());
            }
            Destroy(gameObject); // Destruye el Boss
            if (currentLevelName == "Nivel2")
            {
                ScoreManager.SaveScore(currentLevelName, pacoMovement.GetScore());//Se guarda el puntaje
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Cambia de nivel 
            }else{
                Debug.Log("Estamos en el nivel 3");
            }
        }
    }
}
