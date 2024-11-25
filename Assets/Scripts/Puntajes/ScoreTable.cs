using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al texto donde mostrar los puntajes

    void Start()
    {
        DisplayScores();
    }

    void DisplayScores()
    {
        Dictionary<string, int> scores = ScoreManager.GetAllScores();
        scoreText.text = ""; // Limpiar el texto previo

        foreach (var score in scores)
        {
            scoreText.text += $"{score.Key}: {score.Value}\n"; // Mostrar nombre del nivel y su puntaje
        }
    }
}
