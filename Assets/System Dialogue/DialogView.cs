using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogView : MonoBehaviour
{
    public DialogPresenter _dialoguePresenter;
    public event Action OnFinishMessage;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    
    [Header("Animation")]
    [SerializeField] private float _typingSpeed;
    private TextAnimator _textAnimator;

    private int _selectedButtonIndex = 0;
    private int _activeButtonsCount = 0;
    private Color[] _originalColors;
    private float[] _originalAlphas;
    private void Awake()
    {
        _textAnimator = new TextAnimator(_messageText, this, _typingSpeed);
    }
    private void Start()
    {
        _originalColors = new Color[_buttons.Length];
        _originalAlphas = new float[_buttons.Length];
        for (int i = 0; i < _buttons.Length; i++)
        {
            Image img = _buttons[i].GetComponent<Image>();
            if (img != null)
            {
                _originalColors[i] = img.color;
                _originalAlphas[i] = img.color.a;
            }
        }
    }
    private void OnEnable()
    {
        _textAnimator.OnAnimationFinished += FinishMessage;
    }
    private void OnDisable()
    {
        _textAnimator.OnAnimationFinished -= FinishMessage;
    }
    public void SetPresenter(DialogPresenter presenter)
    {
        _dialoguePresenter = presenter;
    }
    public void StartDialogue(string message, string name)
    {
        UIManager.Instance.ShowDialogue();
        HideButtons();
        _nameText.text = name;
        NewMessage(message);
    }
    public void NextMessage(string message, string name)
    {
        _nameText.text = name;
        NewMessage(message);
    }
    private void NewMessage(string text)
    {
        _messageText.text = text;
        _textAnimator.StartAnimation(text);
    }
    public void InterruptAnimation()
    {
        _textAnimator.InterruptAnimation();
    }
    public void StopDialogue()
    {
        StartCoroutine(Wait());
        UIManager.Instance.HideDialogue();
    }

    public void HideButtons()
    {
        foreach (var button in _buttons)
            button.SetActive(false);
        _messageText.enabled = true;
    }

    public void ActivateButtons(List<string> shortNames)
    {
        _messageText.enabled = false;
        _activeButtonsCount = shortNames.Count;
        _selectedButtonIndex = 0;
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i < shortNames.Count)
            {
                _buttons[i].SetActive(true);
                _buttonsText[i].text = shortNames[i];
                SetButtonHighlight(i, i == 0);
            }
            else
            {
                _buttons[i].SetActive(false);
            }
        }
    }
    public void SelectPreviousOption()
    {
        if (_activeButtonsCount == 0) return;
        
        SetButtonHighlight(_selectedButtonIndex, false);
        _selectedButtonIndex--;
        if (_selectedButtonIndex < 0)
            _selectedButtonIndex = _activeButtonsCount - 1;

        SetButtonHighlight(_selectedButtonIndex, true);
    }

    public void SelectNextOption()
    {
        if (_activeButtonsCount == 0) return;

        SetButtonHighlight(_selectedButtonIndex, false);
        _selectedButtonIndex++;
        if (_selectedButtonIndex >= _activeButtonsCount)
            _selectedButtonIndex = 0;

        SetButtonHighlight(_selectedButtonIndex, true);
    }

    public void SelectCurrentOption()
    {
        if (_activeButtonsCount > 0)
        {
            ClickOnButtonChoice(_selectedButtonIndex);
        }
    }

    private void SetButtonHighlight(int index, bool highlight)
    {
        if (index < 0 || index >= _buttons.Length) return;

        Image img = _buttons[index].GetComponent<Image>();
        if (img != null)
        {
            if (highlight)
            {
                Color yellow = Color.yellow;
                yellow.a = _originalAlphas[index];
                img.color = yellow;
            }
            else
            {
                img.color = _originalColors[index];
            }
        }
    }
    public void ClickOnButtonChoice(int indexButton)
    {
        HideButtons();
        _dialoguePresenter.SwitchBranch(indexButton);
    }
    private void FinishMessage()
    {
        OnFinishMessage?.Invoke();
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        HideDialog();
    }

    private void HideDialog()
    {
        _textAnimator.HideText();
        _nameText.text = "";
    }
}