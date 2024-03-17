using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Load_level(int levelIndex)
    {

        if (levelIndex >= 0 &&
            levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

    public void Play() { Load_level(3); }

    public void Options() { Load_level(1); }

    public void Credits() { Load_level(2); }

    public void Exit() { Application.Quit(); }
}