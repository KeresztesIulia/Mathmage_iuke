using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] string attributeName;
    [SerializeField] int defaultValue;
    [SerializeField] TextMeshProUGUI valueLabel;

    public string AttributeName
    {
        get { return attributeName; }
    }

    public int DefaultValue
    {
        get { return defaultValue; }
    }

    private void Start()
    {
        string actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
        slider.value = PlayerPrefs.GetInt(actualUser + attributeName, defaultValue);
        ChangeValueLabel();
    }

    public void ChangeSavedValue()
    {
        string actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
        PlayerPrefs.SetInt(actualUser + attributeName, (int)slider.value);
        PlayerPrefs.Save();
    }

    public void ChangeValueLabel()
    {
        valueLabel.text = slider.value.ToString();
    }

}
