using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableList_old : MonoBehaviour, IDropHandler
{
    [SerializeField] List<Draggable> draggables; // same heights!
    [SerializeField] RectTransform thisElement;
    [SerializeField] float paddingPercentage = 0;
    float paddings;
    float maxX;
    float minY;
    float startX;

    // Canvas parentCanvas;
    Vector3 scaledDeltaSize;


    Vector3 scaledSize(Vector3 size)
    {
        return size / 2; //* parentCanvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable newDraggable = eventData.pointerDrag.GetComponent<Draggable>();
        AddDraggable(newDraggable); 
    }
    
    public void AddDraggable(Draggable newDraggable)
    {
        if (newDraggable != null)
        {
            RectTransform newDraggableRect = newDraggable.GetComponent<RectTransform>();
            Vector3 newDraggableScaledSize = scaledSize(newDraggableRect.sizeDelta);
            Draggable lastDraggable = null;
            if (draggables.Count > 0) lastDraggable = draggables[draggables.Count - 1];
            draggables.Add(newDraggable);
            Vector2 newDraggablePosition;
            if (lastDraggable != null)
            {
                RectTransform lastDraggableRect = lastDraggable.GetComponent<RectTransform>();
                Vector3 lastDraggableScaledSize = scaledSize(lastDraggableRect.sizeDelta);
                newDraggablePosition = lastDraggableRect.anchoredPosition;
                newDraggablePosition.x += lastDraggableScaledSize.x + paddings + newDraggableScaledSize.x;
                if (newDraggablePosition.x + paddings + newDraggableScaledSize.x > maxX)
                {
                    newDraggablePosition.x = startX + newDraggableScaledSize.x;
                    newDraggablePosition.y -= paddings + 2 * lastDraggableScaledSize.y;
                }
            }
            else
            {
                newDraggablePosition = new Vector3();
                newDraggablePosition.x = startX + newDraggableScaledSize.x;
                newDraggablePosition.y = thisElement.anchoredPosition.y + scaledDeltaSize.y - paddings - newDraggableScaledSize.y;
            }

            newDraggable.snapPosition = newDraggablePosition;
            //newDraggable.ActualDraggableList = this;
            newDraggable.indexInList = draggables.Count - 1;
        }
    }

    public void RemoveDraggable(int index)
    {
        draggables[index].indexInList = -1;
        draggables.RemoveAt(index);
        PlaceDraggables();
    }

    void PlaceDraggables()
    {
        float xPosition = startX;
        float yPosition = thisElement.anchoredPosition.y + scaledDeltaSize.y - paddings;

        for (int i = 0; i < draggables.Count; i++)
        {
            if (yPosition < minY) throw new System.NotSupportedException();

            RectTransform draggableTransform = draggables[i].GetComponent<RectTransform>();
            if (draggableTransform == null) throw new System.NotSupportedException();

            Vector2 draggableScaledSize = scaledSize(draggableTransform.sizeDelta);

            if (i == 0) yPosition -= draggableScaledSize.y;
            xPosition += draggableScaledSize.x;
            if (xPosition + draggableScaledSize.x + paddings > maxX)
            {
                yPosition -= paddings + 2 * draggableScaledSize.y;
                xPosition = startX + draggableScaledSize.x;
            }
            draggables[i].snapPosition = new Vector3(xPosition, yPosition);
            //if (i == 0) draggables[i].snapPosition = thisElement.anchoredPosition + draggableScaledSize;
            //else draggables[i].snapPosition = thisElement.anchoredPosition;
            draggableTransform.anchoredPosition = draggables[i].snapPosition;
            draggables[i].indexInList = i;
            //draggables[i].ActualDraggableList = this;
            xPosition += draggableScaledSize.x + paddings;
            
        }
    }

    void Start()
    {
        //Transform parent = transform.parent;
        //while (parent != null)
        //{
        //    parentCanvas = parent.GetComponent<Canvas>();
        //    if (parentCanvas != null) break;
        //    parent = parent.parent;
        //}
        //if (parentCanvas == null) throw new System.Exception();

        if (thisElement == null)
        {
            thisElement = GetComponent<RectTransform>();
            if (thisElement == null) throw new System.Exception();
        }
        if (paddingPercentage > 1 || paddingPercentage < 0) throw new System.Exception();

        scaledDeltaSize = scaledSize(thisElement.sizeDelta);

        paddings = scaledDeltaSize.x * paddingPercentage;

        maxX = thisElement.anchoredPosition.x + scaledDeltaSize.x - paddings;
        minY = thisElement.anchoredPosition.y - scaledDeltaSize.y + paddings;

        startX = thisElement.anchoredPosition.x - scaledDeltaSize.x + paddings;
        PlaceDraggables();
    }

    void Update()
    {
        paddings = scaledDeltaSize.x * paddingPercentage;
        
        maxX = thisElement.anchoredPosition.x + scaledDeltaSize.x - paddings;
        minY = thisElement.anchoredPosition.y - scaledDeltaSize.y + paddings;
        startX = thisElement.anchoredPosition.x - scaledDeltaSize.x + paddings;
    }
}
