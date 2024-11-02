using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Paco;
    private float LastShoot;
    public int Health = 3;

    private PacoMovement pacoMovement; // Referencia al script de Paco

    private void Start()
    {
        // Buscamos el script de Paco al inicio del juego
        if (Paco != null)
        {
            pacoMovement = Paco.GetComponent<PacoMovement>();
        }
    }

    private void Update()
    {
        if (Paco == null) return;

        Vector3 direction = Paco.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Paco.transform.position.x - transform.position.x);

        if (distance < 0.9f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.08f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--;
        if (Health == 0)
        {
            if (pacoMovement != null)
            {
                pacoMovement.Heal(1); // Paco recupera 1 de salud cuando mata al enemigo
            }
            Destroy(gameObject); // Destruir el enemigo
        }
    }
}
