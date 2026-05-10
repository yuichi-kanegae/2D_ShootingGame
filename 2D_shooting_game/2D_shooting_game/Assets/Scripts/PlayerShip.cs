using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShip : MonoBehaviour
{
    GameController gameController;
    public GameObject explosion; 
    public Transform firePointRight; // 右弾を発射する位置
    public Transform firePointLeft; // 左弾を発射する位置
    public GameObject bulletPrefab;
    
    private float interval = 0.2f; // 何秒間隔で撃つか
    private float timer = 0.0f;
    private float PosY = 0; //Y座標に応じて発射速度を変更

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        Transform myTransform = this.gameObject.GetComponent<Transform> ();
        Vector3 posi = myTransform.position;
        PosY = posi.y;
        PosY = 0.03f*(PosY+2f);
        Move();
        Shot();
    }

    void Shot()
    {
        if (timer <= 0.0f)
        {
            // ショット音を鳴らす
            GetComponent<AudioSource>().Play();
            Instantiate(bulletPrefab, firePointRight.position, transform.rotation);
            Instantiate(bulletPrefab, firePointLeft.position, transform.rotation);
            timer = interval-PosY;
        }
        //球を発射する間隔
        if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
    }

    void Move()
    {
        //Playerの移動操作
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 nextPosition = transform.position + new Vector3(x, y, 0) * Time.deltaTime * 4f;
        // ・Playerの移動範囲制御
        // x：(-2.9,2.9) y：(-2,2)
        nextPosition = new Vector3
        (
            Mathf.Clamp(nextPosition.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPosition.y, -2f, 2f),
            nextPosition.z
        );
        transform.position = nextPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {          
        if (collision.CompareTag("Bullet") == true)
        {
            Instantiate(explosion,collision.transform.position,transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            gameController.GameOver();
        }
    }
}
