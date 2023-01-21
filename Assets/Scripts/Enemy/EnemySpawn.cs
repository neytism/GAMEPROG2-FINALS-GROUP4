using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject SmallEnemy;
    [SerializeField] private GameObject BigEnemy;

    [SerializeField] public float SmallInterval;
    [SerializeField] public float BigInterval;
    // [SerializeField] private float _spawnRadius = 5f;
    // [SerializeField] private float _timeInterval = 1f;

    
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(spawnEnemy(SmallInterval, SmallEnemy));
        StartCoroutine(spawnEnemy(BigInterval, BigEnemy));
    }
    

    IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        // yield return new WaitforSeconds(interval);
        // //GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), (0,0),Quaternion.identity)));
        // StartCoroutine(spawnEnemy(interval, enemy));
    }*/
        
}
