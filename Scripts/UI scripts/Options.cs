using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class Options : MonoBehaviour
{
    public Menu menu;


    public AudioMixer audio_Mixer;
    public TMP_Dropdown res_Drop;

    Resolution[] resolutions;


    void Start()
    {
        resolutions = Screen.resolutions;
        res_Drop.ClearOptions();

        List<string> options = new();
        int current_res = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = 
                resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);


            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                current_res = i;
            }
        }
        
        res_Drop.AddOptions(options);
        res_Drop.value = current_res;
        res_Drop.RefreshShownValue();
    }



    public void Set_Resolution(int resolution)
    {
        Resolution res = resolutions[resolution];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }



    public void Set_Volume(float volume)
    {
        audio_Mixer.SetFloat("volume_master", volume);
    }

    public void Set_Quality(int quality_index)
    {
        QualitySettings.SetQualityLevel(quality_index);
    }

    public void Set_Fullscreen(bool Is_fullscreen)
    {
        Screen.fullScreen = Is_fullscreen;
    }

    public void Main_menu()
    {
        menu.Load_level(0);
    }
}
