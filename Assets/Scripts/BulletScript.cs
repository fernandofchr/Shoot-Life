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
        if (paco !=null)
        {
            paco.Hit();
        }
        if (grunt !=null)
        {
            grunt.Hit();
        }
        DestroyBullet(); 
    }
}