using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedRecording
{
    public string ObjectName;
    public List<Frame> Frames;
    public SavedRecording(string objName, List<Frame> Fr)
    {
        ObjectName = objName;
        Frames = Fr;
    }
}
[System.Serializable]
public class SavedReplay 
{
    public List<SavedRecording> SavedRecordings;
    public int length;
    public SavedReplay()
    {
        SavedRecordings = new List<SavedRecording>();
    }
}


