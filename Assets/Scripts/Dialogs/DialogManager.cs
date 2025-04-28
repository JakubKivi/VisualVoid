using System.IO;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Dialogs")]
    [SerializeField] private string level = "1";  
    [SerializeField] private string language = "EN";

    [SerializeField] private TextMeshProUGUI dialogSpeaker;
    [SerializeField] private TextMeshProUGUI dialog;

    private DialogueEntity[] dialogs;
    private int currentDialogIndex = 0;

    private void Start()
    {   
        LoadDialoguesFromJson();
        StartDialogue();
    }

    

    private void LoadDialoguesFromJson()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, $"dialogs/LVL_{level}_{language}.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueData loadedData = JsonUtility.FromJson<DialogueData>(json);
            dialogs = loadedData.dialogs;
        }
        else
        {
            Debug.LogError("Cannod find dialog file: " + filePath);
        }
    }

    public void StartDialogue()
    {
        currentDialogIndex = 0;
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if (dialogs == null || dialogs.Length == 0)
        {
            Debug.LogWarning("No dialog to show!");
            return;
        }

        if (currentDialogIndex >= dialogs.Length)
        {
            EndDialogue();
            return;
        }

        DialogueEntity entry = dialogs[currentDialogIndex];
        Debug.Log($"{entry.speaker}: {entry.dialog}");

        dialogSpeaker.text = entry.speaker;
        dialogSpeaker.gameObject.SetActive(true);
        dialog.text = entry.dialog;
        dialog.gameObject.SetActive(true);

        currentDialogIndex++;
    }

    private void EndDialogue()
    {
        Debug.Log("Dialog ended.");
    }

    
    public void ChangeLevelAndLanguage(string newLevel, string newLanguage)
    {
        level = newLevel;
        language = newLanguage;
        LoadDialoguesFromJson(); 
    }
}
