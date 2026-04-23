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
        ApplyDefaultIfNeeded();

        Sprite portraitToUse;

        if (LevelResultData.portrait != null)
        {
            portraitToUse = LevelResultData.portrait;
        }
        else
        {
            portraitToUse = Resources.Load<Sprite>("professor");
        }

        ShowSummary(
            LevelResultData.title,
            LevelResultData.levelName,
            LevelResultData.characterName,
            LevelResultData.role,
            LevelResultData.loopsUsed,
            LevelResultData.resultMessage,
            portraitToUse
        );
    }

    private void ApplyDefaultIfNeeded()
    {
        if (string.IsNullOrEmpty(LevelResultData.title))
            LevelResultData.title = "LEVEL COMPLETE";

        if (string.IsNullOrEmpty(LevelResultData.levelName))
            LevelResultData.levelName = "Level 1";

        if (string.IsNullOrEmpty(LevelResultData.characterName))
            LevelResultData.characterName = "Kyryll Flins";

        if (string.IsNullOrEmpty(LevelResultData.role))
            LevelResultData.role = "Mathematics Professor";

        if (LevelResultData.loopsUsed <= 0)
            LevelResultData.loopsUsed = 3;

        if (string.IsNullOrEmpty(LevelResultData.resultMessage))
            LevelResultData.resultMessage = "The truth has been uncovered.";
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
        if (summaryPanel != null)
            summaryPanel.SetActive(true);

        if (titleText != null)
            titleText.text = title;

        if (levelText != null)
            levelText.text = level;

        if (identityText != null)
            identityText.text = "IDENTITY REVEALED: " + characterName;

        if (roleText != null)
            roleText.text = role;

        if (loopsText != null)
            loopsText.text = "Loops Used: " + loopsUsed + " / 10";

        if (resultText != null)
            resultText.text = resultMessage;

        if (portraitImage != null)
        {
            Sprite finalPortrait = portrait != null ? portrait : defaultPortrait;

            portraitImage.sprite = finalPortrait;
            portraitImage.overrideSprite = finalPortrait;
            portraitImage.color = Color.white;
            portraitImage.enabled = true;
            portraitImage.preserveAspect = true;
        }

        ResetAllStars();

        if (gameObject.activeInHierarchy)
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