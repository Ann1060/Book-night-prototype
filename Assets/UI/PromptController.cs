using UnityEngine;
public class PromptController : MonoBehaviour
{
    [SerializeField] private PromptUI promptPrefab;
    private PromptUI currentPrompt;
    private IInteractable interactable;
    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
        if (promptPrefab != null)
        {
            currentPrompt = Instantiate(promptPrefab,transform);
            currentPrompt.transform.localPosition = new Vector3(0f, 1f, -1f);
            currentPrompt.Hide();
        }
    }
    public void Show(string key)
    {
        if (currentPrompt == null || interactable == null)
            return;

        currentPrompt.SetText($"[{key}] {interactable.GetPromptAction()} {interactable.GetObjectName()}");
        currentPrompt.Show();
    }
    public void Hide()
    {
        if (currentPrompt == null)
            return;

        currentPrompt.Hide();
    }
}