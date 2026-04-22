using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrawer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject inkPrefab;
    public Transform drawingParent;

    // How close the dots are (smaller = smoother but more lag
    public float dotSpacing = 2.0f;
    private Vector2 lastPointerPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPointerPosition = eventData.position;
        CreateDot(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(lastPointerPosition, eventData.position);

        // fill the gape between mouse positions if the distance is greater than the spacing
        if (distance > dotSpacing)
        {
            int dotsToSpawn = Mathf.FloorToInt(distance / dotSpacing);

            for (int i = 1; i <= dotsToSpawn; i++)
            {
                // last pos + current pos * t
                float t = (float)i / dotsToSpawn;
                Vector2 interpolatedPosition = Vector2.Lerp(lastPointerPosition, eventData.position, t);
                CreateDot(interpolatedPosition);
            }
        }

        lastPointerPosition = eventData.position;
    }

    void CreateDot(Vector2 position)
    {
        GameObject newDot = Instantiate(inkPrefab, drawingParent);
        newDot.transform.position = position;
    }

    public void ClearDrawing()
    {
        foreach (Transform child in drawingParent)
        {
            Destroy(child.gameObject);
        }
    }
}