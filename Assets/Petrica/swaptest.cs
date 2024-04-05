using UnityEngine;

public class swaptest : MonoBehaviour
{
    public Camera _cam1;
    public Camera _cam2;
    public float _speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !CameraSwap.isSwitching && _cam1.isActiveAndEnabled)
            StartCoroutine(CameraSwap.SwitchCameras(_cam1, _cam2, _speed));
        if (Input.GetKeyDown(KeyCode.D) && !CameraSwap.isSwitching && _cam2.isActiveAndEnabled)
            StartCoroutine(CameraSwap.SwitchCameras(_cam2, _cam1, _speed));
    }
}
