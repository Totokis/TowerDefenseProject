using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_InputField nameInputField = null;
    [SerializeField] Button continueButton = null;
    const string PlayerPrefsNameKey = "PlayerName";

    public static string DisplayName { get; private set; }

    bool IsNotPlayerPrefsHasKey => !PlayerPrefs.HasKey(PlayerPrefsNameKey);

    void Start()
    {
        SetUpInputField();
    }
    void SetUpInputField()
    {
        if (IsNotPlayerPrefsHasKey)
            return;

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);
    }
    public void SetPlayerName(string name)
    {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey,DisplayName);
    }

}
