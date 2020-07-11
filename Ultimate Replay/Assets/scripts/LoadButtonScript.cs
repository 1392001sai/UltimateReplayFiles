using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonScript : MonoBehaviour
{
    public Button ReplayButton;
    public Button SaveButton;

    private void Start()
    {
        SaveSystem.LoadSuccess += OnLoadSuccess;
    }

    void OnLoadSuccess()
    {
        ReplayButton.interactable = true;
        SaveButton.interactable = false;
    }
}
