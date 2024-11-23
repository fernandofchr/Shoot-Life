using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public int selectedCharacterIndex; // Aquí guardas el índice del personaje elegido

    private void Awake()
    {
        // Asegúrate de que solo hay una instancia de PlayerManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
