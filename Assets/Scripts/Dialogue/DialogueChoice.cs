using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue choice")]
public class DialogueChoice : ScriptableObject
{
    [SerializeField] private string _text;
    [SerializeField] private DialogueNode _nextNode;
    [SerializeField] private bool _transitionScene;

    public string Text => _text;
    public DialogueNode NextNode => _nextNode;
    public bool TransitionScene => _transitionScene;
}
