using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pause_menu;
    private bool isActive = false;
    private InputAction pauseAction;

    void Start()
    {
        pause_menu.SetActive(isActive);
        Time.timeScale = 1;

        pauseAction = new InputAction(binding: "<Keyboard>/escape");
        pauseAction.performed += _ => Pause();
        pauseAction.Enable();
    }


    public void Pause()
    {
        isActive = !isActive; 
        pause_menu.SetActive(isActive);
        Time.timeScale = isActive ? 0 : 1;
    }

    public void Resume()
    {
        isActive = false; 
        pause_menu.SetActive(isActive);
        Time.timeScale = 1;
    }
}
