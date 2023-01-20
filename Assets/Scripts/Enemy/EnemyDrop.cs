using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    
    [SerializeField] private float _energyOrbChance;
    
    [SerializeField] private GameObject _XPOrb;
    [SerializeField] private GameObject _energyOrb;
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

    public void DropOrbChance()
    {
        float roll = Random.value;
        if (roll < _energyOrbChance)
        {
            DropEnergyOrb();
        }
        else
        {
            DropExperienceOrb();
        }
    }
}
