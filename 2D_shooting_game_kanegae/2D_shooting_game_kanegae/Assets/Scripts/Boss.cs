using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : MonoBehaviour
{
    GameController gameController;
    public GameObject explosion; 
    public Transform firePointBossMiddle;
    public Transform firePointBossRight;
    public Transform firePointBossLeft;
    public GameObject bulletPrefabEnemy;

    public float Destroytime = 0f;
    public float interval = 0.1f; // 何秒間隔で撃つか
    private float timer = 0.0f;
    private float Addtimer = 0.0f;

    public int hp = 100;
    private int i = 0;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Addtimer <= 1.5f)//降下を1.5秒後に止める
        {
            transform.position -= new Vector3(0,Time.deltaTime,0);
            Addtimer += Time.deltaTime;
        }
        Shot();
    }


    void Shot()
    {
        if (timer <= 0.0f)
        {
            Instantiate(bulletPrefabEnemy,firePointBossMiddle.position, transform.rotation);
            Instantiate(bulletPrefabEnemy,firePointBossRight.position, transform.rotation);
            Instantiate(bulletPrefabEnemy,firePointBossLeft.position, transform.rotation);
            timer = interval;
        }
        //球を発射する間隔のタイマー
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
    }
    //当たり判定と弾が当たったら爆発するコード
    private void OnTriggerEnter2D(Collider2D collision)
    {          
            if (collision.CompareTag("Player") == true)
            {
                Instantiate(explosion,collision.transform.position,transform.rotation);
                gameController.GameOver();
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("Bullet") == true)
            {
		        // ヒットポイントを減らす
		        hp = hp - 1;
		
		        // 弾の削除
		        Destroy(collision.gameObject);
		
		        // ヒットポイントが0であれば
		        if(hp == 0)
		        {
                 for(i=0;i<5;i++)
                 {
                    gameController.AddScore();
                 }
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    Instantiate(explosion,transform.position,transform.rotation);
                    gameController.GameClear();
                }
            }
    }
    
    
    
}
