using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextLife : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(HideDamage());
    }

    IEnumerator HideDamage()
    {
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
    }
}

// can put animation in text