using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{
    private Animator animator;
    public DoorMechanim[] doors;
    public KeyColor properKey;
    bool playerInRange = false;
    bool alreadyUnlocked = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(alreadyUnlocked == false && Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (ChekTheKey())
            {
                alreadyUnlocked = true;
                animator.SetTrigger("open");
                //Unlock(); 
            }
        }
    }

    private bool ChekTheKey()
    {
        switch(properKey)
        {
            case KeyColor.Red:
                if(GameManager.instance.redKeys > 0)
                {
                    GameManager.instance.redKeys--;
                    return true;
                }
                break;

            case KeyColor.Green:
                if (GameManager.instance.greenKeys > 0)
                {
                    GameManager.instance.greenKeys--;
                    return true;
                }
                break;

            case KeyColor.Gold:
                if (GameManager.instance.goldKeys > 0)
                {
                    GameManager.instance.goldKeys--;
                    return true;
                }
                break;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Use the Key");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Lock out of range");
        }
    }

    void Unlock()
    {
        foreach(DoorMechanim d in doors)
        {
            d.Open();
        }
    }
}
