using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] new string name;

    [SerializeField] Image thisElement;
    [SerializeField] RectTransform thisTransform;
    //[SerializeField] TextMeshProUGUI text;
    public Vector3 snapPosition;
    [SerializeField] DraggableList draggableList;
    public int indexInList = -1; // really shouldn't be public, DraggableList should be the only one to know about it...
    protected DropSpot actualDropSpot = null;

    DropSpot prevDropSpot = null;

    public DraggableList DraggableList
    {
        get { return  draggableList; }
    }
    public DropSpot ActualDropSpot
    {
        set
        {
            if (value != null)
            actualDropSpot = value;
        }
        //get { return actualDropSpot; }
    }

    public virtual string Value
    {
        get { return name; }
    }

    protected virtual void Start()
    {
        if (thisElement == null || thisTransform == null)
        {
            throw new System.Exception();
        }
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts) text.raycastTarget = false;
        //ManuallyAddToList();
    }

    public void ManuallyAddToList()
    {
        if (draggableList != null)
        {
            draggableList.AddDraggable(this);
        }
    }
    public void RemoveFromDropSpot()
    {
        if (actualDropSpot != null)
        {
            actualDropSpot.RemoveDraggable();
            actualDropSpot = null;
        }
        
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        IgnoreRaycast(true);
        RemoveRefs();
        transform.SetAsLastSibling();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {

        IgnoreRaycast(false);
        EmptyDropRestoreRefs();
        Snap();
    }

    // When dragging, remove all references, be it list or dropspot
    void RemoveRefs()
    {
        if (draggableList != null && indexInList != -1)
        {
            draggableList.RemoveDraggable(indexInList);
        }

        prevDropSpot = actualDropSpot;
        RemoveFromDropSpot();
    }
    
    // something about if there's no dropspot or list found by the end of the drop
    // then set it back to the previous dropspot, or if there isn't one, then to the previous list
    // One of the two has to exist
    // the name of the function: restore references (to either a dropspot or a list) if the drop is empty (meaning, it didn't land on a dropspot or list)
    void EmptyDropRestoreRefs()
    {
        if (actualDropSpot == null && indexInList == -1)
        {
            if (prevDropSpot != null)
            {
                actualDropSpot = prevDropSpot;
                actualDropSpot.SetDraggable(this);
            }
            else
            {
                ManuallyAddToList();
            }
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        Draggable newDraggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (newDraggable != null)
        {
            if (actualDropSpot != null)
            {
                actualDropSpot.SetDraggable(newDraggable);
                Snap();
            }
        }
    }

    public virtual void Snap()
    {
        thisTransform.position = snapPosition;
    }

    public virtual void IgnoreRaycast(bool ignore)
    {
        thisElement.raycastTarget = !ignore;
    }

}
