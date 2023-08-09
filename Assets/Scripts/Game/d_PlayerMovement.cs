using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d_PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 0.3f;

    public bool ice = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R)){
            ice = true;
        }
        if(!ice){
            PlayerInput();
        }
    
    }

    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0,0,MoveSpeed);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0,0,-MoveSpeed);
        }
    }
}
