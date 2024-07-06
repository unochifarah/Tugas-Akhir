using UnityEngine;
using System.Collections;

public class PrayerRecitation : MonoBehaviour
{
    public AudioClip[] prayerClips; // Array of prayer audio clips
    private AudioSource audioSource;
    private bool isRecitingPrayer = false;
    private Coroutine fadeOutCoroutine;

    public float fadeOutDuration = 1f; // Duration for the fade-out effect

    void Start()
    {
        // Add an AudioSource component to the game object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRecitingPrayer)
        {
            StartRecitingPrayer();
        }
        if (Input.GetKeyUp(KeyCode.Q) && isRecitingPrayer)
        {
            StopRecitingPrayer();
        }
    }

    void StartRecitingPrayer()
    {
        if (prayerClips.Length > 0)
        {
            // Pick a random prayer clip to play
            int randomIndex = Random.Range(0, prayerClips.Length);
            AudioClip prayerClip = prayerClips[randomIndex];

            // Play the prayer clip
            audioSource.clip = prayerClip;
            audioSource.Play();

            // Set isRecitingPrayer to true
            isRecitingPrayer = true;
        }
        else
        {
            Debug.LogWarning("No prayer clips assigned to the PrayerRecitation script.");
        }
    }

    void StopRecitingPrayer()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutDuration));
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        isRecitingPrayer = false;
    }
}
