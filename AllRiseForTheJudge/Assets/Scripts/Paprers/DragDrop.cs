using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Components")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _table;

    [Header("Mover Variables")]
    [SerializeField] private float _xVisibleArea;
    [SerializeField] private float _yVisibleArea;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPickedUp;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool _isPointerOnObject;
    private float _tableWidth;
    private float _tableHeight;

    private float _maxXPosition;
    private float _minXPosition;
    private float _maxYPosition;
    private float _minYPosition;


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
        _tableWidth = _table.rect.width;
        _tableHeight = _table.rect.height;

        _maxXPosition = _tableWidth / 2 - _rectTransform.rect.x * _xVisibleArea;
        _minXPosition = -_tableWidth / 2 + _rectTransform.rect.x * _xVisibleArea;
        _maxYPosition = _tableHeight / 2 - _rectTransform.rect.y * _yVisibleArea;
        _minYPosition = -_tableHeight / 2 + _rectTransform.rect.y * _yVisibleArea;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _onPickedUp?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        MoveInsideBorders();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    private void MoveInsideBorders()
    {
        var paprerPosition = _rectTransform.anchoredPosition;

        paprerPosition.x = Mathf.Clamp(paprerPosition.x, _minXPosition, _maxXPosition);
        paprerPosition.y = Mathf.Clamp(paprerPosition.y, _minYPosition, _maxYPosition);

        _rectTransform.anchoredPosition = new Vector2(paprerPosition.x, paprerPosition.y);
    }
}
