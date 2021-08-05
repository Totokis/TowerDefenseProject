using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("PlayerShoot");
            ClientSend.PlayerShoot(cameraTransform.forward);
        }
    }
    
    void FixedUpdate()
    {
        SendInputToServer();
    }
    void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
        };

        //Debug.Log($"Up {inputs[0]} Down {inputs[1]} Left {inputs[2]} Right {inputs[3]}");
        ClientSend.PlayerMovement(inputs);
    }

}
