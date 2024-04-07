using System.Collections;
using UnityEngine;

public class Brick2D : MonoBehaviour
{
    public Vector3 _initialPos;

    public void Start()
    {
        _initialPos = transform.localPosition;
    }
    public void MoveTo(Transform target, float duration, bool destroy)
    {
        if(destroy)
            StartCoroutine(MoveToCoroDestroy(target, duration));
        else
            StartCoroutine(MoveToCoro(target, duration));
    }
    public IEnumerator MoveToCoroDestroy(Transform target, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration && Vector3.Distance(transform.position, target.position) > 0.15f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, elapsedTime / duration);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(duration / 100f);
        }

        Destroy(gameObject);
    }

    public IEnumerator MoveToCoro(Transform target, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration && Vector3.Distance(transform.position, target.position) > 0.15f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, elapsedTime / duration);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(duration / 100f);
        }
    }

}
