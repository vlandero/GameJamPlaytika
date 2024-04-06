using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public Image tutorialImage;
    private int currentStep = 0;

    public void ShowTutorial()
    {
        StartCoroutine(Sequence());
    }

    public IEnumerator Sequence()
    {
        GameManager.instance.frozen = true;
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        if (currentStep == 0)
        {
            tutorialText.text = "Welcome to the tutorial! Click anywhere to continue.";
            tutorialImage.color = new Color(255, 255, 255, 0);
            yield return TutorialStep();
            tutorialText.text = "Use your left mouse button to move the platform.";
            tutorialImage.sprite = GameManager.instance.leftClickSprite;
            tutorialImage.color = new Color(255, 255, 255, 1);
            yield return TutorialStep();
            tutorialText.text = "Use your right mouse button to move the camera around.";
            tutorialImage.sprite = GameManager.instance.rightClickSprite;
            yield return TutorialStep();
            currentStep++;
        }
        else if (currentStep == 1)
        {
            tutorialText.text = "Use your left mouse button to shoot the ball.";
            tutorialImage.sprite = GameManager.instance.leftClickSprite;
            tutorialImage.color = new Color(255, 255, 255, 1);
            yield return TutorialStep();
            tutorialText.text = "Use your right mouse button to rotate the camera around.";
            tutorialImage.sprite = GameManager.instance.rightClickSprite;
            yield return TutorialStep();
            currentStep++;
        }
        GetComponent<Canvas>().enabled = false;
        GameManager.instance.frozen = false;
        Time.timeScale = 1;
    }

    private IEnumerator TutorialStep()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSecondsRealtime(0.2f);
        
    }
}
