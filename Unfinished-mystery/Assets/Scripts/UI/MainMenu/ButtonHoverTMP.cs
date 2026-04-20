using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TMP_Text text;

    public Color normalColor = new Color32(255,255,255,255);
    public Color hoverColor = new Color32(222,203,184,255); 
    public Color clickColor = new Color32(255,243,214,255);
    
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.color = clickColor;
    }
}