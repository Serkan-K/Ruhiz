using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Menu menu;

    public void Back()
    {
        menu.Load_level(0);
    }
}
