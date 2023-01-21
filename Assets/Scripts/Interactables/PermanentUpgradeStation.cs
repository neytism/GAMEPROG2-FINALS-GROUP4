using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgradeStation : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerInteraction.InteractedWithEnergy += DeactivateStation;
    }
    private void OnDisable()
    {
        PlayerInteraction.InteractedWithEnergy -= DeactivateStation;
    }

    private void DeactivateStation()
    {
        gameObject.SetActive(false);
    }
}
