using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonStyle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image buttonImage;
    private TMP_Text buttonText;
    private RectTransform buttonRect;

    [Header("Colors")]
    private Color normalColor = new Color32(184, 90, 73, 255);   // #B85A49
    private Color hoverColor = new Color32(170, 83, 67, 255);    // #AA5343
    private Color pressedColor = new Color32(145, 70, 58, 255);  // darker

    private Color textColor = Color.white;

    private bool isPointerOver = false;

    private void Awake()
    {
        // 🔥 Auto get components (no drag needed)
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
        if (buttonImage != null)
            buttonImage.color = normalColor;

        if (buttonText != null)
            buttonText.color = textColor;

        if (buttonRect != null)
            buttonRect.localScale = Vector3.one;
    }

    private void ApplyHoverState()
    {
        if (buttonImage != null)
            buttonImage.color = hoverColor;

        if (buttonText != null)
            buttonText.color = textColor;

        if (buttonRect != null)
            buttonRect.localScale = new Vector3(1.04f, 1.04f, 1f);
    }

    private void ApplyPressedState()
    {
        if (buttonImage != null)
            buttonImage.color = pressedColor;

        if (buttonText != null)
            buttonText.color = textColor;

        if (buttonRect != null)
            buttonRect.localScale = new Vector3(0.98f, 0.98f, 1f);
    }
}