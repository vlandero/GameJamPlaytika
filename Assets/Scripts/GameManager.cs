using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float slowMotionTimeScale = 0.4f;
    public static float platformStopTime = 3f;
    public int score = 0;
    public GameObject ball;
    public GameObject platform;
    //public MoveArrow moveArrowComponent;
    [HideInInspector] public Camera SideCamera;
    [HideInInspector] public Camera PlatformCamera;
    [HideInInspector] public Ball ballComponent;
    [HideInInspector] public PlayerMovementPlatform playerMovementPlatform;
    [HideInInspector] public LauchBall lauchBall;
    public bool frozen = false;
    public CamerMovement camerMovement;
    public RotatePlatformCamera rotatePlatformCamera;

    public bool slowMotionActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ballComponent = ball.GetComponent<Ball>();
        //moveArrowComponent = platform.GetComponent<MoveArrow>();
        playerMovementPlatform = platform.GetComponent<PlayerMovementPlatform>();
        lauchBall = platform.GetComponent<LauchBall>();
        rotatePlatformCamera = PlatformCamera.GetComponent<RotatePlatformCamera>();
    }

    //public Vector3 GetPlatformArrowDirection()
    //{
    //    return moveArrowComponent.vectorToDisplay;
    //}

    private void Update()
    {
        if (Mathf.Abs(ball.transform.position.y - platform.transform.position.y) < 3)
        {
            if(ballComponent.rb.velocity.y < 0)
            {
                if (frozen) return;
                Time.timeScale = slowMotionTimeScale;
            }
            else
            {
                Time.timeScale = 1;
            }
            
            //moveArrowComponent.ActivateArrow();
        }
        if (ball.transform.position.y < platform.transform.position.y - 1)
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

    public void SeePlatformPerspective()
    {
        rotatePlatformCamera.ResetRotation();
        frozen = true;
        camerMovement.enabled = false;
        StartCoroutine(CameraSwap.SwitchCameras(SideCamera, PlatformCamera, .5f));
        playerMovementPlatform.enabled = false;
        lauchBall.enabled = true;
    }

    public void SeeSidePerspective()
    {
        frozen = false;
        camerMovement.enabled = true;
        StartCoroutine(CameraSwap.SwitchCameras(PlatformCamera, SideCamera, .5f));
        lauchBall.enabled = false;
        playerMovementPlatform.enabled = true;
    }
}