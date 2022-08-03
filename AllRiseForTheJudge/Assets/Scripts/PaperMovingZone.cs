using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperMovingZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityEvent<GameObject> _onCursorEnter;

    public event UnityAction<GameObject> OnCursorEnter
    {
        add => _onCursorEnter.AddListener(value);
        remove => _onCursorEnter.RemoveListener(value);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData.pointerEnter.gameObject.name);
        _onCursorEnter.Invoke(eventData.pointerEnter);
    }
}
