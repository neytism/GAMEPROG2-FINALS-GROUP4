using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject spawner;
    public GameObject doorlock;
    private void OnTriggerEnter2D(Collider2D col)
    {
        spawner.SetActive(true);
        doorlock.SetActive(true);
    }
}
