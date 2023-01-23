using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public static event Action NewEnemyScycle;
    
    [SerializeField] private float _spawnRadius = 5f;

    //public GameObject[] enemies;  CAN BE USED FOR MULTIPLE ENEMIES
    //[SerializeField] private GameObject[] _enemyPrefabs;

    [SerializeField] private GameObject _enemyMelee;
    [SerializeField] private GameObject _enemyRanged;
    [SerializeField] private GameObject _enemyLongRanged;
    [SerializeField] private GameObject _enemyBoss;
    
    
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DelayBeforeSpawning());
        //StartCoroutine(SpawnEnemy());  //can be commented
    }
    
    IEnumerator DelayBeforeSpawning()   //adds delay to show mini tutorial pop up
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(GameCycle());
    }
    
    IEnumerator GameCycle()
    {
        //spawns melee enemy only for 60 secs on .75s interval
        StartCoroutine(SpawnRandom(.75f, _enemyMelee,_enemyMelee,_enemyMelee));     //start
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnRandom(.75f, _enemyMelee,_enemyMelee,_enemyMelee));      // 1 min
        
        //pause spawning for 30 seconds
        yield return new WaitForSeconds(30);
        
        //spawns 2 random for 60 secs on x interval
        StartCoroutine(SpawnRandom(1.25f, _enemyMelee,_enemyMelee,_enemyRanged));   //1:30
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnRandom(1.25f, _enemyMelee,_enemyMelee,_enemyRanged));    //2:30
        
        //pause spawning for 30 seconds
        yield return new WaitForSeconds(30);
        
        SpawnEnemyObject(_enemyBoss);                                                                                           //3:00
        
        //pause spawning for 30 seconds
        yield return new WaitForSeconds(60);
        
        StartCoroutine(SpawnRandom(1.5f, _enemyMelee,_enemyRanged,_enemyLongRanged));//4:00
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnRandom(1.5f, _enemyMelee,_enemyRanged,_enemyLongRanged));//5:00
        
        //pause spawning for 30 seconds
        yield return new WaitForSeconds(30);
        
        StartCoroutine(SpawnRandom(3f, _enemyRanged,_enemyRanged,_enemyLongRanged));//5:30
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnRandom(3f, _enemyRanged,_enemyRanged,_enemyLongRanged)); //6:30
        
        //pause spawning for 30 seconds
        yield return new WaitForSeconds(60);
        
        SpawnEnemyObject(_enemyBoss);                                                                                           //7:30
        SpawnEnemyObject(_enemyBoss);
        
        yield return new WaitForSeconds(60);
        StartCoroutine(GameCycle());                                                                                       //restarts at 8:30
        
        NewEnemyScycle?.Invoke();
        
    }


    IEnumerator SpawnRandom(float interval, GameObject enemyspawn1, GameObject enemyspawn2, GameObject enemyspawn3)
    {
        yield return new WaitForSeconds(interval);
        GameObject[] list = new GameObject[3];
        list[0]= enemyspawn1;
        list[1]= enemyspawn2;
        list[2]= enemyspawn3;
        SpawnEnemyObject(list[(int)Random.Range(0f,3f)]);
        StartCoroutine(SpawnRandom(interval,enemyspawn1,enemyspawn2,enemyspawn3));  //loops
    }


    private void SpawnEnemyObject(GameObject gameObject)
    {
        Vector2 spawnPos = FindObjectOfType<PlayerMovement>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * _spawnRadius;
        
        GameObject enemy = ObjectPool.Instance.GetObject(gameObject, spawnPos);
        enemy.SetActive(true);
    }
}
