using UnityEngine;

[System.Serializable]
public class DialogueEntity
{
    public string speaker; 
    [TextArea(3, 10)] public string dialog; 
}