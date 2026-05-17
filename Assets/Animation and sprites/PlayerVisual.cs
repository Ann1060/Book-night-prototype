using UnityEngine;
public class PlayerVisual : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.down;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!UIManager.Instance.IsInventoryOpen() && !UIManager.Instance.IsDialogueOpen())
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 move = new Vector2(x, y);
            bool isMoving = move != Vector2.zero;
            if (isMoving)
            {
                move.Normalize();
                lastMoveDir = move;
            }

            animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                animator.SetFloat("MoveX", move.x);
                animator.SetFloat("MoveY", move.y);
            }
            else
            {
                animator.SetFloat("MoveX", lastMoveDir.x);
                animator.SetFloat("MoveY", lastMoveDir.y);
            }
        }
    }
}