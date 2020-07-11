#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    private void Start()
    {
        VideoRecorder.VideoRecordingStarted += OnVideoRecorderStarted;
    }

    void OnVideoRecorderStarted()
    {
        if (RecordPlayer.replay_state != Replay_State.play)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
#endif