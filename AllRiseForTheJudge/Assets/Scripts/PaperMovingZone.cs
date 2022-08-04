using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperMovingZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent<RectTransform, RectTransform> _onCursorEnter;
    [SerializeField] private RectTransform _papersRectTransform;

    public event UnityAction<RectTransform, RectTransform> OnCursorEnter
    {
        add => _onCursorEnter.AddListener(value);
        remove => _onCursorEnter.RemoveListener(value);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onCursorEnter.Invoke((RectTransform)eventData.pointerEnter.gameObject.transform, _papersRectTransform);
    }
}
