using UnityEngine;
using UnityEngine.UI;

public class SanityManager : MonoBehaviour
{
    public int maxSanity = 100;
    private int currentSanity;
    public float sanityDecreaseRate = 1f;
    public float sanityRecoveryRate = 0.5f; // Recovery rate when eyes are closed

    public AudioSource heartbeatAudioSource; // Assign this in the Inspector
    public Image vignetteImage; // Assign this in the Inspector

    public float normalHeartbeatPitch = 0.5f; // Normal pitch when sanity is above 75
    public float increasedHeartbeatPitch = 1.0f; // Slightly increased pitch when sanity is 50 or lower
    public float highHeartbeatPitch = 1.5f; // Further increased pitch when sanity is 25 or lower
    public float maxHeartbeatPitch = 2.0f; // Maximum pitch when sanity is 0

    public float normalHeartbeatVolume = 0.5f; // Normal volume when sanity is above 75
    public float increasedHeartbeatVolume = 0.7f; // Slightly increased volume when sanity is 50 or lower
    public float highHeartbeatVolume = 0.9f; // Further increased volume when sanity is 25 or lower
    public float maxHeartbeatVolume = 1.0f; // Maximum volume when sanity is 0

    private bool eyesClosed = false;

    void Start()
    {
        // Initialize sanity
        currentSanity = maxSanity;

        // Ensure the audio source is initially set
        if (heartbeatAudioSource != null)
        {
            heartbeatAudioSource.volume = normalHeartbeatVolume; // Start with normal volume
            heartbeatAudioSource.pitch = normalHeartbeatPitch; // Start with normal pitch
            heartbeatAudioSource.loop = true; // Loop the heartbeat sound
            heartbeatAudioSource.Play(); // Start playing the heartbeat sound
        }
        else
        {
            Debug.LogError("Heartbeat AudioSource is not assigned!");
        }

        // Ensure the vignette is initially invisible
        if (vignetteImage != null)
        {
            vignetteImage.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            Debug.LogError("Vignette Image is not assigned!");
        }
    }

    void Update()
    {
        // Check if eyes are closed
        eyesClosed = Input.GetKey(KeyCode.Space);

        // Decrease sanity over time if eyes are open
        if (!eyesClosed)
        {
            currentSanity = Mathf.Max(0, currentSanity - (int)(sanityDecreaseRate * Time.deltaTime));
        }
        else
        {
            // Recover sanity over time if eyes are closed
            currentSanity = Mathf.Min(maxSanity, currentSanity + (int)(sanityRecoveryRate * Time.deltaTime));
        }

        // Update effects based on sanity
        UpdateEffectsBasedOnSanity();

        // Check for game over condition
        if (currentSanity <= 0)
        {
            GameOver();
        }
    }

    void UpdateEffectsBasedOnSanity()
    {
        float sanityPercent = (float)currentSanity / maxSanity;

        if (heartbeatAudioSource != null)
        {
            if (currentSanity > 75)
            {
                heartbeatAudioSource.volume = normalHeartbeatVolume;
                heartbeatAudioSource.pitch = normalHeartbeatPitch;
            }
            else if (currentSanity <= 75 && currentSanity > 50)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(normalHeartbeatVolume, increasedHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(normalHeartbeatPitch, increasedHeartbeatPitch, 1 - sanityPercent);
            }
            else if (currentSanity <= 50 && currentSanity > 25)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(increasedHeartbeatVolume, highHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(increasedHeartbeatPitch, highHeartbeatPitch, 1 - sanityPercent);
            }
            else if (currentSanity <= 25)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(highHeartbeatVolume, maxHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(highHeartbeatPitch, maxHeartbeatPitch, 1 - sanityPercent);
            }

            Debug.Log($"Heartbeat volume: {heartbeatAudioSource.volume}, pitch: {heartbeatAudioSource.pitch}");
        }

        if (vignetteImage != null)
        {
            if (currentSanity > 75)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, 0f);
            }
            else if (currentSanity <= 75 && currentSanity > 50)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 0.2f, 1 - sanityPercent));
            }
            else if (currentSanity <= 50 && currentSanity > 25)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0.2f, 0.5f, 1 - sanityPercent));
            }
            else if (currentSanity <= 25)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0.5f, 0.8f, 1 - sanityPercent));
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Implement game over logic here, e.g., load game over screen
        vignetteImage.color = new Color(0f, 0f, 0f, 1f); // Make the screen completely black
        // Optionally, you can also stop the heartbeat sound
        if (heartbeatAudioSource != null)
        {
            heartbeatAudioSource.Stop();
        }
    }

    public void ModifySanity(int amount)
    {
        currentSanity = Mathf.Clamp(currentSanity + amount, 0, maxSanity);
    }
}
