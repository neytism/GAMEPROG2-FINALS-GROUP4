using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public static event Action<float> ExperienceCollected;
    public float xpAmount = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            ExperienceCollected?.Invoke(xpAmount);
            gameObject.SetActive(false);
        }
    }
}

//script must be attached to xp orb object
//every enemy drops this