using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RecordButtonScript : MonoBehaviour
{
    public Button ReplayButton;
    public Button SaveButton;
    public Button LoadButton;
    public GameObject RecordingImage;
    public GameObject NotRecordingImage;
    RecordPlayer recordPlayer;
     
    private void Start()
    {
        recordPlayer = FindObjectOfType<RecordPlayer>();
    }
    public void OnClick()
    {
        if (RecordPlayer.replay_state != Replay_State.Recording)
        {
            ReplayButton.interactable = false;
            SaveButton.interactable = false;
            LoadButton.interactable = false;
            RecordingImage.SetActive(true);
            NotRecordingImage.SetActive(false);
        }
        else
        {
            
            ReplayButton.interactable = true;
            SaveButton.interactable = true;
            LoadButton.interactable = true;
            RecordingImage.SetActive(false);
            NotRecordingImage.SetActive(true);
        }
        recordPlayer.StartRecording();
    }
}
