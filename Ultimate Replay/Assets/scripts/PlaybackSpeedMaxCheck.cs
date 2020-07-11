using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackSpeedMaxCheck : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        RecordPlayer.PlaybackSpeedMax += PlaybackSpeedMax;
        RecordPlayer.PlaybackSpeedInRange += PlaybackSpeedInRange;
    }

    void PlaybackSpeedMax()
    {
        button.interactable = false;
    }
    void PlaybackSpeedInRange()
    {
        button.interactable = true;
    }

}
