using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private PacoMovement pacoMovement;

    private void Start()
    {
        // Encuentra la referencia al script PacoMovement
        pacoMovement = FindObjectOfType<PacoMovement>();
    }


    public void Jump()
    {
        if (pacoMovement.Grounded)
        {
            pacoMovement.SendMessage("Jump");
        }
    }

    public void Shoot()
    {
        pacoMovement.SendMessage("Shoot");
    }
}
