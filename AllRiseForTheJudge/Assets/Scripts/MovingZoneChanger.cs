using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZoneChanger : MonoBehaviour
{
    [SerializeField] private List<PaperMovingZone> _paperMovingZones = new List<PaperMovingZone>();
    [SerializeField] private int _paperTemplateObjectInedx;

    private RectTransform _rectTransform;
    private List<DragDrop> _childObjects = new List<DragDrop>();

    public int PaperTemplateObjectIndex => _paperTemplateObjectInedx;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        foreach (var item in _paperMovingZones)
        {
            item.OnCursorEnter += FindMovingObject;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _paperMovingZones)
        {
            item.OnCursorEnter -= FindMovingObject;
        }
    }

    private void Start()
    {
        UpdateChildList();
    }

    public void FindMovingObject(RectTransform rectTransform, MovingZoneChanger movingZoneChanger, float xInvisibleZone, float yInvisibleZone)
    {
        UpdateChildList();

        foreach (var item in _childObjects)
        {
            if (item.IsDragging == true)
            {
                item.GetComponent<PaperObjectSwap>().Swap(movingZoneChanger.PaperTemplateObjectIndex);
                item.UpdateDraggingArea(rectTransform, movingZoneChanger.GetComponent<RectTransform>(), xInvisibleZone, yInvisibleZone);
                item.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    private void UpdateChildList()
    {
        _childObjects.Clear();

        foreach (Transform child in _rectTransform)
        {
            if (child.gameObject.TryGetComponent(out DragDrop dragDrop))
                _childObjects.Add(dragDrop);
        }
    }
}
