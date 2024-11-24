using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PacoMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public float JumpForce;
    public float Speed;   
    public bool Grounded; 
    public float LastShoot;

    // Variables de vida y corazones
    public int maxHealth = 10; // Salud máxima de Paco (equivalente a la cantidad de corazones)
    public int currentHealth;
    public Image[] hearts; // Arreglo de imágenes de corazones en el UI
    public Sprite fullHeart; // Sprite del corazón lleno
    public Sprite emptyHeart; // Sprite del corazón vacío
    public RuntimeAnimatorController[] animatorControllers; // Arreglo de controladores para los personajes

    // Variables de puntuación
    private int score = 0; // Puntuación inicial
    public TextMeshProUGUI scoreText;
 // Texto de UI para mostrar la puntuación

    void Start()
    {
        //Referencia al cuerpo de Paco
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //Referencia a las animaciones de Paco
        Animator = GetComponent<Animator>();
        // Obtén el índice del personaje seleccionado desde PlayerManager
        int selectedIndex = PlayerManager.Instance.selectedCharacterIndex;
        
        // Valida el índice y asigna el controlador correspondiente
        if (selectedIndex >= 0 && selectedIndex < animatorControllers.Length)
        {
            Animator.runtimeAnimatorController = animatorControllers[selectedIndex];
        }
        else
        {
            Debug.LogError("Índice de personaje seleccionado fuera de rango. Revisa PlayerManager o asigna controladores en el Inspector.");
        }

        // Inicializamos la salud actual con la salud máxima
        currentHealth = maxHealth;
        UpdateHearts(); // Actualizamos los corazones en la UI
        UpdateScoreText(); // Actualizamos la puntuación en la UI
    }

    void Update()
    {
        // Movimiento y lógica de salto y disparo (igual que en tu código original)
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.08f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    // Función para recibir daño y actualizar la vida
    public void Hit()
    {
        currentHealth--;
        UpdateHearts(); // Actualizamos la UI después de recibir daño

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Paco ha muerto");
        }
    }

    // Método para incrementar la puntuación
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText(); // Actualizar el texto de la puntuación en la UI
    }

    // Método para actualizar el texto de la puntuación
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Función para actualizar la UI de corazones
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart; // Mostrar corazón lleno
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Mostrar corazón vacío
            }

            // Oculte los corazones si son mayores que la salud máxima
            hearts[i].enabled = (i < maxHealth);
        }
    }

    // Función para recuperar salud
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Asegurarse de no exceder la salud máxima
        }
        UpdateHearts();
    }
}
