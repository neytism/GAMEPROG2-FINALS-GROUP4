using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    public GameObject poolManager;
    public GameObject saveManager;
    public GameObject soundManager;

    void Awake()
    {
        Instantiate(poolManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(saveManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(soundManager, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    //This script creates GameObjects for Singletons
}
