using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image bg;

    [Header("Text Colors")]
    [SerializeField] private Color normalColor = new Color32(245, 240, 230, 255);
    [SerializeField] private Color hoverColor = new Color32(255, 250, 240, 255);
    [SerializeField] private Color clickColor = new Color32(220, 210, 190, 255);

    [Header("Background Colors")]
    [SerializeField] private Color normalBg = new Color32(60, 40, 25, 255);
    [SerializeField] private Color hoverBg = new Color32(85, 60, 40, 255);
    [SerializeField] private Color clickBg = new Color32(40, 25, 18, 255);

    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<TMP_Text>(true);

        if (bg == null)
            bg = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (text != null) text.color = normalColor;
        if (bg != null) bg.color = normalBg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null) text.color = hoverColor;
        if (bg != null) bg.color = hoverBg;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null) text.color = normalColor;
        if (bg != null) bg.color = normalBg;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text != null) text.color = clickColor;
        if (bg != null) bg.color = clickBg;
    }
}