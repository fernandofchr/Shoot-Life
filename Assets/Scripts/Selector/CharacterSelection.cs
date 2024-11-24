using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public int[] index;
    public Image[] selectionBoxes;

    void Start()
    {
        foreach (var img in this.selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        this.Select(0);
    }

    public void Select(int index)
    {
        foreach (var img in this.selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }

        this.selectionBoxes[index].gameObject.SetActive(true);
        PlayerManager.Instance.selectedCharacterIndex = index; ;
    }
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
