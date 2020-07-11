using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationParameter
{
    public string Parameter_Name;
    public AnimatorControllerParameterType Parameter_Type;
    public float Float_Parameter;
    public int Int_Parameter;
    public bool Bool_Parameter;

    public AnimationParameter(string name, bool parameter, AnimatorControllerParameterType type)
    {
        Parameter_Name = name;
        Parameter_Type = type;
        Bool_Parameter = parameter;
    }
    public AnimationParameter(string name, float parameter, AnimatorControllerParameterType type)
    {
        Parameter_Name = name;
        Parameter_Type = type;
        Float_Parameter = parameter;
    }
    public AnimationParameter(string name, int parameter, AnimatorControllerParameterType type)
    {
        Parameter_Name = name;
        Parameter_Type = type;
        Int_Parameter = parameter;
    }
}
