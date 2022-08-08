using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowObjectBehaviour : MonoBehaviour
{
    private BoxCollider2D _parentBoxCollider;
    private Rigidbody2D _parentRigidBody;
    private DraggableObjectBehaviour _parentDraggableObjectBehaviour;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _parentBoxCollider = GetComponentInParent<BoxCollider2D>();
        _parentRigidBody = GetComponentInParent<Rigidbody2D>();
        _parentDraggableObjectBehaviour = GetComponentInParent<DraggableObjectBehaviour>();

        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _parentBoxCollider.enabled = true;
        _parentBoxCollider.size = new Vector2(_rectTransform.rect.width, _rectTransform.rect.height);

        _parentDraggableObjectBehaviour.OnBeginDraging += DisableRigidBody;
        _parentDraggableObjectBehaviour.OnEndDraging += EnablaRigidBody;
    }

    private void OnDisable()
    {
        _parentDraggableObjectBehaviour.OnBeginDraging -= DisableRigidBody;
        _parentDraggableObjectBehaviour.OnEndDraging -= EnablaRigidBody;
    }

    private void EnablaRigidBody()
    {
        _parentRigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void DisableRigidBody()
    {
        _parentRigidBody.bodyType = RigidbodyType2D.Static;
    }
}
