using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float slowMotionTimeScale = 0.4f;
    public static float platformStopTime = 3f;
    public GameObject bricksComponent;
    public GameObject ball;
    public GameObject platform;
    public GameObject loseCanvas;
    public GameObject winCanvas;
    //public MoveArrow moveArrowComponent;
    [HideInInspector] public Camera SideCamera;
    [HideInInspector] public Camera PlatformCamera;
    [HideInInspector] public Ball ballComponent;
    [HideInInspector] public PlayerMovementWASD playerMovementwasd;
    [HideInInspector] public LauchBall lauchBall;
    [HideInInspector] public int bricksCount;
    [HideInInspector] public bool frozen = false;
    [HideInInspector] public CamerMovement camerMovement;
    [HideInInspector] public RotatePlatformCamera rotatePlatformCamera;
    [HideInInspector] public Tutorial tutorial;
    [HideInInspector] public bool slowMotionActive = false;
    [Header("Tutorial")]
    public bool tutorialActive = false;
    public GameObject tutorialCanvas;
    public Sprite leftClickSprite;
    public Sprite rightClickSprite;
    public Sprite wasdSprite;
    public Sprite middleClickSprite;
    [HideInInspector] public bool tutorialStepActive = false;
    [Header("Score")]
    public int platformHitTimes = 0;
    public int stars3HitTimes = 2;
    public int stars2HitTimes = 10;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        ballComponent = ball.GetComponent<Ball>();
        //moveArrowComponent = platform.GetComponent<MoveArrow>();
        playerMovementwasd = platform.GetComponent<PlayerMovementWASD>();
        lauchBall = platform.GetComponent<LauchBall>();
        rotatePlatformCamera = PlatformCamera.GetComponent<RotatePlatformCamera>();
        bricksCount = bricksComponent.transform.childCount;
        tutorial = tutorialCanvas.GetComponentInChildren<Tutorial>();
        tutorialStepActive = tutorialActive;

        lauchBall.enabled = false;
        playerMovementwasd.enabled = true;
    }

    //public Vector3 GetPlatformArrowDirection()
    //{
    //    return moveArrowComponent.vectorToDisplay;
    //}

    private void Update()
    {
        if(tutorialStepActive)
        {
            tutorialStepActive = false;
            tutorial.ShowTutorial();
        }
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
            GameOverLoss();
        }
        if(bricksCount == 0)
        {
            GameOverWin();
        }
    }

    public void GameOverLoss()
    {
        Time.timeScale = 0;
        loseCanvas.SetActive(true);
    }

    public void GameOverWin()
    {
        Time.timeScale = 0;
        string progressData = PlayerPrefs.GetString("LevelProgress");
        ToSerialize savedLevelsData = JsonUtility.FromJson<ToSerialize>(progressData);
        int starsGained = 1;
        if (platformHitTimes <= stars3HitTimes)
        {
            starsGained = 3;
        }
        else if (platformHitTimes <= stars2HitTimes)
        {
            starsGained = 2;
        }
        Debug.Log(progressData);
        int currentStars = savedLevelsData.levelsData[SceneManager.GetActiveScene().buildIndex - 2].starsGained;
        savedLevelsData.levelsData[SceneManager.GetActiveScene().buildIndex - 2].starsGained = starsGained > currentStars ? starsGained : currentStars;
        if(SceneManager.GetActiveScene().buildIndex - 1 <= 3)
        {
            savedLevelsData.levelsData[SceneManager.GetActiveScene().buildIndex - 1].unlocked = true;
        }
        progressData = JsonUtility.ToJson(savedLevelsData);
        PlayerPrefs.SetString("LevelProgress", progressData);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("SecretLevel2", EasterEgg.secret);
        winCanvas.GetComponentInChildren<ShowStars>().starNumber = starsGained;
        winCanvas.SetActive(true);
    }

    public void DestroyBrick()
    {
        bricksCount--;
    }

    public void SeePlatformPerspective()
    {
        rotatePlatformCamera.ResetRotation();
        frozen = true;
        camerMovement.enabled = false;
        StartCoroutine(CameraSwap.SwitchCameras(SideCamera, PlatformCamera, .5f));
        playerMovementwasd.enabled = false;
        lauchBall.enabled = true;
    }

    public void SeeSidePerspective()
    {
        frozen = false;
        camerMovement.enabled = true;
        StartCoroutine(CameraSwap.SwitchCameras(PlatformCamera, SideCamera, .5f));
        lauchBall.enabled = false;
        playerMovementwasd.enabled = true;
    }
}