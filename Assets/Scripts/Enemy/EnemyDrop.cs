using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private float _expChance = 0;
    [SerializeField] private float _energyOrbChance;
    [SerializeField] private float _stationChance = 0;
    
    [SerializeField] private GameObject _XPOrb;
    [SerializeField] private GameObject _energyOrb;
    [SerializeField] private GameObject _energyStation;
    
    private void DropExperienceOrb()
    {
        GameObject XPOrb = ObjectPool.Instance.GetObject(_XPOrb, transform.position);
        XPOrb.SetActive(true);
    }
    
    private void DropEnergyOrb()
    {
        GameObject energyOrb = ObjectPool.Instance.GetObject(_energyOrb, transform.position);
        energyOrb.SetActive(true);
    }
    
    private void DropEnergyStation()
    {
        GameObject energyStation = ObjectPool.Instance.GetObject(_energyStation, transform.position);
        energyStation.SetActive(true);
    }

    public void DropOrbChance()
    {
        float roll = Random.value;
        
        if (roll < _energyOrbChance)
        {
            DropEnergyOrb();
        }
        
        if (roll < _expChance)
        {
            DropExperienceOrb();
        }

        if (roll < _stationChance)
        {
            DropEnergyStation();
        }
        
    }
}
