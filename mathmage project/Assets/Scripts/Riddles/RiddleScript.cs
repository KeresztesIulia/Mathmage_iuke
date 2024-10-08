using System.Collections.Generic;
using UnityEngine;

public abstract class Riddle : Closeable, ISaveable
{
    [Header("For saving")]
    [SerializeField] protected DropSpot[] allDropSpots;
    [SerializeField] Draggable[] allDraggables;
    Dictionary<string, Draggable> draggableDict = new();

    protected string[] prevValues;
    protected bool checkCorrectnessAtUpdate = true;

    
    protected bool solved = false;

    [Header("Other")]
    [SerializeField] string[] hints; 
    [SerializeField] GameObject correctBorder;
    int hintIndex = -1;
    protected SubtitleHandler subtitleHandler;

    bool savedSolved = false;

    public string nextHint {  get {
            if (hints.Length == 0) return "";
            hintIndex++;
            if (hintIndex == hints.Length)
            {
                hintIndex = 0;
            }
            return hints[hintIndex];
        } }

    public abstract bool Correct {  get; }
    public bool Solved { get { return solved; } }

    protected virtual bool ValuesChanged
    {
        get
        {
            bool changed = false;
            for (int i = 0; i < prevValues.Length; i++)
            {
                if (prevValues[i] != allDropSpots[i].actualValue)
                {
                    
                    prevValues[i] = allDropSpots[i].actualValue;
                    changed = true;
                }
            }
            return changed;
        }
    }

    protected virtual void Start()
    {
        subtitleHandler = FindAnyObjectByType<SubtitleHandler>();
        InitializePrevValues();
        BuildDraggableDictionary();
        ChangeDesign();
        Close();
    }

    private void InitializePrevValues()
    {
        prevValues = new string[allDropSpots.Length];
        for (int i = 0; i < prevValues.Length; i++) prevValues[i] = null;
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            React();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //GameFunctions.PlayerActive = true;
            Close();
        }

        if (checkCorrectnessAtUpdate && ValuesChanged) ChangeDesign();
    }

    private void OnEnable()
    {
        GameFunctions.SaveGame += delegate
        {
            if (Correct) savedSolved = true;
        };
    }

    private void OnDisable()
    {
        GameFunctions.SaveGame -= delegate
        {
            if (Correct) savedSolved = true;
        };
    }

    void BuildDraggableDictionary()
    {
        foreach (Draggable draggable in allDraggables) draggableDict.Add(draggable.name, draggable);
    }

    void React()
    {
        if (subtitleHandler != null && hints.Length > 0)
        {
            subtitleHandler.AddSubtitle(nextHint, true);
        }
    }

    public virtual void ChangeDesign()
    {
        if (Correct)
        {
            solved = true;
            if (!savedSolved && GameHandler.GameInited) SaveOnSolved();
            if (correctBorder != null) correctBorder.SetActive(true);
        }
        else
        {
            if (correctBorder != null) correctBorder.SetActive(false);
        }
    }


    void SaveOnSolved()
    {
        savedSolved = true;
        if (GameFunctions.SaveGame != null) GameFunctions.SaveGame();
    }

    public virtual List<object> SaveData()
    {
        List<object> saveData = new List<object>{ solved };
        foreach (DropSpot ds in allDropSpots) saveData.Add(ds.SaveData());
        return saveData;
    }
    public virtual void LoadData(List<object> data)
    {
        solved = (bool)data[0];
        int i = 1;
        foreach (DropSpot ds in allDropSpots)
        {
            List<object> dsData = (List<object>)data[i];
            string draggableName = (string)dsData[0];
            string inputText = (string)dsData[1];
            if (draggableName != null)
            {
                Draggable draggable = draggableDict[draggableName];
                ds.LoadData(draggable, inputText);
                draggable.DraggableList.RemoveDraggable(draggable);
                draggable.Snap();
            }
            else
            {
                ds.LoadData(null, inputText);
            }
            i++;
        }
    }
}
