using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Frame
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public int AnimatorStateNameHash;
    public List<AnimationParameter> AnimationParameters;
    public float normalisedTime;

    public Frame(Transform tr)
    {
        position = tr.position;
        rotation = tr.rotation;
        scale = tr.localScale;
    }

    public Frame(Transform tr, List<AnimationParameter> parameters, int StateNameHash, float ntime)
    {
        position = tr.position;
        rotation = tr.rotation;
        scale = tr.localScale;
        AnimationParameters = parameters;
        AnimatorStateNameHash = StateNameHash;
        normalisedTime = ntime;
    }
}
