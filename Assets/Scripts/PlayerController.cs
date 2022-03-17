using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    CharacterController controller;

    float velocityY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveFB = transform.forward * inputY;//new Vector3(inputX, 0, inputY);
        Vector3 moveLR = transform.right * inputX;
        Vector3 move = moveFB + moveLR;

        if (move.magnitude > 1)
            move = move.normalized;

        controller.Move(move * Time.deltaTime * speed);

        if(GroundCheck())
        {
            //"Stojê na ziemi"
            velocityY = 0;
        }
        else
        {
            //"Wiszê w powietrzu"
            if (velocityY > -30)
            {
                velocityY += Physics.gravity.y * Time.deltaTime; 
            }
        }
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(
            transform.position,
            Vector3.down, 
            out hit,
            1.1f,
            LayerMask.GetMask("Ground")
            ))
        {
            return true;
        }

        return false;
    }
}
