using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalJuego : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public void Entrar(){
        SceneManager.LoadScene(0);
    }
}
