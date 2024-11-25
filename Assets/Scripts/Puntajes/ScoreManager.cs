using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private static Dictionary<string, int> levelScores = new Dictionary<string, int>();

    // Guardar el puntaje de un nivel
    public static void SaveScore(string levelName, int score)
    {
        if (!levelScores.ContainsKey(levelName) || score > levelScores[levelName])
        {
            levelScores[levelName] = score;
            PlayerPrefs.SetInt(levelName, score);
        }
    }

    // Obtener el puntaje de un nivel
    public static int GetScore(string levelName)
    {
        if (levelScores.ContainsKey(levelName))
        {
            return levelScores[levelName];
        }

        return PlayerPrefs.GetInt(levelName, 0); // Si no está en memoria, busca en PlayerPrefs
    }

    // Obtener todos los puntajes
    public static Dictionary<string, int> GetAllScores()
    {
        return new Dictionary<string, int>(levelScores);
    }
}
