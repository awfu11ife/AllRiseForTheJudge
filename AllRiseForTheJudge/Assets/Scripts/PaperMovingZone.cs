using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperMovingZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent<RectTransform, RectTransform, float, float> _onCursorEnter;

    [SerializeField] private RectTransform _papersRectTransform;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private float _xInvisibleZone;
    [SerializeField] private float _yInvisibleZone;

    public event UnityAction<RectTransform, RectTransform, float, float> OnCursorEnter
    {
        add => _onCursorEnter.AddListener(value);
        remove => _onCursorEnter.RemoveListener(value);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onCursorEnter.Invoke(_rectTransform, _papersRectTransform, _xInvisibleZone, _yInvisibleZone);
    }
}
