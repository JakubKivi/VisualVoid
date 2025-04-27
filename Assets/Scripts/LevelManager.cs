using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelData currentLevelData;

    private float playerSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (currentLevelData != null)
        {
            playerSpeed = currentLevelData.playerSpeed;
        }
    }

    public void CheckForDialogs(float playerPositionX)
    {
        foreach (var dialogPoint in currentLevelData.dialogPoints)
        {
            if (playerPositionX >= dialogPoint)
            {
                // Trigger dialog here, based on the level's dialog points
                Debug.Log("Dialog trigger at X: " + dialogPoint);
            }
        }
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
