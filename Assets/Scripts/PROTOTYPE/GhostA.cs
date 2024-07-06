using UnityEngine;
using System.Collections;

public class GhostA : MonoBehaviour
{
    public float existenceDuration = 5f; // Duration before the ghost disappears

    private GhostManager ghostManager;

    void Start()
    {
        ghostManager = FindObjectOfType<GhostManager>();
        StartCoroutine(DisappearAfterTime(existenceDuration));
    }

    void Update()
    {
        // Handle ghost behavior (e.g., becoming stronger if the player recites prayers)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase ghost strength if the player is reciting prayers
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset ghost strength if the player stops reciting prayers
        }
    }

    IEnumerator DisappearAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        ghostManager.GhostDisappeared();
        Destroy(gameObject);
    }
}
