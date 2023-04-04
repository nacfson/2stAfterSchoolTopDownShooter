using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBloodEffect : Feedback
{
    [SerializeField]
    private AIActionData _aiActionData;
    [SerializeField]
    [Range(0f,1f)]
    private float _sizeFactor;
    public override void CompleteFeedback()
    {
    }

    public override void CreateFeedback()
    {
        TextureParticleManager.Instance.SpawnBlood(_aiActionData.hitPoint,_aiActionData.hitNormal,_sizeFactor);
    }
}
