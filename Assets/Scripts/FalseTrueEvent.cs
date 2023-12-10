using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FalseTrueEvent : MonoBehaviour
{
    public Quiz[] quiz;

    public UnityEvent onTrue;
    public UnityEvent onFalse;


    private void Start()
    {
        for (int i = 0; i < quiz.Length; i++)
        {
            quiz[i].onRightAnswer = onTrue;
            quiz[i].onFalseAnswer = onFalse;
        }
    }
}
