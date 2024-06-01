using UnityEngine;

public class ComponentTap : MonoBehaviour
{
    // Reference to the object you want to appear when tapped
    public GameObject popupObject;

    private void Update()
    {
        // Check if the user tapped the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (you can handle multiple touches if needed)

            // Check if the touch phase is "Began" (when the user first touches the screen)
            if (touch.phase == TouchPhase.Began)
            {
                // Raycast from the touch position
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the ray hits any collider
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the collider belongs to the GameObject with this script
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Instantiate or activate the popup object
                        if (popupObject != null)
                        {
                            // If the popup object is not active, activate it
                            if (!popupObject.activeSelf)
                            {
                                popupObject.SetActive(true);
                            }
                            // Otherwise, deactivate it
                            else
                            {
                                popupObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
