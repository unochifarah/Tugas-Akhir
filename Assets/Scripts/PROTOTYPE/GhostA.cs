using UnityEngine;

public class GhostA : MonoBehaviour
{
    public float drainRate = 7f; // Sanity drain rate per second when the player looks at the ghost
    public float approachSpeed = 1f; // Speed at which the ghost approaches when looked at
    public Transform player;
    public float disappearanceTime = 5f; // Time in seconds for the ghost to disappear when not looked at

    private bool isLookedAt = false;
    private float lookAwayTimer = 0f;
    private float accumulatedDrain = 0f;
    private SanityManager sanityManager;
    private GhostManager ghostManager;

    void Start()
    {
        sanityManager = FindObjectOfType<SanityManager>();
        ghostManager = FindObjectOfType<GhostManager>();

        if (sanityManager == null)
        {
            Debug.LogError("SanityManager not found in the scene!");
        }

        if (ghostManager == null)
        {
            Debug.LogError("GhostManager not found in the scene!");
        }

        Debug.Log("GhostA initialized with drainRate: " + drainRate);
    }

    void Update()
    {
        if (isLookedAt)
        {
            Debug.Log("Player is looking at GhostA");
            // Reset the look away timer
            lookAwayTimer = 0f;

            // Drain sanity
            accumulatedDrain += drainRate * Time.deltaTime;
            Debug.Log($"Accumulated drain amount: {accumulatedDrain}");

            if (accumulatedDrain >= 1f)
            {
                int intDrainAmount = Mathf.FloorToInt(accumulatedDrain);
                accumulatedDrain -= intDrainAmount;
                Debug.Log($"Sanity drain amount (int): {intDrainAmount}");
                sanityManager.ModifySanity(-intDrainAmount);
            }

            // Approach player
            transform.position = Vector3.MoveTowards(transform.position, player.position, approachSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Player is not looking at GhostA");
            // Increase the look away timer
            lookAwayTimer += Time.deltaTime;

            // Check if the ghost should disappear
            if (lookAwayTimer >= disappearanceTime)
            {
                Debug.Log("GhostA disappeared");
                // Notify the GhostManager and destroy this ghost
                ghostManager.GhostDisappeared();
                Destroy(gameObject);
            }
        }
    }

    public void OnLookAt()
    {
        isLookedAt = true;
    }

    public void OnLookAway()
    {
        isLookedAt = false;
    }
}
