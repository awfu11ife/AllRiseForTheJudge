using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgesGavel : MonoBehaviour
{
    [SerializeField] private PaperHolder _paperHolder;

    public void ShowInfo()
    {
        print(_paperHolder.IsDrop);
    }
}
