using UnityEngine;
using System.Collections;

public static class CameraSwap
{
    public static bool isSwitching = false;

    public static IEnumerator SwitchCameras(Camera fromCam, Camera toCam, float duration)
    {
        isSwitching = true;

        Vector3 oldPosition = fromCam.transform.localPosition;
        Quaternion oldRotation = fromCam.transform.localRotation;
        
        float elapsedTime = 0;

        while (elapsedTime < duration && Vector3.Distance(fromCam.transform.position, toCam.transform.position) > 0.15f)
        {
            fromCam.transform.position = Vector3.Lerp(fromCam.transform.position, toCam.transform.position, elapsedTime/duration);
            fromCam.transform.rotation = Quaternion.Slerp(fromCam.transform.rotation, toCam.transform.rotation, elapsedTime/duration);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(duration/100f);
        }

        toCam.gameObject.SetActive(true);
        fromCam.gameObject.SetActive(false);

        fromCam.transform.localPosition = oldPosition;
        fromCam.transform.localRotation = oldRotation;

        isSwitching = false;
    }

}
