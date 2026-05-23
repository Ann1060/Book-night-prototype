using UnityEngine;
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] public string npcName;
    [SerializeField] public uint dialogueIndex;
    [SerializeField] public bool hasDialogue;
    [SerializeField] private int npcId;
    private DialogPresenter _dialogPresenter;
    private DialogView _dialogView;
    void Start()
    {
        _dialogPresenter = FindObjectOfType<DialogPresenter>();
        _dialogView = FindObjectOfType<DialogView>();
    }
    public void Interact()
    {
        if (_dialogPresenter == null || _dialogView == null || hasDialogue == false)
        {
            return;
        }
        _dialogPresenter.StartDialogue(this);
    }
    public uint GetDialogueIndex()
    {
        return dialogueIndex;
    }
    public string GetPromptAction() => "Поговорить";
    public string GetObjectName() => npcName;
    public int GetNpcId() => npcId;
    public void SetDialogIndex(int id)
    {
        dialogueIndex = (uint)id;
    }
    public void SetHasDialog(bool _hasDialog)
    {
        hasDialogue = _hasDialog;
    }
}