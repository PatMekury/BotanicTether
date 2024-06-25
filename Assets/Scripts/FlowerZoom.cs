using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlowerZoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject canvasToShow; // The canvas GameObject to appear
    public GameObject gestureGUI; // Reference to the gesture GUI GameObject
    private bool isZooming = false;

    void Start()
    {
        // Initialize other components or settings as needed
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == 0 && gestureGUI != null && gestureGUI.activeSelf)
        {
            // First finger touched the screen over the active gesture GUI
            isZooming = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == 0)
        {
            // First finger released
            isZooming = false;
        }
    }

    void Update()
    {
        if (isZooming && Input.touchCount == 2)
        {
            // Check if two fingers are touching the screen
            Vector2 finger1Pos = Input.GetTouch(0).position;
            Vector2 finger2Pos = Input.GetTouch(1).position;

            float distance = Vector2.Distance(finger1Pos, finger2Pos);

            // You can adjust the threshold for zooming here
            float zoomThreshold = 100f;

            if (distance < zoomThreshold)
            {
                // Show the canvas GameObject
                canvasToShow.SetActive(true);
                gestureGUI.SetActive(false);
            }
            else
            {
                // Hide the canvas GameObject
                canvasToShow.SetActive(false);
            }
        }
    }
}
