using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayblackSliderScript : MonoBehaviour
{
    float fillAmount;
    RecordPlayer recordPlayer;
    Slider slider;

    private void Start()
    {
        recordPlayer = FindObjectOfType<RecordPlayer>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (RecordPlayer.SliderControl == false)
        {
            if (RecordPlayer.replay_state == Replay_State.play || RecordPlayer.replay_state == Replay_State.pause)
            {
                UpdateSlider();
            }
            else
            {
                slider.value = 0;
            }
        }
        else
        {
            UpdateSliderFrame();
        }
    }

    void UpdateSlider()
    {
        if (recordPlayer.length != 0)
        {
            fillAmount = (recordPlayer.curFrame * 1.0f) / recordPlayer.length;
            slider.value = fillAmount;
        }
    }

    void UpdateSliderFrame()
    {
        recordPlayer.sliderFrame = Mathf.RoundToInt(slider.value * recordPlayer.length);
    }
}
