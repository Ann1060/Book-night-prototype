using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
enum DialoguePresenterState
{
    Await,
    Talk,
    Choice,
    Finish
}
public class DialogPresenter : MonoBehaviour
{
    public event Action OnDialogueStart;

    [Header("Components")]
    [SerializeField] private DialogView _dialogueView;
    [SerializeField] private TextAsset[] _dialoguesArr;
    private uint _currentDialogue;
    private DialogNode _currentNode;

    [Header("Events")]
    [SerializeField] private UnityEvent[] OnDialogueFinished;

    [field: Header("States")]
    [field: SerializeField] public bool CanTalk { get; set; }
    [SerializeField] private DialoguePresenterState _state;

    private bool _messagePrinting;
    private List<DialogNode> _currentChoices;
    private void OnEnable()
    {
        _dialogueView.OnFinishMessage += StopPrinting;
    }
    private void OnDisable()
    {
        _dialogueView.OnFinishMessage -= StopPrinting;
    }
    private void StopPrinting()
    {
        _messagePrinting = false;
    }
    public void StartDialogue(NPC npc)
    {
        _currentChoices = null;
        if (_state == DialoguePresenterState.Finish || !CanTalk)
            return;

        _currentDialogue = npc.GetDialogueIndex();
        _currentNode = ParseDialogFile.GetDialogTree(_dialoguesArr[_currentDialogue]);

        _dialogueView.SetPresenter(this);
        OnDialogueStart?.Invoke();
        CanTalk = true;
        _dialogueView.StartDialogue(_currentNode.Message, _currentNode.Name);
        _state = DialoguePresenterState.Talk;
        _messagePrinting = true;
    }
    public void StartInfoObject(InfoObject obj)
    {
        _currentChoices = null;
        if (_state == DialoguePresenterState.Finish || !CanTalk)
            return;

        _currentDialogue = obj.GetDialogueIndex();
        _currentNode = ParseDialogFile.GetDialogTree(_dialoguesArr[_currentDialogue]);

        _dialogueView.SetPresenter(this);
        OnDialogueStart?.Invoke();
        CanTalk = true;
        _dialogueView.StartDialogue(_currentNode.Message, _currentNode.Name);
        _state = DialoguePresenterState.Talk;
        _messagePrinting = true;
    }
    public void NextMessage()
    {
        if (_state != DialoguePresenterState.Talk || _state == DialoguePresenterState.Choice)
            return;
        if (_messagePrinting)
        {
            _dialogueView.InterruptAnimation();
            return;
        }
        if (_currentNode.children.Count == 0)
        {
            FinishDialogue();
            return;
        }
        GoToChildMessage();
    }
    private void GoToChildMessage()
    {
        if (_currentNode.children.Count == 1)
        {
            _currentNode = _currentNode.children[0];
            _dialogueView.NextMessage(_currentNode.Message, _currentNode.Name);
            _messagePrinting = true;
            return;
        }

        List<string> shortNames = new List<string>();
        foreach (var child in _currentNode.children)
            shortNames.Add(child.ShortName);

        _currentChoices = _currentNode.children;
        _dialogueView.ActivateButtons(shortNames);
        _state = DialoguePresenterState.Choice;
    }
    private void FinishDialogue()
    {
        _currentChoices = null;
        _dialogueView.StopDialogue();
        if (_currentNode.ActionId != null)
            OnDialogueFinished[(int)_currentNode.ActionId].Invoke();

        _state = DialoguePresenterState.Await;
        _currentDialogue++;
        if (_currentDialogue > _dialoguesArr.Length - 1)
        {
            _state = DialoguePresenterState.Finish;
            _state = DialoguePresenterState.Await;
        }
    }
    public void SwitchBranch(int index)
    {;
        _currentNode = _currentChoices[index];
        _currentChoices = null;
        _state = DialoguePresenterState.Talk;
        _dialogueView.NextMessage(_currentNode.Message, _currentNode.Name);
        _messagePrinting = true;
    }
}