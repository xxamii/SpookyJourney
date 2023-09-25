using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueButton : MonoBehaviour
{
    public Action<DialogueChoice> OnClick;
    public DialogueChoice Choice { get; set; }

    [SerializeField] private Text _buttonText;

    public void Click()
    {
        OnClick?.Invoke(Choice);
    }

    public void SetText()
    {
        _buttonText.text = Choice.Text;
    }
}
