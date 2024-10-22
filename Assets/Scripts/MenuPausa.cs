using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool JuegoPausado = false;
    //Pausat con boton P
    public void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            if(JuegoPausado){
                Renaudar();
            }else{
                Pausa();
            }
        }
    }
    //Pausar usando el boton de la esquina
    public void Pausa(){
        JuegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }
    //Renaudar desde el menu de pausa
    public void Renaudar(){
        JuegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    //Reiniciar partida desde menu de pausa
    public void Reiniciar(){
        JuegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Cerrar el juego desde el menu de pausa
    public void Cerrar(){
        Debug.Log("Salio..");
        Application.Quit();
    }
}
