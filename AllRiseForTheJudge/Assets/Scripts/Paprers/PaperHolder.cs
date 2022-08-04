using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaperHolder : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;
    private Paper _currentDroptPaper;
    private DragDrop _dragDrop;

    public bool IsDrop { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out Paper paper))
        {
            /*if (paper.IsChoiceMade == true)
            {
                _currentDroptPaper = paper;
                _dragDrop = paper.GetComponent<DragDrop>();
                _dragDrop.OnPickedUp += OnPickUp;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;
                IsDrop = true;
            }*/
        }
    }

    public void OnPickUp()
    {
        IsDrop = false;
        _dragDrop.OnPickedUp -= OnPickUp;
    }
}
