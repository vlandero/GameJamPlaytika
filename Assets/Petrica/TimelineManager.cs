using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private List<PlayableDirector> _timelines = new List<PlayableDirector>();
    private int _index;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _timelines.Add(child.GetComponent<PlayableDirector>());
            child.gameObject.SetActive(false);
        }
    }
    
    void Start()
    {
        if (_timelines.Count > 0)
        { 
            _index = Random.Range(0, _timelines.Count);
            ActivateTimeline(_timelines[_index]);
        };

    }

    /// <summary>
    /// This is a helper method which starts the timeline on a unactive object.
    /// </summary>
    /// <param name="timeline">The timeline which should be played.</param>
    private void ActivateTimeline(PlayableDirector timeline)
    {
        timeline.gameObject.SetActive(true);
        timeline.Play();
    }

    /// <summary>
    /// This method will be called at the end of each timeline in main menu
    /// and will choose randomly another timeline from the list based on which half it is.
    /// (I did so to be sure it doesn't play the same timeline) 
    /// </summary>
    public void OnTimelineFinished()
    {
        _timelines[_index].gameObject.SetActive(false);

        _index = _index >= (_timelines.Count - 1) / 2 ? Random.Range(0, _index-1) : Random.Range(_index+1, _timelines.Count - 1);

        ActivateTimeline(_timelines[_index]);
    }
}
