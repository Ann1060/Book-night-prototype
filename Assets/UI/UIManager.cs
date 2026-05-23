using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [HideInInspector] public bool activePanelEnd;
    [Header("Inventory UI")]
    [SerializeField] private GameObject inventoryPanel;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [Header("End Panel")]
    [SerializeField] private GameObject panelEnd;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        PlayerInteraction.isInventoryOpen = UIManager.Instance.IsInventoryOpen();
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        PlayerInteraction.isDialogueOpen = UIManager.Instance.IsDialogueOpen();
        if (panelEnd!=null)
        {
            panelEnd.SetActive(false);
            activePanelEnd = false;
        }
    }
    public void ShowInventory()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(true);
        AudioEvents.OnAudioEvent?.Invoke(GameAudioEvent.InventoryOpen);
        PlayerInteraction.isInventoryOpen = UIManager.Instance.IsInventoryOpen();
    }
    public void HideInventory()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        AudioEvents.OnAudioEvent?.Invoke(GameAudioEvent.InventoryOpen);
        PlayerInteraction.isInventoryOpen = UIManager.Instance.IsInventoryOpen();
    }
    public bool IsInventoryOpen()
    {
        return inventoryPanel != null && inventoryPanel.activeSelf;
    }
    public void ShowDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        PlayerInteraction.isDialogueOpen = UIManager.Instance.IsDialogueOpen();
    }
    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        PlayerInteraction.isDialogueOpen = UIManager.Instance.IsDialogueOpen();
    }
    public bool IsDialogueOpen()
    {
        return dialoguePanel != null && dialoguePanel.activeSelf;
    }
    public void ShowPanelEnd()
    {
        panelEnd.SetActive(true);
        activePanelEnd = true;
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        panelEnd.SetActive(false);
        activePanelEnd = false;
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        activePanelEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        activePanelEnd = false;
        Application.Quit();
    }
}