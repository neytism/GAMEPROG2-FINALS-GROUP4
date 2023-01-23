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
        StartCoroutine(SpawnRandom(.75f, _enemyMelee,_enemyMelee,_enemyMelee));
        yield return new WaitForSeconds(75);
        StopCoroutine(SpawnRandom(.75f, _enemyMelee,_enemyMelee,_enemyMelee));
        
        StartCoroutine(SpawnRandom(1.25f, _enemyMelee,_enemyMelee,_enemyRanged));
        yield return new WaitForSeconds(75);
        StopCoroutine(SpawnRandom(1.25f, _enemyMelee,_enemyMelee,_enemyRanged));
        
        StartCoroutine(SpawnRandom(1.5f, _enemyMelee,_enemyRanged,_enemyLongRanged));
        yield return new WaitForSeconds(75);
        StopCoroutine(SpawnRandom(1.5f, _enemyMelee,_enemyRanged,_enemyLongRanged));
        
        StartCoroutine(SpawnRandom(3f, _enemyRanged,_enemyRanged,_enemyLongRanged));
        yield return new WaitForSeconds(75);
        StopCoroutine(SpawnRandom(3f, _enemyRanged,_enemyRanged,_enemyLongRanged));
        
        SpawnEnemyObject(_enemyBoss);

        yield return new WaitForSeconds(30);
        StartCoroutine(GameCycle());
        
        NewEnemyScycle?.Invoke();
        
    }


    IEnumerator SpawnRandom(float interval, GameObject enemyspawn1, GameObject enemyspawn2, GameObject enemyspawn3)
    {
        yield return new WaitForSeconds(interval);
        GameObject[] list = new GameObject[3];
        list[0]= enemyspawn1;
        list[1]= enemyspawn2;
        list[2]= enemyspawn3;
        SpawnEnemyObject(list[(int)Random.Range(0f,2f)]);
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
