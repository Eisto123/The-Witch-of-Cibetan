using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControl playerControl;
    private CharacterController controller;

    [Header("Movement")]
    public Vector2 inputValue;
    private Vector3 playerMovement;
    public float speed;
    private float gravityValue = -9.81f;
    [SerializeField] private float smoothTime = 0.02f;
    private float currentVelocity = 0.0f;
    public float MoveRadius = 10f;
    private Vector3 initialPos;
    public float rayDistance = 1.2f;
    public float detectRadius = 1;
    private bool onPlain;
    public UnityEvent onDamage;
    public LayerMask layerMask;

    private void Awake()
    {
        playerControl =new PlayerControl();
        controller = GetComponent<CharacterController>();
        initialPos = transform.position;
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
        if (Physics.SphereCast(transform.position, detectRadius, Vector3.down, out RaycastHit hit, rayDistance,layerMask))//layermask is Plain
        {
            onPlain = true;
            inputValue = playerControl.Gameplay.Move.ReadValue<Vector2>();
        }
        else
            {
                controller.velocity.Set(0,0,0);
                onPlain = false;
                transform.position -= Vector3.down*gravityValue*Time.deltaTime;
            }  
    }

    private void FixedUpdate()
    {
        if(onPlain){
            Move();
        }
        
    }

    private void Move()
    {
        
        if (inputValue != Vector2.zero){
            playerMovement = new Vector3(inputValue.x,playerMovement.y,inputValue.y);
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
    public void TakeDamage(){
        //shake
        //受击animation
        onDamage.Invoke();

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position+Vector3.down*rayDistance, detectRadius);
    }
}
