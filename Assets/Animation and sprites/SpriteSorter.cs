using UnityEngine;
public class SpriteSorter : MonoBehaviour
{
    private int sortingOrderBase = 0;
    private Renderer renderer;
    public float offset = 0;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    private void LateUpdate()
    {
        renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);
    }
}