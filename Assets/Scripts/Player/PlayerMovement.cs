using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControl playerControl;
    private CharacterController controller;

    [Header("Movement")]
    public Vector2 inputValue;
    private Vector3 playerMovement;
    public float speed;
    [SerializeField] private float smoothTime = 0.02f;
    private float currentVelocity = 0.0f;

    private void Awake()
    {
        playerControl =new PlayerControl();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }
    void Update()
    {
        inputValue = playerControl.Gameplay.Move.ReadValue<Vector2>();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        
        if (inputValue != Vector2.zero){
            playerMovement = new Vector3(inputValue.x,0f,inputValue.y);
        }
        else
        {
            return;
        }
        var targetAngle = -Mathf.Atan2(inputValue.y,inputValue.x)*Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0,angle,0);
        
        controller.Move(playerMovement.normalized*speed*Time.deltaTime);
    }
}
