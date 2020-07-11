using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Windows.Forms;
//using GracesGames.SimpleFileBrowser.Scripts;
//using UnityEditor;
using System;

public class SaveSystem : MonoBehaviour
{
    public string RecordFileCommonName = "Recording.txt";
    public static Action LoadSuccess;
    RecordPlayer recordPlayer;
    SavedReplay savedReplay = new SavedReplay();
    SavedReplay loadReplay = new SavedReplay();
    OpenFileDialog openFileDialog = new OpenFileDialog();
    SaveFileDialog saveFileDialog = new SaveFileDialog();

    private void Start()
    {
        recordPlayer = FindObjectOfType<RecordPlayer>();
        saveFileDialog.InitialDirectory = "C:/";
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.FileName = RecordFileCommonName;
        saveFileDialog.Filter = "txt files (*.txt)|*.txt";
        openFileDialog.InitialDirectory = "C:/";
        openFileDialog.RestoreDirectory = true;
        openFileDialog.FileName = RecordFileCommonName;
        openFileDialog.Filter = "txt files (*.txt)|*.txt";
    }
    public void AddSavedRecording(Recording recording)
    {
        if (savedReplay.SavedRecordings.Count == 0)
        {
            savedReplay.length = recording.length;
        }
        savedReplay.SavedRecordings.Add(new SavedRecording(recording.gameObject.name, recording.Frames));

    }
    public void SaveReplay()
    {
        string SavedReplayJson = JsonUtility.ToJson(savedReplay);
        SaveFile(SavedReplayJson);
                
    }

    public void LoadReplay()
    {
        string savedReplayJson = LoadFile();
        if (savedReplayJson != null)
        {
            loadReplay = JsonUtility.FromJson<SavedReplay>(savedReplayJson);
         
            recordPlayer.length = loadReplay.length;
            foreach (SavedRecording savedRecording in loadReplay.SavedRecordings)
            {
                Recording recording = GameObject.Find(savedRecording.ObjectName).GetComponent<Recording>();
                recording.Frames = savedRecording.Frames;
                recording.length = loadReplay.length;
                recording.gotoFrame = 0;
            }
            LoadSuccess();
            
        }
    }


    void SaveFile(string savedReplayJson)
    {
        /*System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe", "/select");
        p.Start();
        //string FilePath = EditorUtility.SaveFilePanel("Save Replay as txt", "", RecordFileCommonName, "txt");
        return "";*/
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            Stream stream = saveFileDialog.OpenFile();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(savedReplayJson);
            streamWriter.Close();
            stream.Close();
        }
    }

    string LoadFile()
    {
        /*string FilePath = EditorUtility.OpenFilePanel("Select Reccording", "", "txt");
        return FilePath;*/
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string fileName = openFileDialog.FileName;
            return File.ReadAllText(fileName);
        }
        else
        {
            return null;
        }

    }
}
