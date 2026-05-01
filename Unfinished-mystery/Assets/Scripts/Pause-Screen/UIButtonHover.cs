using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public PauseMenuController pauseMenuController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pauseMenuController != null)
        {
            pauseMenuController.PlayHoverSound();
        }
    }
}