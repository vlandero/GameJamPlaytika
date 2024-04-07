using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public List<Canvas> boxes;
    public int index;
    public int n;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
            boxes.Add(transform.GetChild(i).GetComponent<Canvas>());
        index = 0;
    }
    public void StartChatBox(int numberOfChats)
    {
        n = numberOfChats;
        if(numberOfChats > 0)
            StartCoroutine(Sequence(numberOfChats));
    }

    public IEnumerator Sequence(int n)
    {
         if(index < boxes.Count) 
        {
            boxes[index].gameObject.SetActive(true);
            yield return TutorialStep();
            boxes[index].gameObject.SetActive(false);
            index++;
            StartChatBox(n-1);
        }   
            
    }

    private IEnumerator TutorialStep()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSecondsRealtime(0.2f);

    }
}
