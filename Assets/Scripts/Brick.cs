using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health = 1;
    public int score = 1;
    
    public void HandleHit()
    {
        health--;
        if (health <= 0)
        {
            GameManager.instance.DestroyBrick();
            Destroy(gameObject);
        }
    }
}