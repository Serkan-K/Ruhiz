using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ExplosionDestroy",2f);
    }

    void ExplosionDestroy()
    {
        Destroy(gameObject);
    }
}
