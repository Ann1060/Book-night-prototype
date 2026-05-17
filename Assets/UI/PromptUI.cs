using TMPro;
using UnityEngine;
public class PromptUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.forward = cam.transform.forward;
        }
    }
    public void SetText(string message)
    {
        text.text = message;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}