using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResumeFromPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;


    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

    }
     public void CloseSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);

    }

}
