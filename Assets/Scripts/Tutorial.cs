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
            tutorialText.text = "Use your WASD keys to move the platform.";
            tutorialImage.sprite = GameManager.instance.wasdSprite;
            tutorialImage.color = new Color(255, 255, 255, 1);
            yield return TutorialStep();
            tutorialText.text = "Use your right mouse button to move the camera around.";
            tutorialImage.sprite = GameManager.instance.rightClickSprite;
            yield return TutorialStep();
            currentStep++;
            GameManager.instance.ballComponent.ballSpeed = GameManager.instance.ballComponent.tutorialBallSpeed;
        }
        else if (currentStep == 1)
        {
            tutorialText.text = "You can launch the ball wherever you want. You are the platform now.";
            tutorialImage.sprite = null;
            tutorialImage.color = new Color(255, 255, 255, 0);
            yield return TutorialStep();
            tutorialText.text = "Use your left mouse button to shoot the ball.";
            tutorialImage.sprite = GameManager.instance.leftClickSprite;
            tutorialImage.color = new Color(255, 255, 255, 1);
            yield return TutorialStep();
            tutorialText.text = "Use your middle mouse button to rotate the camera around.";
            tutorialImage.sprite = GameManager.instance.middleClickSprite;
            tutorialImage.color = new Color(255, 255, 255, 1);
            yield return TutorialStep();
            currentStep++;
            GameManager.instance.ballComponent.ballSpeed = GameManager.instance.ballComponent.normalBallSpeed;
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
