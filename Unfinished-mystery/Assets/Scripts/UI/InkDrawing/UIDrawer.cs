using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrawer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject inkPrefab;
    public RectTransform drawingArea;  
    public float dotSpacing = 2.0f;
    private Vector2 lastPointerPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Only draw if the click is actually inside the box
        if (IsInsideBoundary(eventData.position))
        {
            lastPointerPosition = eventData.position;
            CreateDot(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Only draw the next dots if the mouse is still inside the box
        if (IsInsideBoundary(eventData.position))
        {
            float distance = Vector2.Distance(lastPointerPosition, eventData.position);

            if (distance > dotSpacing)
            {
                int dotsToSpawn = Mathf.FloorToInt(distance / dotSpacing);
                for (int i = 1; i <= dotsToSpawn; i++)
                {
                    float t = (float)i / dotsToSpawn;
                    Vector2 interpolatedPosition = Vector2.Lerp(lastPointerPosition, eventData.position, t);
                    CreateDot(interpolatedPosition);
                }
            }
            lastPointerPosition = eventData.position;
        }
    }

    
    bool IsInsideBoundary(Vector2 screenPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(drawingArea, screenPos, null);
    }

    void CreateDot(Vector2 position)
    {
        GameObject newDot = Instantiate(inkPrefab, drawingArea);
        newDot.transform.position = position;
    }

    public void ClearDrawing()
    {
        foreach (Transform child in drawingArea)
        {
            Destroy(child.gameObject);
        }
    }
}