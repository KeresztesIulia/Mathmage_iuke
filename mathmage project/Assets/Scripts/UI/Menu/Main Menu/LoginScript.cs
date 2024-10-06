using System;
using TMPro;
using UnityEngine;

public class LoginScript : ButtonScript
{
    [SerializeField] TextMeshProUGUI usernameText;
    [SerializeField] TMP_InputField usernameInput;

    public static event Action UsernameChanged;

    string usernameString = GameFunctions.usernameString;
    static bool inited;

    private void OnEnable()
    {
        if (inited)
        {
            StartUsername();
        }
        else
        {
            inited = true;
        }
    }

    private void Start()
    {
        StartUsername();
    }

    void StartUsername()
    {
        ChangeText();
        if (UsernameChanged != null)
        {
            UsernameChanged();
        }
    }

    public override void OnClick()
    {
        SetUsername();
        ChangeText();
        if(UsernameChanged != null)
        {
            UsernameChanged(); // what's supposed to be subscribed here? -> continue button
        }
    }

    void SetUsername()
    {
        if (usernameInput.text == "")
        {
            PlayerPrefs.SetString(usernameString, "guest");
        }
        else
        {
            PlayerPrefs.SetString(usernameString, usernameInput.text);
        }
    }
    void ChangeText()
    {
        string username = PlayerPrefs.GetString(usernameString);
        if (username == "guest" || username == "")
        {
            usernameText.text = "vendég";
        }
        else
        {
            usernameText.text = username;
        }
        usernameInput.text = "";
    }
}
