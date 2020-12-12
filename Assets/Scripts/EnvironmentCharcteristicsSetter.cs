using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnvironmentCharcteristicsSetter : MonoBehaviour
{
    public string[] townNames;
    public string[] years;
    public TextMeshProUGUI startingTownAndYearTMP;
    string chosenTownName, chosenYear;
    public string chosenLocationAndYear;
    void Start()
    {
        chosenTownName = townNames[Random.Range(0, townNames.Length)];
        chosenYear = years[Random.Range(0, years.Length)];
        chosenLocationAndYear = chosenTownName + ", " + chosenYear;
        startingTownAndYearTMP.text = chosenLocationAndYear;
        
    }
   
    
}
