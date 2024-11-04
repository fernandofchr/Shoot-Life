using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoter : MonoBehaviour
{

    public Animator QuestionMark;
    public Animator ExclamationMark;

    public bool EnableQuestionMark;
    public bool EnableExclamationMark; 

    // Update is called once per frame
    void Update()
    {
        if (EnableExclamationMark)
        {
            ExclamationMark.SetBool("enable", true);
        }

        else
        {
            ExclamationMark.SetBool("enable", false);
        }

        if (EnableQuestionMark)
        {
            QuestionMark.SetBool("enable", true);
        }
        else
        {
            QuestionMark.SetBool("enable", false);
        }




    }
}
