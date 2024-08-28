using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int totalObject;
    public int totalScore;
    public GameObject popup;
    public AudioSource audioSource;

    public bool hasWon = false;
    public AudioManager audioManager;

    private void Awake()
    {
        totalScore = 0;
        hasWon = false;
    }

    public ScoreManager()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update() => HandleWin();

    public void HandleWin()
    {
        if (totalObject == totalScore && !hasWon)
        {
            hasWon = true;
            popup.SetActive(true);
            // PlayAudio();
        }
    }

    public void AddScore()
    {
        totalScore++;
        Debug.Log("Score Added " + totalScore);
    }

    public void ResetScore()
    {
        totalScore = 0;
        hasWon = false;
    }

    public void PlayAudio()
    {
        var gameAudio = audioManager.audioSource;
        gameAudio.Stop();
        audioSource.Play();

        StartCoroutine(CheckIfAudioFinished(audioSource));
    }


    private IEnumerator CheckIfAudioFinished(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        var gameAudio = audioManager.audioSource;
        gameAudio.Play();

    }
}