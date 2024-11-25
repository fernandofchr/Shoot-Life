using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivel : MonoBehaviour
{
    [ContextMenu("boton cambiar nivel")]
    public void SubirNivel()
    {
        // Guardar el puntaje del nivel actual
        string currentLevelName = SceneManager.GetActiveScene().name;
        PacoMovement paco = FindObjectOfType<PacoMovement>(); // Buscar el script del personaje
        if (paco != null)
        {
            ScoreManager.SaveScore(currentLevelName, paco.GetScore()); // Guardar el puntaje actual del nivel
        }

        // Cargar el siguiente nivel
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual + 1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "subirnivel")
        {
            SubirNivel();
        }
    }
}
