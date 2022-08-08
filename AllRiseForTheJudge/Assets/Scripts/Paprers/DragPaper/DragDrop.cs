using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [Header("Mover Variables")]
    [SerializeField] private float _xInvisibleArea;
    [SerializeField] private float _yInvisibleArea;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPickedUp;

    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private PaperMovingZone _currentPaperMovingZone;
    private MovingZoneChanger _currentPaperHolder;
    private RectTransform _currentDraggingArea;
    private RectTransform _currentParentRectTransform;

    private float _draggingAreaWidth;
    private float _draggingAreaHeight;

    private float _maxXPosition;
    private float _minXPosition;
    private float _maxYPosition;
    private float _minYPosition;

    private float _previousMousePositionX;
    private float _previousMousePositionY;

    private bool _blockRotationMaxX = false;
    private bool _blockRotationMinX = false;
    private bool _blockRotationMaxY = false;
    private bool _blockRotationMinY = false;


    private bool _stopCountMousePositionX = false;
    private bool _stopCountMousePositionY = false;

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
        _canvas = GetComponentInParent<Canvas>();
        _currentPaperMovingZone = GetComponentInParent<PaperMovingZone>();
        _currentPaperHolder = GetComponentInParent<MovingZoneChanger>();
        _currentDraggingArea = _currentPaperMovingZone.GetComponent<RectTransform>();
        _currentParentRectTransform = _currentPaperHolder.GetComponent<RectTransform>();

    }

    private void Start()
    {
        UpdateDraggingArea(_currentDraggingArea, _currentParentRectTransform, _xInvisibleArea, _yInvisibleArea);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _onPickedUp?.Invoke();
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveInsideBorders(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        IsDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.SetAsLastSibling();

        _previousMousePositionX = Input.mousePosition.x;
        _previousMousePositionY = Input.mousePosition.y;

        _blockRotationMaxX = false;
        _blockRotationMinX = false;
        _blockRotationMinY = false;
        _blockRotationMaxY = false;
    }

    public void UpdateDraggingArea(RectTransform draggingArea, RectTransform parentRectTransform, float xInvisibleZone, float yInvizibleZone)
    {
        _currentDraggingArea = draggingArea;
        _rectTransform.SetParent(parentRectTransform.gameObject.transform);

        _draggingAreaWidth = draggingArea.rect.width;
        _draggingAreaHeight = draggingArea.rect.height;

        _xInvisibleArea = xInvisibleZone;
        _yInvisibleArea = yInvizibleZone;

        _maxXPosition = _draggingAreaWidth / 2 - _rectTransform.rect.x * _xInvisibleArea;
        _minXPosition = -_draggingAreaWidth / 2 + _rectTransform.rect.x * _xInvisibleArea;
        _maxYPosition = _draggingAreaHeight / 2 - _rectTransform.rect.y * _yInvisibleArea;
        _minYPosition = -_draggingAreaHeight / 2 + _rectTransform.rect.y * _yInvisibleArea;

        _blockRotationMaxX = false;
        _blockRotationMinX = false;
        _blockRotationMinY = false;
        _blockRotationMaxY = false;
    }

    private void MoveInsideBorders(PointerEventData pointerData)
    {
        var paprerPosition = _rectTransform.anchoredPosition;


        if (_stopCountMousePositionX == false)
            _previousMousePositionX = Input.mousePosition.x;

        if (_blockRotationMaxX == false && _blockRotationMinX == false)
            paprerPosition.x += pointerData.delta.x / _canvas.scaleFactor;

        if (_stopCountMousePositionY == false)
            _previousMousePositionY = Input.mousePosition.y;

        if (_blockRotationMaxY == false && _blockRotationMinY == false)
            paprerPosition.y += pointerData.delta.y / _canvas.scaleFactor;

        paprerPosition.x = Mathf.Clamp(paprerPosition.x, _minXPosition, _maxXPosition);
        paprerPosition.y = Mathf.Clamp(paprerPosition.y, _minYPosition, _maxYPosition);

        _rectTransform.anchoredPosition = new Vector2(paprerPosition.x, paprerPosition.y);

        BlockRotationOnMouseOut();
    }

    private void BlockRotationOnMouseOut()
    {
        if (_rectTransform.anchoredPosition.x != _maxXPosition && _rectTransform.anchoredPosition.x != _minXPosition)
        {
            _stopCountMousePositionX = false;
        }
        else
        {
            _stopCountMousePositionX = true;

            if (_rectTransform.anchoredPosition.x == _maxXPosition && _previousMousePositionX < Input.mousePosition.x)
                _blockRotationMaxX = true;
            else
                _blockRotationMaxX = false;

            if (_rectTransform.anchoredPosition.x == _minXPosition && _previousMousePositionX > Input.mousePosition.x)
                _blockRotationMinX = true;
            else
                _blockRotationMinX = false;
        }

        if (_rectTransform.anchoredPosition.y != _maxYPosition && _rectTransform.anchoredPosition.y != _minYPosition)
        {
            _stopCountMousePositionY = false;
        }
        else
        {
            _stopCountMousePositionY = true;

            if (_rectTransform.anchoredPosition.y == _maxYPosition && _previousMousePositionY < Input.mousePosition.y)
                _blockRotationMaxY = true;
            else
                _blockRotationMaxY = false;

            if (_rectTransform.anchoredPosition.y == _minYPosition && _previousMousePositionY > Input.mousePosition.y)
                _blockRotationMinY = true;
            else
                _blockRotationMinY = false;
        }
    }
}
