using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnvironmentCharcteristicsSetter : MonoBehaviour
{
    public string[] townNames;
    public string[] years;
    public TextMeshProUGUI startingTownAndYearTMP;
    public string chosenTownName, chosenYear;

    public 
    void Start()
    {
        chosenTownName = townNames[Random.Range(0, townNames.Length)];
        chosenYear = years[Random.Range(0, years.Length)];

        startingTownAndYearTMP.text = chosenTownName + ", " + chosenYear;
        
    }
   
    
}
