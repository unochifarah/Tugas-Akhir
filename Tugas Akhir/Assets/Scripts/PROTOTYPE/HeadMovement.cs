using UnityEngine;
using System.Collections;

public class HeadMovement : MonoBehaviour
{
    public float sensitivity = 2f; // Sensitivity for mouse movement
    public float maxVerticalAngle = 35f; // Maximum vertical angle the head can turn
    public float maxHorizontalAngle = 60f; // Maximum horizontal angle the head can turn

    public float blinkSpeed = 0.2f; // Speed at which the eyelids close and open

    private float xRotation = 0f;
    private float yRotation = 0f;

    private RectTransform blinkOverlayTop;
    private RectTransform blinkOverlayBottom;

    private bool isBlinking = false;

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;

        // Create the blink overlay
        GameObject overlayTopObject = new GameObject("BlinkOverlayTop");
        overlayTopObject.transform.SetParent(transform, false);
        blinkOverlayTop = overlayTopObject.AddComponent<RectTransform>();
        SetupBlinkOverlay(blinkOverlayTop, new Vector2(0.5f, 1f), new Vector2(Screen.width, 0));

        GameObject overlayBottomObject = new GameObject("BlinkOverlayBottom");
        overlayBottomObject.transform.SetParent(transform, false);
        blinkOverlayBottom = overlayBottomObject.AddComponent<RectTransform>();
        SetupBlinkOverlay(blinkOverlayBottom, new Vector2(0.5f, 0f), new Vector2(Screen.width, 0));
    }

    void SetupBlinkOverlay(RectTransform rectTransform, Vector2 pivot, Vector2 size)
    {
        rectTransform.sizeDelta = size;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = pivot;

        Canvas overlayCanvas = rectTransform.gameObject.AddComponent<Canvas>();
        overlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        rectTransform.gameObject.AddComponent<CanvasRenderer>();

        UnityEngine.UI.Image overlayImage = rectTransform.gameObject.AddComponent<UnityEngine.UI.Image>();
        overlayImage.color = Color.black;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Adjust the rotations based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp the xRotation to simulate laying down head movement
        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);
        yRotation = Mathf.Clamp(yRotation, -maxHorizontalAngle, maxHorizontalAngle);

        // Apply the rotations to the camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Handle blinking
        if (Input.GetKeyDown(KeyCode.Space) && !isBlinking)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    IEnumerator BlinkRoutine()
    {
        isBlinking = true;
        float elapsedTime = 0f;

        // Close eyes
        while (elapsedTime < blinkSpeed)
        {
            float t = elapsedTime / blinkSpeed;
            blinkOverlayTop.sizeDelta = new Vector2(Screen.width, Mathf.Lerp(0, Screen.height / 2, t));
            blinkOverlayBottom.sizeDelta = new Vector2(Screen.width, Mathf.Lerp(0, Screen.height / 2, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        blinkOverlayTop.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        blinkOverlayBottom.sizeDelta = new Vector2(Screen.width, Screen.height / 2);

        // Wait for a short duration before opening eyes
        yield return new WaitForSeconds(0.1f);

        // Open eyes
        elapsedTime = 0f;
        while (elapsedTime < blinkSpeed)
        {
            float t = elapsedTime / blinkSpeed;
            blinkOverlayTop.sizeDelta = new Vector2(Screen.width, Mathf.Lerp(Screen.height / 2, 0, t));
            blinkOverlayBottom.sizeDelta = new Vector2(Screen.width, Mathf.Lerp(Screen.height / 2, 0, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        blinkOverlayTop.sizeDelta = new Vector2(Screen.width, 0);
        blinkOverlayBottom.sizeDelta = new Vector2(Screen.width, 0);

        isBlinking = false;
    }
}
