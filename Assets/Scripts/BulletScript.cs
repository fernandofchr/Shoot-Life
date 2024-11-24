using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;
    private Rigidbody2D Rigidbody2D;

    private Vector2 Direction;
    // private bool isDead = false;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        PacoMovement paco = collider.GetComponent<PacoMovement>();
        GruntScript grunt = collider.GetComponent<GruntScript>(); 
        BossScript boss = collider.GetComponent<BossScript>(); 
        Boss3Script boss3 = collider.GetComponent<Boss3Script>();
        SoldierScript soldier = collider.GetComponent<SoldierScript>();

        if (paco !=null)
        {
            paco.Hit();
        }
        if (grunt !=null)
        {
            grunt.Hit();
        }
        if (boss !=null)
        {
            boss.Hit();
        }
                if (boss3 !=null)
        {
            boss3.Hit();
        }if(soldier !=null){
            soldier.Hit();
        }
        DestroyBullet(); 
    }
}
