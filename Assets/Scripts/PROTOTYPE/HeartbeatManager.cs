using UnityEngine;

public class HeartbeatManager : MonoBehaviour
{
    public AudioSource heartbeatAudioSource;
    public float normalHeartbeatPitch = 0.5f;
    public float increasedHeartbeatPitch = 1.0f;
    public float highHeartbeatPitch = 1.5f;
    public float maxHeartbeatPitch = 2.0f;

    public float normalHeartbeatVolume = 0.5f;
    public float increasedHeartbeatVolume = 0.7f;
    public float highHeartbeatVolume = 0.9f;
    public float maxHeartbeatVolume = 1.0f;

    void Start()
    {
        if (heartbeatAudioSource != null)
        {
            heartbeatAudioSource.volume = normalHeartbeatVolume;
            heartbeatAudioSource.pitch = normalHeartbeatPitch;
            heartbeatAudioSource.loop = true;
            heartbeatAudioSource.Play();
        }
        else
        {
            Debug.LogError("Heartbeat AudioSource is not assigned!");
        }
    }

    public void UpdateHeartbeat(float sanityPercent)
    {
        if (heartbeatAudioSource != null)
        {
            if (sanityPercent > 0.75f)
            {
                heartbeatAudioSource.volume = normalHeartbeatVolume;
                heartbeatAudioSource.pitch = normalHeartbeatPitch;
            }
            else if (sanityPercent <= 0.75f && sanityPercent > 0.5f)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(normalHeartbeatVolume, increasedHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(normalHeartbeatPitch, increasedHeartbeatPitch, 1 - sanityPercent);
            }
            else if (sanityPercent <= 0.5f && sanityPercent > 0.25f)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(increasedHeartbeatVolume, highHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(increasedHeartbeatPitch, highHeartbeatPitch, 1 - sanityPercent);
            }
            else if (sanityPercent <= 0.25f)
            {
                heartbeatAudioSource.volume = Mathf.Lerp(highHeartbeatVolume, maxHeartbeatVolume, 1 - sanityPercent);
                heartbeatAudioSource.pitch = Mathf.Lerp(highHeartbeatPitch, maxHeartbeatPitch, 1 - sanityPercent);
            }
        }
    }

    public void StopHeartbeat()
    {
        if (heartbeatAudioSource != null)
        {
            heartbeatAudioSource.Stop();
        }
    }
}
