using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivel : MonoBehaviour
{
    [ContextMenu("boton cambiar nivel")]
    public void SubirNivel(){
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene( nivelActual + 1 );
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "subirnivel")
        {
            SubirNivel();
        }

    }
}
