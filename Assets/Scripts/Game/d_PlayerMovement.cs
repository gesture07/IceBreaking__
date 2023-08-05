using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    
    }

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
    }
}
