using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue Data", order = 0)]
public class DialogueData : ScriptableObject
{
    public DialogueEntity[] dialogs;
}
