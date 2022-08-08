using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class DraggableObjectBehaviour : MonoBehaviour, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _onBeginDrag;
    [SerializeField] private UnityEvent _onEndDrag;

    public bool _isDragging { get; private set; }

    public event UnityAction OnBeginDraging
    {
        add => _onBeginDrag.AddListener(value);
        remove => _onBeginDrag.RemoveListener(value);
    }

    public event UnityAction OnEndDraging
    {
        add => _onEndDrag.AddListener(value);
        remove => _onEndDrag.RemoveListener(value);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _onBeginDrag?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        _onEndDrag?.Invoke();
    }
}
