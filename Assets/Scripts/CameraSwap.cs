using UnityEngine;
using System.Collections;

public static class CameraSwap
{
    public static bool isSwitching = false;

    public static IEnumerator SwitchCameras(Camera fromCam, Camera toCam, float duration)
    {
        Debug.Log("Am inceput");
        isSwitching = true;

        Vector3 oldPosition = fromCam.transform.position;
        Quaternion oldRotation = fromCam.transform.rotation;
        
        float elapsedTime = 0;

        while (elapsedTime < duration && fromCam.transform.position != toCam.transform.position)
        {
            fromCam.transform.position = Vector3.Lerp(fromCam.transform.position, toCam.transform.position, elapsedTime/duration);
            fromCam.transform.rotation = Quaternion.Slerp(fromCam.transform.rotation, toCam.transform.rotation, elapsedTime/duration);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(duration/100f);
        }

        toCam.gameObject.SetActive(true);
        fromCam.gameObject.SetActive(false);

        fromCam.transform.position = oldPosition;
        fromCam.transform.rotation = oldRotation;

        isSwitching = false;
        Debug.Log("Am terminat");
        
    }

}
