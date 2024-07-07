using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public int maxSanity = 100;
    [SerializeField] private int currentSanity;
    public float sanityRecoveryRate = 0.5f;
    public float breathingNormalizationRate = 0.2f;
    public float breathingSanityRecoveryRate = 1f;

    public HeartbeatManager heartbeatManager;
    public BreathingManager breathingManager;
    public VisionManager visionManager;

    private bool eyesClosed = false;
    private float accumulatedSanityRecovery = 0f;
    private float accumulatedBreathingRecovery = 0f;

    void Start()
    {
        currentSanity = maxSanity;
        Debug.Log("SanityManager started with currentSanity: " + currentSanity);
    }

    void Update()
    {
        eyesClosed = Input.GetKey(KeyCode.Space);

        if (eyesClosed)
        {
            accumulatedSanityRecovery += sanityRecoveryRate * Time.deltaTime;
            Debug.Log("Accumulated sanity recovery while eyes are closed: " + accumulatedSanityRecovery);
            if (accumulatedSanityRecovery >= 1f)
            {
                int recoveryAmount = Mathf.FloorToInt(accumulatedSanityRecovery);
                accumulatedSanityRecovery -= recoveryAmount;
                currentSanity = Mathf.Min(maxSanity, currentSanity + recoveryAmount);
                Debug.Log("Recovering sanity while eyes are closed. Current sanity: " + currentSanity);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            NormalizeBreathing();
        }

        UpdateEffectsBasedOnSanity();

        if (currentSanity <= 0)
        {
            GameOver();
        }

        Debug.Log("Current Sanity: " + currentSanity);
    }

    void NormalizeBreathing()
    {
        // Normalize breathing and recover sanity
        accumulatedBreathingRecovery += breathingSanityRecoveryRate * Time.deltaTime;
        Debug.Log("Accumulated breathing recovery: " + accumulatedBreathingRecovery);
        if (accumulatedBreathingRecovery >= 1f)
        {
            int recoveryAmount = Mathf.FloorToInt(accumulatedBreathingRecovery);
            accumulatedBreathingRecovery -= recoveryAmount;
            currentSanity = Mathf.Min(maxSanity, currentSanity + recoveryAmount);
            Debug.Log("Normalizing breathing and recovering sanity. Current sanity: " + currentSanity);
        }

        breathingManager.NormalizeBreathing(breathingNormalizationRate * Time.deltaTime);
    }

    void UpdateEffectsBasedOnSanity()
    {
        float sanityPercent = (float)currentSanity / maxSanity;

        if (heartbeatManager != null)
        {
            heartbeatManager.UpdateHeartbeat(sanityPercent);
        }

        if (breathingManager != null)
        {
            breathingManager.UpdateBreathing(sanityPercent);
        }

        if (visionManager != null)
        {
            visionManager.UpdateVision(sanityPercent);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        if (heartbeatManager != null)
        {
            heartbeatManager.StopHeartbeat();
        }
        if (breathingManager != null)
        {
            breathingManager.StopBreathing();
        }
        if (visionManager != null)
        {
            visionManager.GameOverVision();
        }
    }

    public void ModifySanity(int amount)
    {
        Debug.Log("Modifying sanity by: " + amount);
        currentSanity = Mathf.Clamp(currentSanity + amount, 0, maxSanity);
        Debug.Log("Sanity modified. New sanity: " + currentSanity);
    }

    public bool AreEyesClosed()
    {
        return eyesClosed;
    }
}
