using UnityEngine;

[CreateAssetMenu(fileName = "Level -1 Data", menuName = "Level Data", order = 0)]
public class LevelData : ScriptableObject
{
    public float playerSpeed;
    public float[] dialogPoints;
    public string nextLVLName;
}