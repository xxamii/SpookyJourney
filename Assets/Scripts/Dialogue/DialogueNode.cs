using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue node")]
public class DialogueNode : ScriptableObject
{
    [SerializeField] private DialogueLine _line;
    [SerializeField] private DialogueChoice[] _choices;

    public DialogueLine Line => _line;
    public DialogueChoice[] Choices => _choices;
}
