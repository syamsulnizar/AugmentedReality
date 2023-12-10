using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListQuiz : MonoBehaviour
{
    public GameObject await;

    public void IsAwait()
    {
        await.SetActive(true);
    }

    public void IsNotAwait()
    {
        await.SetActive(false);
    }
}
