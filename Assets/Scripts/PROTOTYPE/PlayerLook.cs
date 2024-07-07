using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;
    public float lookDistance = 10f; // Distance at which the player can look at objects

    private HeadMovement headMovement;

    void Start()
    {
        headMovement = playerCamera.GetComponent<HeadMovement>();

        if (headMovement == null)
        {
            Debug.LogError("HeadMovement script not found on the player camera!");
        }
    }

    void Update()
    {
        if (headMovement != null && headMovement.AreEyesClosed())
        {
            foreach (var ghost in FindObjectsOfType<GhostA>())
            {
                ghost.OnLookAway();
            }
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, lookDistance))
        {
            GhostA ghost = hit.collider.GetComponent<GhostA>();
            if (ghost != null)
            {
                ghost.OnLookAt();
            }
            else
            {
                foreach (var g in FindObjectsOfType<GhostA>())
                {
                    g.OnLookAway();
                }
            }
        }
        else
        {
            foreach (var ghost in FindObjectsOfType<GhostA>())
            {
                ghost.OnLookAway();
            }
        }
    }
}
