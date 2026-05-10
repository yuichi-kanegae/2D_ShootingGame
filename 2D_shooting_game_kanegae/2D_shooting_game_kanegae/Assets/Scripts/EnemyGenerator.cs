using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    void Start()
    {
         InvokeRepeating("Spawn",2f,0.5f);
         //spawn関数を２秒後に0.5秒間隔で繰り返し実行
    }

    // Update is called once per frame

    void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-2.5f,2.5f),
            transform.position.y,
            transform.position.z
        );
        Instantiate(enemyPrefab,spawnPosition,transform.rotation);
           //instantiate(生成オブジェクト、生成位置、向き);
    }
    void Update()
    {
        
    }
}
