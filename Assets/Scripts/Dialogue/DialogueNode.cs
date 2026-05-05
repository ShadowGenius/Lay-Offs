using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(2,4)]
    public string[] lines;

}
