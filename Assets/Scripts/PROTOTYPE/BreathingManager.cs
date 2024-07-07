using UnityEngine;

public class BreathingManager : MonoBehaviour
{
    public AudioSource breathingAudioSource;
    public float normalBreathingVolume = 0.2f;
    public float maxBreathingVolume = 1.0f;
    public float normalBreathingPitch = 1.0f;
    public float maxBreathingPitch = 2.0f;

    void Start()
    {
        if (breathingAudioSource != null)
        {
            breathingAudioSource.volume = normalBreathingVolume;
            breathingAudioSource.pitch = normalBreathingPitch;
            breathingAudioSource.loop = true;
            breathingAudioSource.Play();
        }
        else
        {
            Debug.LogError("Breathing AudioSource is not assigned!");
        }
    }

    public void UpdateBreathing(float sanityPercent)
    {
        if (breathingAudioSource != null)
        {
            if (sanityPercent > 0.75f)
            {
                breathingAudioSource.volume = normalBreathingVolume;
                breathingAudioSource.pitch = normalBreathingPitch;
            }
            else if (sanityPercent <= 0.75f && sanityPercent > 0.5f)
            {
                breathingAudioSource.volume = Mathf.Lerp(normalBreathingVolume, maxBreathingVolume, 1 - sanityPercent);
                breathingAudioSource.pitch = Mathf.Lerp(normalBreathingPitch, maxBreathingPitch, 1 - sanityPercent);
            }
            else if (sanityPercent <= 0.5f && sanityPercent > 0.25f)
            {
                breathingAudioSource.volume = Mathf.Lerp(normalBreathingVolume, maxBreathingVolume, 1 - sanityPercent);
                breathingAudioSource.pitch = Mathf.Lerp(normalBreathingPitch, maxBreathingPitch, 1 - sanityPercent);
            }
            else if (sanityPercent <= 0.25f)
            {
                breathingAudioSource.volume = Mathf.Lerp(normalBreathingVolume, maxBreathingVolume, 1 - sanityPercent);
                breathingAudioSource.pitch = Mathf.Lerp(normalBreathingPitch, maxBreathingPitch, 1 - sanityPercent);
            }
        }
    }

    public void NormalizeBreathing(float rate)
    {
        if (breathingAudioSource != null)
        {
            breathingAudioSource.volume = Mathf.MoveTowards(breathingAudioSource.volume, normalBreathingVolume, rate);
            breathingAudioSource.pitch = Mathf.MoveTowards(breathingAudioSource.pitch, normalBreathingPitch, rate);
        }
    }

    public void StopBreathing()
    {
        if (breathingAudioSource != null)
        {
            breathingAudioSource.Stop();
        }
    }
}
