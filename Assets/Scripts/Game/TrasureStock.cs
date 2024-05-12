using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TrasureStock : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Image img;

    public Image chest;
    public string displayName;
    

    private bool isDragging;
    public bool isInChest = false;
    Vector3[] cornersItem;
    Vector3[] cornersChest;
    RectTransform rectItem;
    RectTransform rectChest;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        //ignores transparent parts of image and allows to interact with opague parts
        img = this.gameObject.GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = 1f;

        rectItem = img.rectTransform;
        rectChest = chest.rectTransform;

        cornersItem = new Vector3[4];
        rectItem.GetWorldCorners(cornersItem);
        cornersChest = new Vector3[4];
        rectChest.GetWorldCorners(cornersChest);


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        ItemLocCheck();

    }

    private void ItemLocCheck()
    {
        if (IsRectFullyInsideRect())
        {
            Debug.Log("Inside Chest!");
            isInChest = true;
        }
    }
    bool IsPointInsideRect(Vector3 point, RectTransform rectTransform)
    {
        // Convert point to local position of the RectTransform
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, point, null, out localPoint);

        // Check if the point is inside the rect
        return rectTransform.rect.Contains(localPoint);
    }

    bool IsRectFullyInsideRect()
    {
        

        // Check if all corners of the first rectangle are inside the second rectangle
        foreach (Vector3 corner in cornersItem)
        {
            if (!IsPointInsideRect(corner, rectChest))
            {
                return false;
            }
        }

        return true;
    }

}
