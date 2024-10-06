using System.Collections.Generic;
using UnityEngine;

public class F3BookSpot : MonoBehaviour, IActivatable, ISaveable
{
    [SerializeField] string noBookText;
    [SerializeField] protected Vector3 snapPosition;
    [SerializeField] protected Vector3 snapRotation;
    [SerializeField] protected new string name;

    static string takeOutText  = "Kivétel";
    static string putInText = "Berakás";

    protected static F3PutBackBook book;

    static SubtitleHandler subtitleHandler;

    protected bool containsBook = false;
    


    public string ActivationText
    {
        get { if (containsBook) return takeOutText; else return putInText;  }
    }

    public string Name
    {
        get { return name; }
    }

    public bool ContainsBook { get { return containsBook; } }

    public void Activate()
    {
        if (GameFunctions.F3HasBook)
        {
            book.SetTransform(snapPosition, snapRotation);
            containsBook = true;
            GameFunctions.F3HasBook = false;
        }
        else if (containsBook)
        {
            book.ResetTransform();
            containsBook = false;
            GameFunctions.F3HasBook = true;
        }
        else
        {
            subtitleHandler.AddSubtitle(noBookText);
        }
    }

    void Start()
    {
        subtitleHandler = FindObjectOfType<SubtitleHandler>();
        if (book == null) book = FindObjectOfType<F3PutBackBook>();
    }

    public List<object> SaveData()
    {
        return new List<object>
        {
            containsBook
        };
    }

    public void LoadData(List<object> data)
    {
        containsBook = (bool)data[0];
        if (containsBook)
        {
            Debug.Log(book);
            book.SetTransform(snapPosition, snapRotation);
            GameFunctions.F3HasBook = false;
        }
    }
}
