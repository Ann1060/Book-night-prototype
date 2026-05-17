using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class TextAnimator
{
    public event Action OnAnimationFinished;
    private TextMeshProUGUI _message;
    private float _typingSpeed;
    private MonoBehaviour _coroutineOwner;
    private bool _isAnimating;
    private string _currentText;
    private Coroutine _typingCoroutine;
    public bool IsAnimating => _isAnimating;
    public TextAnimator(TextMeshProUGUI messageText, MonoBehaviour coroutineOwner, float typingSpeed = 0.03f)
    {
        _message = messageText;
        _typingSpeed = typingSpeed;
        _coroutineOwner = coroutineOwner;
    }
    public void StartAnimation(string text)
    {
        _currentText = text;

        if (_typingCoroutine != null)
        {
            _coroutineOwner.StopCoroutine(_typingCoroutine);
        }
        _typingCoroutine = _coroutineOwner.StartCoroutine(TypeText(text));
    }

    public void InterruptAnimation()
    {
        if (_isAnimating)
        {
            if (_typingCoroutine != null)
            {
                _coroutineOwner.StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }
            _message.text = _currentText;
            _isAnimating = false;
            OnAnimationFinished?.Invoke();
        }
    }
    public void HideText()
    {
        _message.text = "";
        _isAnimating = false;
    }

    private IEnumerator TypeText(string fullText)
    {
        _isAnimating = true;
        _message.text = "";

        foreach (char c in fullText)
        {
            _message.text += c;
            yield return new WaitForSeconds(_typingSpeed);
        }

        _isAnimating = false;
        _typingCoroutine = null;
        OnAnimationFinished?.Invoke();
    }
}