using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Prisoner", menuName = "Prisoner/Prisoner Info", order = 51)]
public class PrisonerInfo : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => _name;
}
