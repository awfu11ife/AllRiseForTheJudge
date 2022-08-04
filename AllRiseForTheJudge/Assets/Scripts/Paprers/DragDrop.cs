using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [Header("Components")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _draggingArea;
    [SerializeField] private RectTransform _currentParentRectTransform;

    [Header("Mover Variables")]
    [SerializeField] private float _xVisibleArea;
    [SerializeField] private float _yVisibleArea;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPickedUp;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private float _draggingAreaWidth;
    private float _draggingAreaHeight;

    private float _maxXPosition;
    private float _minXPosition;
    private float _maxYPosition;
    private float _minYPosition;

    public bool IsDragging { get; private set; }

    public event UnityAction OnPickedUp
    {
        add => _onPickedUp.AddListener(value);
        remove => _onPickedUp.RemoveListener(value);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        UpdateDraggingArea(_draggingArea, _currentParentRectTransform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _onPickedUp?.Invoke();
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        MoveInsideBorders();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        IsDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.SetAsLastSibling();
    }

    public void UpdateDraggingArea(RectTransform draggingArea, RectTransform parentRectTransform)
    {
        _draggingArea = draggingArea;
        _rectTransform.SetParent(parentRectTransform.gameObject.transform);

        _draggingAreaWidth = draggingArea.rect.width;
        _draggingAreaHeight = draggingArea.rect.height;

        _maxXPosition = _draggingAreaWidth / 2 - _rectTransform.rect.x * _xVisibleArea;
        _minXPosition = -_draggingAreaWidth / 2 + _rectTransform.rect.x * _xVisibleArea;
        _maxYPosition = _draggingAreaHeight / 2 - _rectTransform.rect.y * _yVisibleArea;
        _minYPosition = -_draggingAreaHeight / 2 + _rectTransform.rect.y * _yVisibleArea;
    }

    private void MoveInsideBorders()
    {
        var paprerPosition = _rectTransform.anchoredPosition;

        paprerPosition.x = Mathf.Clamp(paprerPosition.x, _minXPosition, _maxXPosition);
        paprerPosition.y = Mathf.Clamp(paprerPosition.y, _minYPosition, _maxYPosition);

        _rectTransform.anchoredPosition = new Vector2(paprerPosition.x, paprerPosition.y);
    }

}
