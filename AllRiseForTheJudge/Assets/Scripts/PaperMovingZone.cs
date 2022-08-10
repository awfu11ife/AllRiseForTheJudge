using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperMovingZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent<RectTransform, MovingZoneChanger, float, float> _onCursorEnter;

    [SerializeField] private RectTransform _papersRectTransform;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private MovingZoneChanger _movingZoneChanger;

    [SerializeField] private float _xInvisibleZone;
    [SerializeField] private float _yInvisibleZone;

    public event UnityAction<RectTransform, MovingZoneChanger, float, float> OnCursorEnter
    {
        add => _onCursorEnter.AddListener(value);
        remove => _onCursorEnter.RemoveListener(value);
    }
  
    private void OnDisable()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onCursorEnter.Invoke(_rectTransform, _movingZoneChanger, _xInvisibleZone, _yInvisibleZone);
    }
}
