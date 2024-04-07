using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallWithPlatform : MonoBehaviour
{
    public GenerateLevelsUI generateLevelsUI;
    public GameObject pressToPlay;
    public float distanceToCube = 50f;
    public float movementDuration = 0.5f;

    private Coroutine moveCoroutine;

    private void Start()
    {
        MoveBallToLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBallRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBallLeft();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
    }

    IEnumerator SmoothMove(RectTransform targetRectTransform)
    {
        pressToPlay.SetActive(false);

        RectTransform ballRectTransform = GetComponent<RectTransform>();
        Vector2 startPosition = ballRectTransform.anchoredPosition;
        Vector2 targetPosition = targetRectTransform.anchoredPosition;

        float elapsedTime = 0f;
        while (elapsedTime < movementDuration)
        {
            float t = elapsedTime / movementDuration;
            ballRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        ballRectTransform.anchoredPosition = targetPosition;

        pressToPlay.SetActive(true);
    }

    void MoveBallRight()
    {
        if (generateLevelsUI.currentLevel < generateLevelsUI.levelsData.Length)
        {
            generateLevelsUI.currentLevel++;
            RectTransform targetRectTransform = generateLevelsUI.cubes[generateLevelsUI.currentLevel].GetComponent<RectTransform>();
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(targetRectTransform));
        }
    }

    void MoveBallLeft()
    {
        if (generateLevelsUI.currentLevel > 1)
        {
            generateLevelsUI.currentLevel--;
            RectTransform targetRectTransform = generateLevelsUI.cubes[generateLevelsUI.currentLevel].GetComponent<RectTransform>();
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(targetRectTransform));
        }
    }

    public void MoveBallToLevel()
    {
        RectTransform targetRectTransform = generateLevelsUI.cubes[generateLevelsUI.currentLevel].GetComponent<RectTransform>();
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(SmoothMove(targetRectTransform));
    }

    public void StartLevel()
    {
        if (!generateLevelsUI.levelsData[generateLevelsUI.currentLevel - 1].unlocked) return;
        SceneLoader sceneLoader = new SceneLoader();
        sceneLoader.LoadScene(generateLevelsUI.currentLevel + 1);
    }
}
