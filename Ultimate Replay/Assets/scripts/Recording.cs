using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Recording : MonoBehaviour
{
    [HideInInspector]
    public List<Frame> Frames;
    [HideInInspector]
    public bool ReplayCompleteChecker = false;
    string ObjectName;
    [HideInInspector]
    public int length = 0;
    [HideInInspector]
    public int gotoFrame = 0;
    Animator animator;
    RecordPlayer recordPlayer;
    SaveSystem saveSystem;

    private void Start()
    {
        length = 0;
        gotoFrame = 0;
        ObjectName = gameObject.name;
        Frames = new List<Frame>();
        animator = GetComponent<Animator>();
        recordPlayer = FindObjectOfType<RecordPlayer>();
        saveSystem = FindObjectOfType<SaveSystem>();
        RecordPlayer.SaveRecordings += () =>
        {
            saveSystem.AddSavedRecording(this);
        };
        RecordPlayer.ResetLength += () =>
        {
            length = 0;
            gotoFrame = 0;
        };
        RecordPlayer.TimeDirectionChange += ChangeGoToFrameForReverseTime;
    }
    private void FixedUpdate()
    {

        if (RecordPlayer.replay_state == Replay_State.Recording)
        {
            RecordFrame();
        }
      
        else if (RecordPlayer.replay_state != Replay_State.none)
        {
            PlayFrame();
        }

    }

    void RecordFrame()
    {
        if (length == recordPlayer.MaxLength)
        {
            Frames.RemoveAt(0);
            length--;
            UpdateLength(length);
        }

        if (animator != null)
        {
            List<AnimationParameter> parameters = new List<AnimationParameter>();
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    parameters.Add(new AnimationParameter(parameter.name, animator.GetBool(parameter.name), parameter.type));
                }
                else if (parameter.type == AnimatorControllerParameterType.Float)
                {
                    parameters.Add(new AnimationParameter(parameter.name, animator.GetFloat(parameter.name), parameter.type));
                }
                else
                {
                    parameters.Add(new AnimationParameter(parameter.name, animator.GetInteger(parameter.name), parameter.type));
                }
            }
            if (length == Frames.Count)
            {
                Frames.Add(new Frame(transform, parameters, animator.GetCurrentAnimatorStateInfo(0).fullPathHash, animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
            }
            else
            {
                Frames[length] = new Frame(transform, parameters, animator.GetCurrentAnimatorStateInfo(0).fullPathHash, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
        }
        else
        {
            if (length == Frames.Count)
            {
                Frames.Add(new Frame(transform));
            }
            else
            {
                Frames[length] = new Frame(transform);
            }
        }
        length++;
        UpdateLength(length);
    }

    void PlayFrame()
    {
        if (gotoFrame == length && RecordPlayer.TimeMovesForward == true)
        {
            gotoFrame = 0;
            if (ReplayCompleteChecker == true)
            {
                RecordPlayer.ReplayComplete();
            }
        }
        else if (gotoFrame == -1 && RecordPlayer.TimeMovesForward == false)
        {
            gotoFrame = length - 1;
            if (ReplayCompleteChecker == true)
            {
                RecordPlayer.ReplayComplete();
            }
        }
        else
        {
            Frame frame = Frames[gotoFrame];
            transform.position = frame.position;
            transform.rotation = frame.rotation;
            transform.localScale = frame.scale;
            if (animator != null)
            {
                if (RecordPlayer.SliderControl == false && RecordPlayer.replay_state == Replay_State.pause)
                {
                    animator.speed = 0;
                }
                else
                {
                    animator.speed = 1;
                }
                foreach (AnimationParameter parameter in frame.AnimationParameters)
                {
                    if (parameter.Parameter_Type == AnimatorControllerParameterType.Bool)
                    {
                        animator.SetBool(parameter.Parameter_Name, parameter.Bool_Parameter);
                    }
                    else if (parameter.Parameter_Type == AnimatorControllerParameterType.Int)
                    {
                        animator.SetInteger(parameter.Parameter_Name, parameter.Int_Parameter);
                    }
                    else if (parameter.Parameter_Type == AnimatorControllerParameterType.Float)
                    {
                        animator.SetFloat(parameter.Parameter_Name, parameter.Float_Parameter);
                    }
                }
                animator.Play(frame.AnimatorStateNameHash);
                animator.Play(0, 0, frame.normalisedTime);
            }
            SetgotoFrame();
            UpdateFrame(gotoFrame);
        }
    }

    void UpdateLength(int length)
    {
        if (ReplayCompleteChecker == true)
        {
            recordPlayer.length = length;
        }
    }
    void UpdateFrame(int curFrame)
    {
        if (ReplayCompleteChecker == true)
        {
            recordPlayer.curFrame = curFrame;
        }
    }

    void SetgotoFrame()
    {
        if (RecordPlayer.SliderControl == false)
        {
            if (RecordPlayer.replay_state != Replay_State.pause)
            {
                if (RecordPlayer.TimeMovesForward)
                {
                    gotoFrame++;
                }
                else
                {
                    gotoFrame--;
                }
            }
            
        }
        else
        {
            gotoFrame = recordPlayer.sliderFrame;
        }
    }

    void ChangeGoToFrameForReverseTime()
    {
        if (RecordPlayer.TimeMovesForward == true && gotoFrame == length - 1)
        {
            gotoFrame = 0;
        }
        else if (RecordPlayer.TimeMovesForward == false && gotoFrame == 0)
        {
            gotoFrame = length - 1;
        }
    }


}
