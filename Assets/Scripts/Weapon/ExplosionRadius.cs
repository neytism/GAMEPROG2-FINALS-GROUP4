using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(RadiusLife(gameObject));
    }
    
    IEnumerator RadiusLife(GameObject explosionRadius)
    {
        yield return new WaitForSeconds(0.1f);
        explosionRadius.SetActive(false);
    }
}
