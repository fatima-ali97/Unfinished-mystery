using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasFilmReel = false;
    public GameObject filmReelIcon;

    public void AddFilmReel()
    {
        hasFilmReel = true;

        if (filmReelIcon != null)
            filmReelIcon.SetActive(true);

        Debug.Log("Film reel added to inventory UI!");
    }
}