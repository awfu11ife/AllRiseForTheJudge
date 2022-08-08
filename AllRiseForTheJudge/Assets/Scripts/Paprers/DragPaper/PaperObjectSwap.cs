using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PaperObjectSwap : MonoBehaviour
{
    [SerializeField] private List<Paper> _paperTypes = new List<Paper>();

    private RectTransform _currentSelectedPaperTypeRectTransform;
    private RectTransform _rectTransform;
    private MovingZoneChanger _currentMovingZoneChanger;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _currentMovingZoneChanger = GetComponentInParent<MovingZoneChanger>();
        _currentSelectedPaperTypeRectTransform = _currentMovingZoneChanger.GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdateRectTransform(_currentSelectedPaperTypeRectTransform);
    }

    public void Swap(int objectNumber)
    {
        foreach (var item in _paperTypes)
            item.gameObject.SetActive(false);

        if (_paperTypes.Count > objectNumber)
        {
            _paperTypes[objectNumber].gameObject.SetActive(true);
            _currentSelectedPaperTypeRectTransform = _paperTypes[objectNumber].gameObject.GetComponent<RectTransform>();
            UpdateRectTransform(_currentSelectedPaperTypeRectTransform);
        }
    }

    private void UpdateRectTransform(RectTransform rectTransformTempLate)
    {
        _rectTransform.sizeDelta = rectTransformTempLate.sizeDelta;
    }
}
