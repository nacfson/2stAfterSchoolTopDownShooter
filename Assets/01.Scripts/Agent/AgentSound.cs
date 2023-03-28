using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip stepSound,hitClip,deathClip,attackClip;

    public void PlayStepSound()
    {
        PlayClipWithVariableSound(stepSound);
    }

    public void PlayHitClip()
    {
        PlayClipWithVariableSound(hitClip);
    }

    public void PlayDeathClip()
    {
        PlayClip(deathClip);
    }

    public void PlayAttackSound()
    {
        PlayClip(attackClip);
    }














    
}
