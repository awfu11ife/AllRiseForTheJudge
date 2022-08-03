using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZoneChanger : MonoBehaviour
{
    [SerializeField] private List<PaperMovingZone> _paperMovingZones = new List<PaperMovingZone>();

    private RectTransform _rectTransform;
    private List<DragDrop> _childObjects = new List<DragDrop>();
    private PaperMovingZone _currentSelectedMovingzone;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateChildList();
    }

    private void UpdateChildList()
    {
        foreach (Transform child in _rectTransform)
        {
            if (child.gameObject.TryGetComponent(out DragDrop dragDrop))
                _childObjects.Add(dragDrop);
        }
    }
}
