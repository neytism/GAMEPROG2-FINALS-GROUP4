using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action InteractedWithEnergy;
    [SerializeField] private GameObject _resonateText;

    private bool _isNear = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isNear)
        {
           InteractedWithEnergy?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PermaUpgradeStation"))
        {
            _isNear = true;
            _resonateText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PermaUpgradeStation"))
        {
            _isNear = false;
            _resonateText.SetActive(false);
        }
    }
}
