using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWithSubSpots : Draggable
{
    [SerializeField] protected DropSpot[] subSpots;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        foreach (DropSpot ds in subSpots)
        {
            ds.IgnoreRaycast(true);
        }
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        foreach (DropSpot ds in subSpots)
        {
            ds.ManualSnap();
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        foreach (DropSpot ds in subSpots)
        {
            ds.IgnoreRaycast(false);
            ds.ManualSnap();
        }
    }

    public override void Snap()
    {
        base.Snap();
        foreach (DropSpot ds in subSpots) ds.ManualSnap();
    }
    public bool OneContainsDraggable
    {
        get
        {
            foreach (DropSpot ds in subSpots)
            {
                if (ds.ContainsDraggable) return true;
            }
            return false;
        }
    }

    public bool AllContainDraggable
    {
        get
        {
            foreach(DropSpot ds in subSpots)
            {
                if (!ds.ContainsDraggable) return false;
            }
            return true;
        }
    }
}
