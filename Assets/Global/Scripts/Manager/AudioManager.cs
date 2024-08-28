using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip audioClip;
    public AudioSource audioSource;

    [Header("Audio Drag and Drop")]
    public AudioClip audioDrag;
    public AudioClip audioDrop;

    private void Awake()
    {
        InitGO();
        PlayBGM();
    }

    private void InitGO()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void PlayBGM()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
}