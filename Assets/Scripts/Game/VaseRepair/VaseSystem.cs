using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VaseSystem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Image img;

    public GameObject hintForm;
    public float snapTolerance = 1f;

    private bool isDragging;
    public bool isInteractable = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        img = this.gameObject.GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = 1f;
        PieceLocCheck();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable)
        {
            isDragging = true;
        }
        
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
        PieceLocCheck();

    }

    private void PieceLocCheck()
    {

        if (Mathf.Abs(this.transform.localPosition.x - hintForm.transform.localPosition.x) <= snapTolerance &&
            Mathf.Abs(this.transform.localPosition.y - hintForm.transform.localPosition.y) <= snapTolerance)
        {
            Debug.Log("Shape matched");
            isInteractable = false;
            this.transform.localPosition = new Vector3(hintForm.transform.localPosition.x, hintForm.transform.localPosition.y, hintForm.transform.localPosition.z);
        }
    }
    
}
