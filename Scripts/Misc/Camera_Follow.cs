using UnityEngine;
using Cinemachine;

public class CameraFollow_ : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    public CinemachineVirtualCamera virtual_Camera;


    private void Start()
    {
        FindPlayerObject();
    }

    private void FindPlayerObject()
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (player != null && virtual_Camera != null)
        {
            virtual_Camera.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
    }
}
