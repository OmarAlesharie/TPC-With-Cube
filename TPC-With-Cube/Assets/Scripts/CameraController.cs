﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float Away;
    public float Up;
    public float cameraSmothFollow;

    [SerializeField]
    private Vector2 verticalViewLimits = new Vector2(-30, 40);

    private Transform Player;
    private Transform rootOfPlayer;
    private Vector3 Target;
    private Transform TargetTransform;
    private float mouseX, mouseY;
    private Quaternion PrevRotation;


    // Start is called before the first frame update
    void Start()
    {
         
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rootOfPlayer = new GameObject("rootOfPlayer").transform;

        rootOfPlayer.position = Player.position;
        TargetTransform = new GameObject("TargetTransform").transform;

        SetTargetPosition();
        rootOfPlayer.forward = Player.forward;
        transform.LookAt(Player.position);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetTargetPosition()
    {
        TargetTransform.position = rootOfPlayer.position + Vector3.up * Up - rootOfPlayer.forward * Away;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.Q))
        {
            rootOfPlayer.forward = Player.forward;
        }


        Quaternion newRotation = Quaternion.Euler(-mouseY, mouseX, 0f);
        rootOfPlayer.rotation *= newRotation;

        rootOfPlayer.position = Player.position;

        SetTargetPosition();
        WallCollision();

        transform.position = Vector3.Lerp(transform.position, TargetTransform.position, cameraSmothFollow*Time.deltaTime);
        transform.LookAt(Player.position);
    }

    private void WallCollision()
    {
        if (Physics.Linecast(rootOfPlayer.position, TargetTransform.position, out RaycastHit Hit))
        {
            TargetTransform.position = new Vector3(Hit.point.x, TargetTransform.position.y, Hit.point.z);
        }
    }
}
