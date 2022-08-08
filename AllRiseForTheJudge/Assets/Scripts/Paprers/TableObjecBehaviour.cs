using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObjecBehaviour : MonoBehaviour
{
    private DraggableObjectBehaviour _parentObjectBeehaviour;
    private Rigidbody2D _parentRigidbody2D;
    private BoxCollider2D _parentBoxCollider;

    private void Awake()
    {
        _parentRigidbody2D = GetComponentInParent<Rigidbody2D>();
        _parentBoxCollider = GetComponentInParent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _parentRigidbody2D.bodyType = RigidbodyType2D.Static;
        _parentBoxCollider.enabled = false;
    }   
}
