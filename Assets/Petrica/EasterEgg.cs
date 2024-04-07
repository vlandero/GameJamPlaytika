using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    // Update is called once per frame
    public static int secret;
    public AudioSource audioSource;

    private void Start()
    {
        secret = PlayerPrefs.GetInt("SecretLevel2");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            audioSource.Play();
            secret = 1;
            Destroy(gameObject, 0.5f);
        }
    }
}
