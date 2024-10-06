using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SubtitleHandler : MonoBehaviour
{
    TextMeshProUGUI upperSubtitles;
    TextMeshProUGUI lowerSubtitles;
    [SerializeField] GameObject lowerBg;
    [SerializeField] GameObject upperBg;
    [SerializeField] int upperSubtitleMaxLength;
    [SerializeField] int lowerSubtitleMaxLength;

    bool activeUpperSubtitle = false;
    bool activeLowerSubtitle = false;

    Queue<string> upperSubtitleQueue = new();
    Queue<string> lowerSubtitleQueue = new();

    float upperStartTime;
    float upperShowTime;
    float upperElapsedTime
    {
        get { return Time.time - upperStartTime; }
    }
    float lowerStartTime;
    float lowerShowTime;
    float lowerElapsedTime
    {
        get { return Time.time - lowerStartTime; }
    }

    // int s = 0; // DEBUG!!!

    private void Start()
    {
        upperStartTime = Time.time;
        upperShowTime = 0;
        lowerStartTime = Time.time;
        lowerShowTime = 0;
        lowerSubtitles = lowerBg.GetComponentInChildren<TextMeshProUGUI>(true);
        upperSubtitles = upperBg.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Update()
    {
        if (upperElapsedTime > upperShowTime)
        {
            activeUpperSubtitle = false;
            if (upperSubtitleQueue.Count == 0) upperBg.SetActive(false);
            else ShowNextSubtitle(true);
        }
        if (lowerElapsedTime > lowerShowTime)
        {
            activeLowerSubtitle = false;
            if (lowerSubtitleQueue.Count == 0) lowerBg.SetActive(false);
            else ShowNextSubtitle(false);
        }
    }

    public void AddSubtitle(string subtitle, bool upper = false, bool interrupt = true)
    {
        if (interrupt)
        {
            if (upper)
            {
                upperSubtitleQueue.Clear();
                activeUpperSubtitle = false;
            }
            else
            {
                lowerSubtitleQueue.Clear();
                activeLowerSubtitle = false;
            }
        }
        if (subtitle == "" || subtitle == null) return;
        if (subtitle.Contains("\\\\"))
        {
            AddSubtitle(subtitle.Split("\\\\"), upper);
            return;
        }
        subtitle = subtitle.Replace("%", "<br>");
        if (upper)
        {
            string substring;
            for (int i = 0, j = 0; i < subtitle.Length && j < 1000; i += substring.Length, j++)
            {
                int remaining = subtitle.Length - 1 - i;
                if (remaining > upperSubtitleMaxLength)
                {
                    substring = subtitle.Substring(i, upperSubtitleMaxLength);
                        substring = substring.Substring(0, substring.LastIndexOf(' '));
                    upperSubtitleQueue.Enqueue(substring);
                }
                else
                {
                    substring = subtitle.Substring(i);
                    upperSubtitleQueue.Enqueue(substring);
                    break;
                }
            }
        }
        else
        {
            string substring;
            for (int i = 0; i < subtitle.Length; i += substring.Length)
            {
                int remaining = subtitle.Length - 1 - i;
                if (remaining > lowerSubtitleMaxLength)
                {
                    substring = subtitle.Substring(i, lowerSubtitleMaxLength);
                    substring = substring.Substring(0, substring.LastIndexOf(' '));
                    lowerSubtitleQueue.Enqueue(substring);
                }
                else
                {
                    substring = subtitle.Substring(i);
                    lowerSubtitleQueue.Enqueue(substring);
                    break;
                }
            }
        }
        ShowNextSubtitle(upper);
    }

    public void AddSubtitle(string[] subtitles, bool upper)
    {
        bool first = true;
        foreach (string subtitle in subtitles)
        {
            AddSubtitle(subtitle, upper, first);
            first = false;
        }
    }

    void ShowNextSubtitle(bool upper)
    {
        if(upper)
        {
            if (activeUpperSubtitle) return;
            upperBg.SetActive(true);
            activeUpperSubtitle = true;
            string subtitle = upperSubtitleQueue.Dequeue();
            upperSubtitles.text = subtitle;
            upperShowTime = subtitle.Length * 0.1f;
            upperStartTime = Time.time;
        }
        else
        {
            if (activeLowerSubtitle) return;
            lowerBg.SetActive(true);
            activeLowerSubtitle = true;
            string subtitle = lowerSubtitleQueue.Dequeue();
            lowerSubtitles.text = subtitle;
            lowerShowTime = subtitle.Length * 0.1f;
            lowerStartTime = Time.time;
        }
    }
}
