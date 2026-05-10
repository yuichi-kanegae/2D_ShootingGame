using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    GameController gameController;
    public GameObject explosion; 
    public Transform firePointEnemy;
    public GameObject bulletPrefabEnemy;

    public float Destroytime = 7.0f;
    public float interval = 0.5f; // 玉の発射間隔
    private float timer = 0.0f;

    public int hp = 1;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if(Destroytime != 0)
        {
        //モブの削除時間・ボスはDestroytime=0
        Invoke(nameof(Destroy),Destroytime);
        }
    }

    void Update()
    {
        transform.position -= new Vector3(0,Time.deltaTime,0);
        Shot();
    }

    void Shot()
    {
        if (timer <= 0.0f)
        {
            Instantiate(bulletPrefabEnemy,firePointEnemy.position, transform.rotation);
            timer = interval;
        }
        //球を発射する間隔のタイマー
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
    }
    
    //当たり判定と弾が当たったら爆発
    private void OnTriggerEnter2D(Collider2D collision)
    {          
            if (collision.CompareTag("Player") == true)
            {
                Instantiate(explosion,collision.transform.position,transform.rotation);
                gameController.GameOver();
                Destroy(gameObject);
                Destroy(collision.gameObject);
                Instantiate(explosion,transform.position,transform.rotation);
            }
            else if (collision.CompareTag("Bullet") == true)
            {
		        // ヒットポイントを減らす
		        hp = hp - 1;
		        // 弾の削除
		        Destroy(collision.gameObject);
		
		        // ヒットポイントが0以下であれば
		        if(hp == 0)
		        {
                 gameController.AddScore();
                 Destroy(gameObject);
                 Destroy(collision.gameObject);
                 Instantiate(explosion,transform.position,transform.rotation);
                }
            }
            
    }

    void Destroy(){
        Destroy(gameObject);
    }

    
    
    
}
