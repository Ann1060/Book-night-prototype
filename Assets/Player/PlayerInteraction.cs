using System.Collections.Generic;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    public static bool isInventoryOpen;
    public static bool isDialogueOpen;

    public IInteractable currentEObject;

    public IInteractable closestF;
    public IInteractable closestE;

    private IInteractable lastF;
    private IInteractable lastE;

    private List<IInteractable> nearbyFObjects = new List<IInteractable>();
    private List<IInteractable> nearbyEObjects = new List<IInteractable>();

    private Vector2 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, lastPosition) > 0.1f)
        {
            closestF = GetNearestF();
            closestE = GetNearestE();
            UpdatePrompts();
            currentEObject = closestE;
            lastPosition = transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        if (other.GetComponent<PickupItem>() != null ||
            other.GetComponent<InfoObject>() != null ||
            other.GetComponent<NPC>() != null)
        {
            if (!nearbyFObjects.Contains(interactable))
            {
                nearbyFObjects.Add(interactable);
                closestF = GetNearestF();
            }
        }
        if (other.GetComponent<UsableTarget>() != null)
        {
            if (!nearbyEObjects.Contains(interactable))
            {
                nearbyEObjects.Add(interactable);
                closestE = GetNearestE();
                currentEObject = closestE;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        if (nearbyFObjects.Contains(interactable))
        {
            nearbyFObjects.Remove(interactable);
            closestF = GetNearestF();
        }
        if (nearbyEObjects.Contains(interactable))
        {
            nearbyEObjects.Remove(interactable);
            closestE = GetNearestE();
            currentEObject = closestE;
        }
    }
    private IInteractable GetNearestF()
    {
        if (nearbyFObjects.Count == 0) return null;
        IInteractable closest = null;
        float minSqrDistance = float.MaxValue;
        Vector2 playerPos = transform.position;
        foreach (IInteractable obj in nearbyFObjects)
        {
            MonoBehaviour mb = obj as MonoBehaviour;
            if (mb == null) continue;
            float sqrDistance = (playerPos - (Vector2)mb.transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closest = obj;
            }
        }
        return closest;
    }
    private IInteractable GetNearestE()
    {
        if (nearbyEObjects.Count == 0) return null;

        IInteractable closest = null;
        float minSqrDistance = float.MaxValue;
        Vector2 playerPos = transform.position;

        foreach (IInteractable obj in nearbyEObjects)
        {
            MonoBehaviour mb = obj as MonoBehaviour;
            if (mb == null) continue;
            float sqrDistance = (playerPos - (Vector2)mb.transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closest = obj;
            }
        }
        return closest;
    }
    private void ShowPrompt(IInteractable interactable, string key)
    {
        MonoBehaviour mb = interactable as MonoBehaviour;
        if (mb == null)
            return;

        PromptController prompt = mb.GetComponent<PromptController>();

        if (prompt != null)
        {
            prompt.Show(key);
        }
    }
    private void HidePrompt(IInteractable interactable)
    {
        MonoBehaviour mb = interactable as MonoBehaviour;
        if (mb == null)
            return;

        PromptController prompt = mb.GetComponent<PromptController>();
        if (prompt != null)
        {
            prompt.Hide();
        }
    }
    private void UpdatePrompts()
    {
        if (lastF != null && lastF != closestF)
        {
            HidePrompt(lastF);
        }
        if (lastE != null && lastE != closestE)
        {
            HidePrompt(lastE);
        }
        if (closestF != null)
        {
            ShowPrompt(closestF, "F");
        }
        if (closestE != null)
        {
            ShowPrompt(closestE, "E");
        }
        lastF = closestF;
        lastE = closestE;
    }
}