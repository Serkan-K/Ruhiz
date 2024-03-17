using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rot_Z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot_Z);
    }
}
