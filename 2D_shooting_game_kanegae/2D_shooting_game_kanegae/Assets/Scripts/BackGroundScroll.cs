using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    //背景のスクロールスピード
    public float BGspeed = 1;

    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * BGspeed);

        if (transform.position.y <= -4.8f)
        {
            transform.position = new Vector2(0, 4.8f);
        }
    }
}