using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeWallController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float blinkWaitTime = 5; 
    void Start()
    {

    }

    IEnumerator triggerEyes()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(blinkWaitTime);

            anim.SetTrigger("blink");




        }
    }



}
