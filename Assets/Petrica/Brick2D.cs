using System.Collections;
using UnityEngine;

public class Brick2D : MonoBehaviour
{
    public void MoveTo(Transform target, float duration)
    {
        StartCoroutine(MoveToCoro(target, duration));
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

        Destroy(gameObject);
    }
}
