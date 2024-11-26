using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectNiveles : MonoBehaviour
{
    public int[] indices; // Arreglo de índices de las escenas
    private int selectedIndex = 0; // Índice seleccionado actualmente

    void Start()
    {
        // Asegurarse de seleccionar el primer índice por defecto
        Select(0);
    }

    // Método para seleccionar un índice (llamado desde los botones de selección)
    public void Select(int index)
    {
        if (index >= 0 && index < indices.Length)
        {
            selectedIndex = indices[index]; // Actualizamos el índice seleccionado
            Debug.Log($"Índice seleccionado: {selectedIndex}");
        }
        else
        {
            Debug.LogError("El índice seleccionado está fuera del rango del arreglo de índices.");
        }
    }

    // Método para cargar la escena basada en el índice seleccionado (llamado desde el botón "Jugar")
    public void Entrar()
    {
        if (selectedIndex >= 0)
        {
            Debug.Log($"Cargando escena con índice: {selectedIndex}");
            SceneManager.LoadScene(selectedIndex); // Cargar la escena basada en el índice
        }
        else
        {
            Debug.LogError("No se ha seleccionado un índice válido.");
        }
    }
}
