using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _feedbackToPlay = new List<Feedback>();

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach(var f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }
    public void FinishFeedback()
    {
        foreach(var f in _feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }

}
