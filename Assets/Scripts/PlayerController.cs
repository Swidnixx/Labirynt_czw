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
        //Sterowanie
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveFB = transform.forward * inputY;//new Vector3(inputX, 0, inputY);
        Vector3 moveLR = transform.right * inputX;
        Vector3 move = moveFB + moveLR;

        if (move.magnitude > 1)
            move = move.normalized;

        controller.Move(move * Time.deltaTime * speed);

        // Opadanie
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Win"))
        {
            GameManager.instance.GameWin();
        }
        if(hit.gameObject.CompareTag("Pickup"))
        {
            hit.gameObject.GetComponent<Pickup>().Pick();
        }
    }

    private bool GroundCheck()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(
            transform.position,
            Vector3.down, 
            out hit,
            1.02f,
            LayerMask.GetMask("Ground")
            ))
        {
            //Debug.Log(hit.collider.tag);

            switch(hit.collider.tag)
            {
                case "GroundFast":
                    speed = 25;
                    break;

                case "GroundSlow":
                    speed = 5;
                    break;

                default:
                    speed = 12;
                    break;
            }

            return true;
        }

        return false;
    }
}
