using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] PlayerManager player;
   [SerializeField] float sensitivity = 100f;
   [SerializeField] float clampAngle = 85f;

    private float verticalRotation;
    private float horizontalRotation;

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
    }

    private void Update()
    {
        Look();
        Debug.DrawRay(transform.position,transform.forward *20,Color.red);
    }
    private void Look()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += mouseVertical * sensitivity * Time.deltaTime;
        horizontalRotation += mouseHorizontal * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);
        
        transform.localRotation = Quaternion.Euler(verticalRotation,0f,0f);
        player.transform.rotation = Quaternion.Euler(0f,horizontalRotation,0f); 
    }




}
