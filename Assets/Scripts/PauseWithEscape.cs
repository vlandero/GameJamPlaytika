using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWithEscape : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
}
