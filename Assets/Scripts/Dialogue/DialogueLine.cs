using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue line")]
public class DialogueLine : ScriptableObject
{
    [SerializeField] private string _character;
    [SerializeField] private string _text;

    public string Text => _text;
    public string Character => _character;
}
