using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.black;
    [SerializeField] private Color hoverColor = new Color32(74, 66, 61, 255);
    [SerializeField] private Color clickColor = new Color32(110, 95, 85, 255); 
    
    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<TMP_Text>(true);
    }

    private void OnEnable()
    {
        if (text != null)
            text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
            text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null)
            text.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text != null)
            text.color = clickColor;
    }
}