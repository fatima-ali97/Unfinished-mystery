using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonStyle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    public Image buttonImage;
    public TMP_Text buttonText;
    public RectTransform buttonRect;

    [Header("Colors")]
    public Color normalColor = new Color32(243, 239, 232, 255);     // #F3EFE8
    public Color hoverColor = new Color32(228, 183, 128, 255);      // #E4B780
    public Color pressedColor = new Color32(184, 90, 73, 255);      // #B85A49

    public Color normalTextColor = new Color32(69, 69, 70, 255);    // #454546
    public Color hoverTextColor = Color.white;
    public Color pressedTextColor = Color.white;

    [Header("Scale")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.04f, 1.04f, 1f);
    public Vector3 pressedScale = new Vector3(0.98f, 0.98f, 1f);

    private bool isPointerOver = false;

    private void Reset()
    {
        buttonImage = GetComponent<Image>();
        buttonRect = GetComponent<RectTransform>();
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        ApplyNormalState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        ApplyHoverState();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        ApplyNormalState();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyPressedState();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerOver)
            ApplyHoverState();
        else
            ApplyNormalState();
    }

    private void ApplyNormalState()
    {
        if (buttonImage != null) buttonImage.color = normalColor;
        if (buttonText != null) buttonText.color = normalTextColor;
        if (buttonRect != null) buttonRect.localScale = normalScale;
    }

    private void ApplyHoverState()
    {
        if (buttonImage != null) buttonImage.color = hoverColor;
        if (buttonText != null) buttonText.color = hoverTextColor;
        if (buttonRect != null) buttonRect.localScale = hoverScale;
    }

    private void ApplyPressedState()
    {
        if (buttonImage != null) buttonImage.color = pressedColor;
        if (buttonText != null) buttonText.color = pressedTextColor;
        if (buttonRect != null) buttonRect.localScale = pressedScale;
    }
}