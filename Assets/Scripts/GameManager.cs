using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float slowMotionTimeScale = 0.2f;
    public int score = 0;
    public GameObject ball;
    public GameObject platform;
    public MoveArrow moveArrowComponent;
    private Ball ballComponent;

    private Rigidbody ballRb;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        ballRb = ball.GetComponent<Rigidbody>();
        ballComponent = ball.GetComponent<Ball>();
        
        platform = GameObject.FindGameObjectWithTag("platform");
        moveArrowComponent = platform.GetComponent<MoveArrow>();
    }

    public Vector3 GetPlatformArrowDirection()
    {
        return moveArrowComponent.vectorToDisplay;
    }

    private void Update()
    {
        if (Mathf.Abs(ball.transform.position.y - platform.transform.position.y) < 3)
        {
            if(ballComponent.IsMovingUp())
            {
                moveArrowComponent.DeactivateArrow();
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = slowMotionTimeScale;
                moveArrowComponent.ActivateArrow();
            }
            
        }
        if (ball.transform.position.y < platform.transform.position.y - 2)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

    public void AddScore(int value)
    {
        score += value;
    }
}
