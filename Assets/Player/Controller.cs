using UnityEngine;
public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }
    [SerializeField] private int speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetMove(Vector2 vector2)
    {
        moveInput = vector2;
    }
    private void FixedUpdate()
    {
        if (PlayerInteraction.isInventoryOpen || PlayerInteraction.isDialogueOpen)
        {
            rb.velocity = Vector2.zero;
            moveInput = Vector2.zero;
            return;
        }
        rb.velocity = moveInput*speed;
    }
}