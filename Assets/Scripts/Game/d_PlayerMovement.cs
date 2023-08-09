using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d_PlayerMovement : MonoBehaviour
{
    private Transform tr;

    public float MoveSpeed = 0.3f;

    public bool ice = false;

    public float turnSpeed = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = Input.GetAxis("Mouse X");

        if(Input.GetKey(KeyCode.R)){
            ice = true;
        }
        if(!ice){
            PlayerInput();
        }

        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime* r);
    
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
