using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(2,4)]
    public string[] lines;
    public string speakerName;
    public bool hasChoice;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public DialogueNode nextNode;

}

[System.Serializable]
public class DialogueChoice
{
    public string label;
    public DialogueNode nextNode;
}