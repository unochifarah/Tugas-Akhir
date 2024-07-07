using UnityEngine;
using UnityEngine.UI;

public class VisionManager : MonoBehaviour
{
    public Image vignetteImage;

    void Start()
    {
        if (vignetteImage != null)
        {
            vignetteImage.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            Debug.LogError("Vignette Image is not assigned!");
        }
    }

    public void UpdateVision(float sanityPercent)
    {
        if (vignetteImage != null)
        {
            if (sanityPercent > 0.75f)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, 0f);
            }
            else if (sanityPercent <= 0.75f && sanityPercent > 0.5f)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 0.2f, 1 - sanityPercent));
            }
            else if (sanityPercent <= 0.5f && sanityPercent > 0.25f)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0.2f, 0.5f, 1 - sanityPercent));
            }
            else if (sanityPercent <= 0.25f)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0.5f, 0.8f, 1 - sanityPercent));
            }
        }
    }

    public void GameOverVision()
    {
        if (vignetteImage != null)
        {
            vignetteImage.color = new Color(0f, 0f, 0f, 1f);
        }
    }
}
