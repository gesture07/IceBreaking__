using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //플레이어의 움직임과 관련된 변수들
    public float MoveSpeed = 0.5f;
    public float jump = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    //플레이어의 움직임
    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0,0,MoveSpeed);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0,0,-MoveSpeed);
        }
        /*else if(Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, jump,0);
        }*/
    }
}
