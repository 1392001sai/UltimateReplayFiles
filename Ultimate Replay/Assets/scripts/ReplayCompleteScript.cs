using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayCompleteScript : MonoBehaviour
{
    void Start()
    {
        RecordPlayer.ReplayComplete += ResetPlayer;
    }

    void ResetPlayer()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
