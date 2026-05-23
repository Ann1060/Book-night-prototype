using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip inventoryOpen;
    public AudioClip itemPickup;
    public AudioClip typingClip;

    private bool typingLoopActive;
    private float nextTypingTime;

    void OnEnable()
    {
        AudioEvents.OnAudioEvent += HandleAudioEvent;
    }
    void OnDisable()
    {
        AudioEvents.OnAudioEvent -= HandleAudioEvent;
    }
    void Update()
    {
        if (typingLoopActive && Time.time > nextTypingTime)
        {
            sfxSource.PlayOneShot(typingClip);
            nextTypingTime = Time.time + Random.Range(0.03f, 0.12f);
        }
    }
    void HandleAudioEvent(GameAudioEvent audioEvent)
    {
        switch (audioEvent)
        {
            case GameAudioEvent.InventoryOpen:
                sfxSource.PlayOneShot(inventoryOpen);
                break;

            case GameAudioEvent.ItemPickup:
                sfxSource.PlayOneShot(itemPickup);
                break;

            case GameAudioEvent.TextTypingStart:
                typingLoopActive = true;
                break;

            case GameAudioEvent.TextTypingStop:
                typingLoopActive = false;
                break;
        }
    }
}