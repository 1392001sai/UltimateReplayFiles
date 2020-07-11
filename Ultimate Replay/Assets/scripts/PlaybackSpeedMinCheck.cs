using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackSpeedMinCheck : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        RecordPlayer.PlaybackSpeedMin += PlaybackSpeedMin;
        RecordPlayer.PlaybackSpeedInRange += PlaybackSpeedInRange;
    }

    void PlaybackSpeedInRange()
    {
        button.interactable = true;
    }
    void PlaybackSpeedMin()
    {
        button.interactable = false;
    }
}
