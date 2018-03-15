using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public delegate void AnimationHandler();
    Animation animation;
    public static AnimationManager instance;

    public AnimationClip Run;
    public AnimationClip Dead;
    public AnimationClip JumpDown;
    public AnimationClip Roll;
    public AnimationClip JumpUp;
    public AnimationClip TurnRight;
    public AnimationClip TurnLeft;
    public AnimationClip JumpLoop;

    public AnimationHandler animationHandler;
    // Use this for initialization
    void Start () {
        instance = this;
        animationHandler = PlayRun;
        animation = GetComponent<Animation>();
	}
	
    public void PlayDead()
    {
        animation.Play(Dead.name);
    }
    public void PlayJumpDown()
    {
        animation.Play(JumpDown.name);
    }
    public void PlayJumpLoop()
    {
        animation.Play(JumpLoop.name);
    }
    public void PlayJumpUp()
    {
        animation.Play(JumpUp.name);
        if (animation[JumpUp.name].normalizedTime > 0.95f) 
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayTurnLeft()
    {
        animation.Play(TurnLeft.name);
        if (animation[TurnLeft.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayTurnRight()
    {
        animation.Play(TurnRight.name);
        if (animation[TurnRight.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayDoubleJump()
    {
        animation.Play(Roll.name);
        if (animation[Roll.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayJumpLoop;
        }
    }

    public void PlayRoll()
    {
        animation.Play(Roll.name);
        if (animation[Roll.name].normalizedTime > 0.95f)
        {
            animationHandler = PlayRun;
        }
    }
    public void PlayRun()
    {
        animation.Play(Run.name);
    }
    // Update is called once per frame
    void Update () {
        //animation.Play(Run.name);
        if(animationHandler != null)
        {
            animationHandler();
        }
	}
}
