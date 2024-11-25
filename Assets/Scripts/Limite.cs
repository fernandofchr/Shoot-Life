using UnityEngine;
using UnityEngine.SceneManagement;

public class Limite : MonoBehaviour
{
    public GameObject gameOverCanvas; // Canvas que aparece al morir

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
            // Destruir el jugador
            Destroy(collision.gameObject);
                gameOverCanvas.SetActive(true);
        }
    }

    // Método que puede ser llamado desde el botón de "Reiniciar"
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Restaurar la escala del tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }

    // Método opcional para salir del juego
     public void Salir(){
        Debug.Log("Salir..");
        SceneManager.LoadScene(0);
    }
}
