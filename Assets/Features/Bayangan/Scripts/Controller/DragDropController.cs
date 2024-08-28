using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private GameObject objectTarget;
    private AudioSource audioSource;
    private AudioManager audioManager;

    public float dragDistance = 1.0f;
    public bool isLocked = false;
    Vector2 initialPosition;
    ScoreManager scoreManager;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioManager = GameObject.FindAnyObjectByType<AudioManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        initialPosition = gameObject.transform.position;

        AddTrigger();
    }

    private void AddTrigger()
    {
        var trigger = gameObject.AddComponent<EventTrigger>();

        trigger.triggers.Add(CreateEntry(EventTriggerType.BeginDrag, (data) => StartDrag()));
        trigger.triggers.Add(CreateEntry(EventTriggerType.Drag, (data) => OnDrag()));
        trigger.triggers.Add(CreateEntry(EventTriggerType.EndDrag, (data) => OnDrop()));
    }

    private EventTrigger.Entry CreateEntry(EventTriggerType eventID, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener(callback);
        return entry;
    }

    public void StartDrag()
    {
        gameObject.transform.SetAsFirstSibling();
        PlayAudio(audioManager.audioDrag);
    }

    public void OnDrag()
    {
        if (isLocked) return;
        gameObject.transform.position = Input.mousePosition;
    }

    public void OnDrop()
    {

        if (isLocked) return;
        float distance = Vector2.Distance(gameObject.transform.position, objectTarget.transform.position);
        bool isCloseToDrop = distance < dragDistance;

        isLocked = isCloseToDrop;
        gameObject.transform.position = isCloseToDrop ? objectTarget.transform.position : initialPosition;

        if (isCloseToDrop)
        {
            scoreManager.AddScore();
        }

        PlayAudio(audioManager.audioDrop);
    }

    void PlayAudio(AudioClip clip)
    {
        if (audioSource)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}