using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSpot : MonoBehaviour, IDropHandler
{
    [SerializeField] string[] acceptedValues;
    [SerializeField] string defaultValue;
    [SerializeField] TMP_InputField input;

    [SerializeField] bool droppable = true;
    [SerializeField] bool inputable = true;
    Draggable actualDraggable = null;

    [SerializeField] RectTransform thisTransform;

    [SerializeField] DraggableList[] acceptsFromLists;

    public string actualValue
    {
        get
        {
            if (actualDraggable == null)
            {
                if (input.text == "")
                {
                    return defaultValue;
                }
                return input.text;
            }
            else
            {
                return actualDraggable.Value;
            }
        }
    }
    public bool ValueIsCorrect
    {
        get
        {
            if (acceptedValues.Length == 0)
                return true;
            foreach (string value in acceptedValues)
            {
                if (actualValue == value)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public bool ContainsDraggable
    {
        get
        {
            if (actualDraggable == null) return false;
            return true;
        }
    }

    public bool IsEmpty
    {
        get
        {
            if (input.text.Length > 0 || ContainsDraggable) return false;
            return true;
        }
    }

    public Draggable ActualDraggable
    {
        get { return actualDraggable; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (droppable)
        {
            Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
            SetDraggable(draggable);
        }
        
    }

    bool Accaptable(Draggable draggable)
    {
        if (acceptsFromLists.Length == 0) return true;
        foreach (DraggableList list in acceptsFromLists) if (list == draggable.DraggableList) return true;
        return false;
    }

    public void SetDraggable(Draggable newDraggable)
    {
        if (newDraggable != null && Accaptable(newDraggable))
        {
            newDraggable.snapPosition = GameFunctions.SamePlace(newDraggable.GetComponent<RectTransform>(), thisTransform, newDraggable.DraggableList.parentCanvas);
            if (actualDraggable != null && newDraggable != actualDraggable)
            {
                actualDraggable.ManuallyAddToList();
                actualDraggable.RemoveFromDropSpot();
            }
            actualDraggable = newDraggable;
            actualDraggable.ActualDropSpot = this;
            input.gameObject.SetActive(false);
        }
    }

    public void RemoveDraggable()
    {
        actualDraggable = null;
        input.gameObject.SetActive(true);
    }

    void Start()
    {
        if (thisTransform == null) throw new System.Exception();
        if (!inputable) input.interactable = false;
        if (!inputable && !droppable) throw new System.Exception("DropSpot must be either inputable or droppable!");
    }

    public void IgnoreRaycast(bool ignore)
    {
        try
        {
            GetComponentInChildren<Image>().raycastTarget = !ignore;
            GetComponentInChildren<TextMeshProUGUI>().raycastTarget = !ignore;
            GetComponentInChildren<TMP_SelectionCaret>().raycastTarget = !ignore;
            if (actualDraggable != null) actualDraggable.IgnoreRaycast(ignore);
        }
        catch { return; }
    }

    public void ManualSnap()
    {
        if (actualDraggable != null)
        {
            actualDraggable.snapPosition = GameFunctions.SamePlace(actualDraggable.GetComponent<RectTransform>(), thisTransform, actualDraggable.DraggableList.parentCanvas);
            actualDraggable.Snap();
            actualDraggable.transform.SetAsLastSibling();
        }
    }

    public List<object> SaveData()
    {
        return new List<object>
        {
            ContainsDraggable ? actualDraggable.name : null,
            input.text
        };
    }

    public void LoadData(Draggable savedDraggable, string inputText)
    {
        SetDraggable(savedDraggable);
        input.text = inputText;
    }
}
