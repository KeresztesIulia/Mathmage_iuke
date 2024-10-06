using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] SliderScript[] settingsSliders;
    [SerializeField] List<ISaveable> saveables = new List<ISaveable>();
    [SerializeField] Canvas blackscreen;
    [SerializeField] GameObject videoBg;

    public DateTime saveTime;

    static bool inited = false;

    public static bool GameInited
    {
        get
        {
            return inited;
        }
    }

    void Start()
    {
        inited = true;
        FindSaveables();
        bool isNewGame = PlayerPrefs.GetInt(GameFunctions.NewGameString) != 0;
        if (isNewGame)
        {
            DeletePreviousSave();
            StartTime();
            ResetSettings();
            SaveGame();
        }
        else
        {
            // check if the 3 day thing is over
            LoadGame();
        }
        GameFunctions.videoBg = videoBg;
        blackscreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameFunctions.GameIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameFunctions.PlayerActive)
            {
                pauseMenu.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameFunctions.PlayerActive = !GameFunctions.PlayerActive;
            }
        }
    }
    private void OnEnable()
    {
        GameFunctions.SaveGame += SaveGame;
    }

    private void OnDisable()
    {
        GameFunctions.SaveGame -= SaveGame;
    }

    void FindSaveables()
    {
        GameObject[] rootGOs = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in rootGOs) saveables.AddRange(go.GetComponentsInChildren<ISaveable>(true));
    }

    void DeletePreviousSave()
    {
        
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), GameFunctions.SavePath);
        string username = PlayerPrefs.GetString(GameFunctions.usernameString);
        if (!Directory.Exists(Path.Combine(savePath, username))) return;
            Directory.Delete(Path.Combine(savePath, username), true);
    }

    void StartTime()
    {
        string actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
        PlayerPrefs.SetString(actualUser + "startDate", DateTime.Now.ToString());
    }

    void ResetSettings()
    {
        string actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
        foreach (SliderScript slider in settingsSliders)
        {
            PlayerPrefs.SetInt(actualUser + slider.AttributeName, slider.DefaultValue);
        }
    }

    void SaveGame()
    {
        if (!inited) return;
        MakeSaveFolder();
        MakeUserFolder();
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), GameFunctions.SavePath);
        string dateString = GameFunctions.SaveDate.ToString();
        string username = PlayerPrefs.GetString(GameFunctions.usernameString);
        Stream file = File.Create(Path.Combine(savePath, username, username + dateString));
        BinaryFormatter bf = new BinaryFormatter();
        foreach (ISaveable saveable in saveables)
        {
            bf.Serialize(file, saveable.SaveData());
        }
        bf.Serialize(file, SaveGlobalData());
        file.Flush();
        file.Close();
        file.Dispose();

        PlayerPrefs.SetString(username, dateString);
    }

    List<object> SaveGlobalData()
    {
        return new List<object>
        {
            Physics.gravity.y,
            GameFunctions.F1HasFood,
            GameFunctions.MageIsNotHereKnown,
            GameFunctions.F3HasBook,
            GameFunctions.F3HasGravityDevice,
            GameFunctions.ENDVideoWatched,
            GameFunctions.ENDVideoFinished,
            GameFunctions.ENDDeviceActivated
        };
    }

    void MakeSaveFolder()
    {
        string dirPath = Path.Combine(Directory.GetCurrentDirectory(), GameFunctions.SavePath);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
    }

    void MakeUserFolder()
    {
        string actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
        string dirPath = Path.Combine(Directory.GetCurrentDirectory(), GameFunctions.SavePath, actualUser);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
    }

    void LoadGame()
    {
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), GameFunctions.SavePath);
        string username = PlayerPrefs.GetString(GameFunctions.usernameString);
        string dateString = PlayerPrefs.GetString(username);
        Stream file = File.OpenRead(Path.Combine(savePath, username, username + dateString));
        BinaryFormatter bf = new BinaryFormatter();
        foreach (ISaveable saveable in saveables)
        {
            Debug.Log("Loading " +  saveable);
            saveable.LoadData((List<object>)bf.Deserialize(file));
        }
        LoadGlobalData((List<object>) bf.Deserialize(file));
        file.Close();
        file.Dispose();
    }

    void LoadGlobalData(List<object> data)
    {
        Physics.gravity = new Vector3(0, (float)data[0], 0);
        GameFunctions.F1HasFood = (bool)data[1];
        GameFunctions.MageIsNotHereKnown = (bool)data[2];
        GameFunctions.F3HasBook = (bool)data[3];
        GameFunctions.F3HasGravityDevice = (bool)data[4];
        GameFunctions.ENDVideoWatched = (bool)data[5];
        GameFunctions.ENDVideoFinished = (bool)data[6];
        GameFunctions.ENDDeviceActivated = (bool)data[7];
    }

}
