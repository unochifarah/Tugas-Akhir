using UnityEngine;
using System.Collections;

public class GhostB : MonoBehaviour
{
    public float existenceDuration = 8f; // Duration before the ghost disappears

    private GhostManager ghostManager;
    //private AudioSource prayerAudioSource;

    void Start()
    {
        ghostManager = FindObjectOfType<GhostManager>();
        //prayerAudioSource = GetComponent<AudioSource>();
        StartCoroutine(DisappearAfterTime(existenceDuration));
    }

    void Update()
    {
        // Handle ghost behavior (e.g., weaken if the player recites prayers)
        //if (prayerAudioSource.isPlaying && IsPlayerLookingAtGhost())
        {
            // Weaken the ghost
        }
    }

    bool IsPlayerLookingAtGhost()
    {
        // Implement logic to check if the player is looking at the ghost
        return true; // Placeholder
    }

    IEnumerator DisappearAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        ghostManager.GhostDisappeared();
        Destroy(gameObject);
    }
}
