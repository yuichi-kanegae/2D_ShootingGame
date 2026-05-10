using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Ymoving;

    public float MinXmoving;
    public float MaxXmoving;

    public float Xmoving;
    bool isCalledOnce = false;

    void Start()
    {
       Invoke(nameof(Destroy),4.0f);
    }

    // Update is called once per frame
    void Update()
    {   
        //X方向の動きを決め、後から変更不可にする
        if(!isCalledOnce)
        {
            isCalledOnce = true;
            Xmoving = Random.Range(MinXmoving,MaxXmoving);
        }
        transform.position += new Vector3(Xmoving,Ymoving,0)*Time.deltaTime;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

   

}
