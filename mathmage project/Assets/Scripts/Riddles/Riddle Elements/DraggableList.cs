using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableList : MonoBehaviour, IDropHandler
{
    [SerializeField] List<Draggable> draggables;
    [SerializeField] RectTransform thisElement;
    [SerializeField] float paddingAmount = 5;
    float padding;
    public Canvas parentCanvas;
    Vector2 start;
    Vector2 max;

    void Start()
    {
        thisElement = GetComponent<RectTransform>();
        if (thisElement == null || thisElement.pivot != new Vector2(0, 1))
        {
            throw new System.ArgumentException("Pivot has to be in upper left corner!");
        }
        start = thisElement.position;
        max = start + ScaledSize(thisElement.rect.size);
        padding = ScaledSize(paddingAmount);
        PlaceDraggables();
    }

    void Update()
    {
        padding = ScaledSize(paddingAmount);
        max = start + ScaledSize(thisElement.rect.size);
    }

    float ScaledSize(float value)
    {
        return parentCanvas.scaleFactor * value;
    }

    Vector2 ScaledSize(Vector2 value)
    {
        return parentCanvas.scaleFactor * value;
    }

    void PlaceDraggables()
    {
        Vector2 position = start;
        for (int i = 0; i < draggables.Count; i++)
        {
            RectTransform draggableTransform = draggables[i].GetComponent<RectTransform>();
            Vector2 draggableSize = ScaledSize(draggableTransform.rect.size);
            if (i == 0)
            {
                position = GameFunctions.SamePlace(draggableTransform, thisElement, parentCanvas);
                position.x += padding;
                position.y -= padding;
            }
            else
            {
                position.x += padding + draggableSize.x / 2;
            }
            if (position.x + draggableSize.x / 2 > max.x)
            {
                position.x = GameFunctions.SamePlace(draggableTransform, thisElement, parentCanvas).x + padding;
                position.y -= padding + draggableSize.y;
            }
            draggableTransform.position = position;
            draggables[i].snapPosition = position;
            draggables[i].indexInList = i;
            //draggables[i].DraggableList = this;
            position.x += draggableSize.x / 2;
            draggables[i].Snap();
        }
    }

    public void AddDraggable(Draggable newDraggable)
    {
        if (newDraggable == null) return;
        RectTransform newDraggableTransform = newDraggable.GetComponent<RectTransform>();
        Vector2 newDraggableSize = ScaledSize(newDraggableTransform.rect.size);
        Draggable lastDraggable = null;
        if (draggables.Count > 0) lastDraggable = draggables[draggables.Count - 1];
        draggables.Add(newDraggable);
        Vector2 newDraggablePosition;
        if (lastDraggable != null)
        {
            RectTransform lastDraggableTransform = lastDraggable.GetComponent<RectTransform>();
            Vector2 lastDraggableSize = ScaledSize(lastDraggableTransform.rect.size);
            newDraggablePosition = lastDraggableTransform.position;
            newDraggablePosition.x += lastDraggableSize.x / 2 + padding + newDraggableSize.x / 2;
            if (newDraggablePosition.x > max.x)
            {
                newDraggablePosition.x = GameFunctions.SamePlace(newDraggableTransform, thisElement, parentCanvas).x + padding;
                newDraggablePosition.y -= padding + lastDraggableSize.y / 2 + newDraggableSize.y / 2;
            }
        }
        else
        {
            newDraggablePosition = GameFunctions.SamePlace(newDraggableTransform, thisElement, parentCanvas);
            newDraggablePosition.x += padding;
            newDraggablePosition.y -= padding;
        }
        newDraggable.snapPosition = newDraggablePosition;
        //newDraggable.ActualDraggableList = this;
        newDraggable.indexInList = draggables.Count - 1;
        newDraggable.Snap();
    }

    public void RemoveDraggable(int index)
    {
        draggables[index].indexInList = -1;
        draggables.RemoveAt(index);
        PlaceDraggables();
    }

    public void RemoveDraggable(Draggable draggableToRemove)
    {
        draggables.Remove(draggableToRemove);
        draggableToRemove.indexInList = -1;
        PlaceDraggables();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable newDraggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (newDraggable != null && newDraggable.DraggableList == this) AddDraggable(newDraggable);
    }
}