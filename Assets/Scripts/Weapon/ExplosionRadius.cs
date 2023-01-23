using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(ColliderLife());
    }
    
    IEnumerator RadiusLife(GameObject explosionRadius)
    {
        yield return new WaitForSeconds(1.5f);
        explosionRadius.SetActive(false);
    }

    IEnumerator ColliderLife()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(RadiusLife(gameObject));
    }
}
