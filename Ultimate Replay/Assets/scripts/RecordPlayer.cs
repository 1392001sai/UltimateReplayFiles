using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Replay_State
{
    none, Recording, play, pause
}
public class RecordPlayer : MonoBehaviour
{
    public static Replay_State replay_state;
    public static bool SliderControl = false;
    public static bool TimeMovesForward = true;
    public int MaxLength;
    float ReplaySpeed = 1;
    public float MaxReplaySpeed;
    public float MinReplaySpeed;
    public float deltaReplaySpeed;
    [HideInInspector]
    public int curFrame = -1;
    [HideInInspector]
    public int sliderFrame = -1;
    [HideInInspector]
    public int length = 0;
    public static Action TimeDirectionChange;
    public static Action ReplayComplete;
    public static Action ResetLength;
    public static Action PlaybackSpeedMax;
    public static Action PlaybackSpeedMin;
    public static Action PlaybackSpeedInRange;
    public static Action SaveRecordings;

    private void Start()
    {
        FindObjectOfType<Recording>().ReplayCompleteChecker = true;
        
        replay_state = Replay_State.none;
        Debug.Log(replay_state);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartRecording()
    {
        if (replay_state != Replay_State.Recording)
        {
            length = 0;
            curFrame = -1;
            ReplaySpeed = 1;
            ResetLength();
            Time.timeScale = ReplaySpeed;
            replay_state = Replay_State.Recording;
            Debug.Log(replay_state);
        }
        else
        {
            replay_state = Replay_State.none;
            Debug.Log(replay_state);
            SaveRecordings();
        }
    }
    public void PlayRecording()
    {
        if (replay_state != Replay_State.play)
        {
            replay_state = Replay_State.play;
            Time.timeScale = ReplaySpeed;
            Debug.Log(replay_state);
        }
    }
    public void PauseRecording()
    {
        if (replay_state != Replay_State.pause)
        {
            replay_state = Replay_State.pause;
            Debug.Log(replay_state);
        }
    }

    public void StopRecording()
    {
        curFrame = 0;
        replay_state = Replay_State.none;
        Debug.Log(replay_state);
    }

    

    public void SpeedUpPlayback()
    {
        if (ReplaySpeed == MinReplaySpeed)
        {
            PlaybackSpeedInRange();
        }
        if (ReplaySpeed < MaxReplaySpeed)
        {
            ReplaySpeed += deltaReplaySpeed;
            ReplaySpeed = Mathf.Round(ReplaySpeed * (1 / deltaReplaySpeed)) * deltaReplaySpeed;
            if (ReplaySpeed == MaxReplaySpeed)
            {
                PlaybackSpeedMax();
            }
            if (replay_state != Replay_State.pause)
            {
                Time.timeScale = ReplaySpeed;
            }
        }
    }
    public void SlowDownPlayback()
    {
        if (ReplaySpeed == MaxReplaySpeed)
        {
            PlaybackSpeedInRange();
        }
        if (ReplaySpeed > MinReplaySpeed)
        {
            ReplaySpeed -= deltaReplaySpeed;
            ReplaySpeed = Mathf.Round(ReplaySpeed * (1 / deltaReplaySpeed)) * deltaReplaySpeed;
            if (ReplaySpeed == MinReplaySpeed)
            {
                PlaybackSpeedMin();
            }
            if (replay_state != Replay_State.pause)
            {
                Time.timeScale = ReplaySpeed;
            }
        }
    }

    public void SliderControlOn()
    {
        SliderControl = true;
    }
    public void SliderControlOff()
    {
        SliderControl = false;
    }

    public void MakeTimeMoveForward()
    {
        TimeMovesForward = true;
        TimeDirectionChange();
    }

    public void MakeTimeMoveBackward()
    {
        TimeMovesForward = false;
        TimeDirectionChange();
    }

}


