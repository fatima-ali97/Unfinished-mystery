using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSummaryUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject summaryPanel;

    [Header("Texts")]
    public TMP_Text titleText;
    public TMP_Text levelText;
    public TMP_Text identityText;
    public TMP_Text roleText;
    public TMP_Text loopsText;
    public TMP_Text resultText;

    [Header("Portrait")]
    public Image portraitImage;
    public Sprite defaultPortrait;

    [Header("Stars")]
    public StarBounceUI star1;
    public StarBounceUI star2;
    public StarBounceUI star3;
    public float delayBetweenStars = 0.2f;

    private void Start()
    {
        ShowSummary(
            "LEVEL COMPLETE",
            "Level 1",
            "Kyryll Flins",
            "Mathematics Professor",
            3,
            "The truth has been uncovered.",
            defaultPortrait
        );
    }

    public void ShowSummary(
        string title,
        string level,
        string characterName,
        string role,
        int loopsUsed,
        string resultMessage,
        Sprite portrait)
    {
        summaryPanel.SetActive(true);

        titleText.text = title;
        levelText.text = level;
        identityText.text = "IDENTITY REVEALED: " + characterName;
        roleText.text = role;
        loopsText.text = "Loops Used: " + loopsUsed + " / 10";
        resultText.text = resultMessage;

        if (portrait != null)
            portraitImage.sprite = portrait;

        ResetAllStars();
        StartCoroutine(PlayStarsRoutine(GetStarCount(loopsUsed)));
    }

    private int GetStarCount(int loopsUsed)
    {
        if (loopsUsed >= 1 && loopsUsed <= 3) return 3;
        if (loopsUsed >= 4 && loopsUsed <= 7) return 2;
        return 1;
    }

    private void ResetAllStars()
    {
        if (star1 != null) star1.ResetStar();
        if (star2 != null) star2.ResetStar();
        if (star3 != null) star3.ResetStar();
    }

    private IEnumerator PlayStarsRoutine(int starCount)
    {
        if (starCount >= 1 && star1 != null)
        {
            yield return star1.PlayAnimation();
            yield return new WaitForSecondsRealtime(delayBetweenStars);
        }

        if (starCount >= 2 && star2 != null)
        {
            yield return star2.PlayAnimation();
            yield return new WaitForSecondsRealtime(delayBetweenStars);
        }

        if (starCount >= 3 && star3 != null)
        {
            yield return star3.PlayAnimation();
        }
    }
}