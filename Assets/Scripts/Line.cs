using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Line", menuName = "Line")]
public class Line : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] string text;


    public string Text
    {
        get { return text; }
    }
}
