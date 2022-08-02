using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prisoner : MonoBehaviour
{
    [SerializeField] private PrisonerInfo _prisonerInfo;
    [SerializeField] private Paper _paper;
    [SerializeField] private TMP_Text _nameArea;

    private void Start()
    {
        _nameArea.text = _prisonerInfo.Name;
        _paper.SetName(_prisonerInfo.Name);
    }
}
