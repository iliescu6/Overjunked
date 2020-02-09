using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image energyBar;
    public float maxEnergy;
    public float currentEnergy;
    public float difficultyOverTime=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentEnergy -= Time.deltaTime* difficultyOverTime;
        energyBar.fillAmount = currentEnergy/maxEnergy;
    }
}
