using UnityEngine;
public class InfoObject : MonoBehaviour, IInteractable
{
    [SerializeField] public string itemName;
    [SerializeField] private uint dialogueIndex;
    private DialogPresenter _dialogPresenter;
    private DialogView _dialogView;
    void Start()
    {
        _dialogPresenter = FindObjectOfType<DialogPresenter>();
        _dialogView = FindObjectOfType<DialogView>();
    }
    public void Interact()
    {
        if (_dialogPresenter == null || _dialogView == null)
        {
            return;
        }
        _dialogPresenter.StartInfoObject(this);
    }
    public uint GetDialogueIndex()
    {
        return dialogueIndex;
    }
    public string GetPromptAction() => "Осмотреть";
    public string GetObjectName() => gameObject.name;
}