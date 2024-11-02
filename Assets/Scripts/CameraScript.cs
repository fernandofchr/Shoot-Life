using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Paco;
    void Update()
    {
        if (Paco == null) return;
        Vector3 position = transform.position;
        position.x = Paco.transform.position.x;
        transform.position = position;
    }
}
