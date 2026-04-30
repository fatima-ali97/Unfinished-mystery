using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelTitle;     
    public string identityName;    
    [TextArea(3, 6)]
    public string description;    
    public int loopsRemaining;
    public bool isLocked;
}