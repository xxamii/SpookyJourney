using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    private void Update()
    {
        if (SceneTransitioner.Instance.FadedInThisFrame)
        {
            UIDialogueTextBox.Instance.StartDialogue();
        }
    }
}
