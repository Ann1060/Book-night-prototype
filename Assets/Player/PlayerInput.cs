using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    //public static PlayerInput Instance;
    [SerializeField] private Controller controller;
    [SerializeField] private PlayerInteraction interaction;
    [SerializeField] private DialogView dialogView;
    public Vector2 moveInput;
    private void Awake()
    {
        //Instance = this;
        if (!UIManager.Instance.activePanelEnd)
            Cursor.visible = false;
    }
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controller.SetMove(moveInput);
        if (PlayerInteraction.isDialogueOpen)
        {
            if (Input.GetKeyDown(KeyCode.W))
                dialogView.SelectPreviousOption();
            if (Input.GetKeyDown(KeyCode.S))
                dialogView.SelectNextOption();

            if (Input.GetKeyDown(KeyCode.F))
                dialogView.SelectCurrentOption();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dialogView._dialoguePresenter != null)
                    dialogView._dialoguePresenter.NextMessage();
            }
        }
        if (!PlayerInteraction.isDialogueOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!UIManager.Instance.IsInventoryOpen())
                {
                    UIManager.Instance.ShowInventory();
                }
                else
                {
                    UIManager.Instance.HideInventory();
                }
            }
            if (!PlayerInteraction.isInventoryOpen && !PlayerInteraction.isDialogueOpen && interaction.closestF != null 
                && Input.GetKeyDown(KeyCode.F))
            {
                if (interaction.closestF is NPC || interaction.closestF is InfoObject)
                {
                    interaction.closestF.Interact();
                }
                if (interaction.closestF is PickupItem)
                {
                    interaction.closestF.Interact();
                }
            }
            if (!PlayerInteraction.isInventoryOpen && !PlayerInteraction.isDialogueOpen && interaction.closestE != null && Input.GetKeyDown(KeyCode.E))
            {
                interaction.closestE.Interact();
            }
        }
        if (UIManager.Instance.activePanelEnd)
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            UIManager.Instance.ShowPanelEnd();
        }
    }
    public Vector2 MoveInput()
    {
        return moveInput;
    }
}