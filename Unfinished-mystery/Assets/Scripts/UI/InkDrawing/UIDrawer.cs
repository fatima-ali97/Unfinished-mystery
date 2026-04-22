using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIDrawer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject inkPrefab; 
    public Transform drawingParent; 

    public void OnPointerDown(PointerEventData eventData)
    {
        CreateDot(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        CreateDot(eventData.position);
    }

    void CreateDot(Vector2 position)
    {
        GameObject newDot = Instantiate(inkPrefab, drawingParent);
        newDot.transform.position = position;
    }

    // Add a button to call this to clear the page!
    public void ClearDrawing()
    {
        foreach (Transform child in drawingParent)
        {
            Destroy(child.gameObject);
        }
    }
}