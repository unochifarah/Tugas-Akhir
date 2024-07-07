using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxVerticalAngle = 35f;
    public float maxHorizontalAngle = 60f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private CanvasGroup blinkOverlay;

    private bool eyesClosed = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        GameObject overlayObject = new GameObject("BlinkOverlay");
        overlayObject.transform.SetParent(transform, false);

        blinkOverlay = overlayObject.AddComponent<CanvasGroup>();
        blinkOverlay.alpha = 0f;

        RectTransform rectTransform = overlayObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        Canvas overlayCanvas = overlayObject.AddComponent<Canvas>();
        overlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        overlayObject.AddComponent<CanvasRenderer>();

        UnityEngine.UI.Image overlayImage = overlayObject.AddComponent<UnityEngine.UI.Image>();
        overlayImage.color = Color.black;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);
        yRotation = Mathf.Clamp(yRotation, -maxHorizontalAngle, maxHorizontalAngle);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Smoothly handle blinking
        float targetAlpha = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        blinkOverlay.alpha = Mathf.Lerp(blinkOverlay.alpha, targetAlpha, Time.deltaTime * 5f);

        // Update eyesClosed status
        eyesClosed = Input.GetKey(KeyCode.Space);
    }

    public bool AreEyesClosed()
    {
        return eyesClosed;
    }
}
