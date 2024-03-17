using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ffinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SsceneController.instance.NextLevel(); // Transition to the third scene
        }
    }
}

