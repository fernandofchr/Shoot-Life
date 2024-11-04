using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject canvasReglas; // Canvas para las reglas del juego
    [SerializeField] private float tiempoVisible = 5f; // Tiempo que el Canvas estará visible

    private bool JuegoPausado = false;

    // Método de inicio para mostrar las reglas al principio
    void Start()
    {
        if (canvasReglas != null)
        {
            canvasReglas.SetActive(true); // Activa el Canvas de reglas
            Invoke("OcultarCanvasReglas", tiempoVisible); // Llama a OcultarCanvasReglas después del tiempo
        }
    }

    // Método para ocultar el Canvas de reglas
    void OcultarCanvasReglas()
    {
        if (canvasReglas != null)
        {
            canvasReglas.SetActive(false); // Desactiva el Canvas de reglas
        }
    }

    // Pausar con el botón P
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (JuegoPausado)
            {
                Renaudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    // Pausar usando el botón de la esquina
    public void Pausa()
    {
        JuegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    // Reanudar desde el menú de pausa
    public void Renaudar()
    {
        JuegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    // Reiniciar partida desde menú de pausa
    public void Reiniciar()
    {
        JuegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Cerrar el juego desde el menú de pausa
    public void Cerrar()
    {
        Debug.Log("Salió...");
        Application.Quit();
    }
}
