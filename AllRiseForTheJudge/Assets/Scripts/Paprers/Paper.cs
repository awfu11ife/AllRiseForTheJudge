using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Paper : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameArea;

    public bool IsChoiceMade { get; private set; }
    public bool IsGuilty { get; private set; }

    private void Start()
    {
        IsChoiceMade = false;
        IsGuilty = false;
    }

    public void SetName(string name)
    {
        _nameArea.text = name;
    }

    public void MakeChoice()
    {
        IsChoiceMade = !IsChoiceMade;
    }

    public void SetGuiltyOrInnocent()
    {
        IsGuilty = !IsGuilty;
    }
}
