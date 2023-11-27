using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speaker;
    [TextArea(3, 10)] // This attribute allows for multi-line text in the inspector.
    public string dialogue;
}
