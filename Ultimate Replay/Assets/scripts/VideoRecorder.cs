#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEditor;
using System;

public class VideoRecorder : MonoBehaviour
{
    RecorderController recorderController;
    MovieRecorderSettings movieRecorderSettings;
    public static Action VideoRecordingStarted;
    string OutputFilePath = "";
    public string VideoCommonName = "Video.mp4";

    private void Start()
    {
        movieRecorderSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        RecorderControllerSettings recorderControllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        recorderController = new RecorderController(recorderControllerSettings);
        movieRecorderSettings.name = "My Video Recorder";
        movieRecorderSettings.Enabled = true;
        movieRecorderSettings.VideoBitRateMode = VideoBitrateMode.High;

        movieRecorderSettings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = Screen.width,
            OutputHeight = Screen.height
        };

        movieRecorderSettings.AudioInputSettings.PreserveAudio = true;
        movieRecorderSettings.FrameRate = 30;
        movieRecorderSettings.CapFrameRate = true;

        recorderControllerSettings.AddRecorderSettings(movieRecorderSettings);
        recorderControllerSettings.SetRecordModeToManual();

        RecorderOptions.VerboseMode = false;
        recorderController.PrepareRecording();
        RecordPlayer.ReplayComplete += VideoRecording;
        
    }

    public void VideoRecording()
    {
        if (recorderController.IsRecording() == false)
        {
            OutputFilePath = EditorUtility.SaveFilePanel("Save Video as mp4", "", VideoCommonName, "mp4");
            if (OutputFilePath != "")
            {
                movieRecorderSettings.OutputFile = OutputFilePath;
                recorderController.StartRecording();
                if (VideoRecordingStarted != null)
                {
                    VideoRecordingStarted();
                }
            }
        }
        else
        {
            recorderController.StopRecording();
        }
    }
}
#endif