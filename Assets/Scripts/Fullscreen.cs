using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public void ChangeFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("changed screen mode");
    }
}
