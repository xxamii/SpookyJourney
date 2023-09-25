using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueTextBox : Singleton<UIDialogueTextBox>
{
    [SerializeField] private DialogueNode _dialogue;
    [SerializeField] private Text _lineTextBox;
    [SerializeField] private Text _characterTextBox;
    [SerializeField] private RectTransform _buttonsTransform;
    [SerializeField] private UIDialogueButton _buttonPrefab;
    [SerializeField] private GameObject _panel;
    [SerializeField] private DialogueNode _winNode;
    [SerializeField] private DialogueNode _loseNode;

    private DialogueNode _currentNode;
    private bool _hasStarted;

    private void Start()
    {
        _currentNode = _dialogue;

        GameState.Instance.OnLose += ShowLose;
        GameState.Instance.OnWin += ShowWin;
    }

    private void OnDisable()
    {
        GameState.Instance.OnLose -= ShowLose;
        GameState.Instance.OnWin -= ShowWin;
    }

    public void StartDialogue()
    {
        if (!_hasStarted)
        {
            _panel.SetActive(true);
            ShowCurrentNode();
        }
    }

    private void ShowWin()
    {
        _panel.gameObject.SetActive(true);
        _currentNode = _winNode;
        ShowCurrentNode();
    }

    private void ShowLose()
    {
        _panel.gameObject.SetActive(true);
        _currentNode = _loseNode;
        ShowCurrentNode();
    }
    

    private void ShowCurrentNode()
    {
        _characterTextBox.text = _currentNode.Line.Character;
        _lineTextBox.text = _currentNode.Line.Text;
        SpawnButtons();
    }

    private void SpawnButtons()
    {
        for (int i = 0; i < _buttonsTransform.childCount; i++)
        {
            Destroy(_buttonsTransform.GetChild(i).gameObject);
        }

        int buttonIndex = 0;
        foreach (DialogueChoice choice in _currentNode.Choices)
        {
            UIDialogueButton button = Instantiate(_buttonPrefab, _buttonsTransform);
            button.Choice = choice;
            button.SetText();
            button.OnClick += ShowNextNode;

            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(buttonTransform.anchoredPosition.x + buttonTransform.sizeDelta.x * buttonIndex, buttonTransform.anchoredPosition.y);
            buttonIndex++;
        }
    }

    private void ShowNextNode(DialogueChoice choice)
    {
        _currentNode = choice.NextNode;

        if (choice.TransitionScene)
        {
            SceneTransitioner.Instance.StartTransitionNext();
        }
        else
        {
            ShowCurrentNode();
        }
    }
}
