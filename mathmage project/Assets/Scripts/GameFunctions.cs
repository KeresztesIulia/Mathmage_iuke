using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public static class GameFunctions
{
    public static bool PlayerActive = true;
    public static bool GameIsActive = true;
    public static Action SaveGame;
    public static DateTime saveDate = DateTime.Now;

    public static bool F1HasFood = false;
    public static bool MageIsNotHereKnown = false;
    public static bool F3HasBook = false;
    public static bool F3HasGravityDevice = false;
    public static bool ENDVideoWatched = false;
    public static bool ENDVideoFinished = false;
    public static bool ENDDeviceActivated = false;

    public static GameObject videoBg;

    public static PolarCurve F1TeleportCurve = PolarCurve.None;

    public enum PolarCurve { None, Circle, HorizontalLine, VerticalLine, Cardioid, InvertedCardioid, Spiral, InvertedSpiral, Lemniscate };

    public static long SaveDate { get { return saveDate.ToBinary(); } }

    public static string usernameString
    {
        get { return "ActualUser"; }
    }

    public static string NewGameString
    {
        get { return "isNewGame"; }
    }

    public static string SavePath
    {
        get
        {
            return "GameSaves/";
        }
    }

    public static void SwapCanvas(Canvas from, Canvas to)
    {
        if (from == null || from == to) { return; }
        
        to.gameObject.SetActive(true);
        from.gameObject.SetActive(false);
    }

    public static void Exit()
    {
        Application.Quit();
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static bool UserSaveExists()
    {
        string actualUser = PlayerPrefs.GetString(usernameString);
        return !string.IsNullOrEmpty(PlayerPrefs.GetString(actualUser));
    }

    public static Vector2 SamePlace(RectTransform toPlace, RectTransform where, Canvas parentCanvas)
    {
        float x = (toPlace.pivot.x - where.pivot.x) * toPlace.rect.width * parentCanvas.scaleFactor + where.position.x;
        float y = (toPlace.pivot.y - where.pivot.y) * toPlace.rect.height * parentCanvas.scaleFactor + where.position.y;
        return new Vector2(x, y);
    }

    public static bool zeroInput(string number)
    {
        return (number == "0" || number == "-" || number == ".");
    }

    public static void PlayTimeline(PlayableDirector director)
    {
        PlayerActive = false;
        GameIsActive = false;
        videoBg.SetActive(true);
        GameObject.FindObjectOfType<SubtitleHandler>().AddSubtitle(" ");
        //GameObject.FindObjectOfType<SubtitleHandler>().AddSubtitle(" ", true);
        director.Play();
    }

    public static void AfterVideo()
    {
        videoBg.SetActive(false);
        PlayerActive = true;
        GameIsActive = true;
        if (SaveGame != null) SaveGame();
    }
}
