using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Copyright © 2022 Kyo Matias & Nate Florendo. All rights reserved.
//  

public class Instance : MonoBehaviour
{
    public GameObject poolManager;

    void Awake()
    {
        Instantiate(poolManager, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    //This script creates GameObjects for Singletons
}
