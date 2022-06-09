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
                    GameManager.instance.redKeyText.text = GameManager.instance.redKeys.ToString();
                    return true;
                }
                break;

            case KeyColor.Green:
                if (GameManager.instance.greenKeys > 0)
                {
                    GameManager.instance.greenKeys--;
                    GameManager.instance.greenKeyText.text = GameManager.instance.greenKeys.ToString();
                    return true;
                }
                break;

            case KeyColor.Gold:
                if (GameManager.instance.goldKeys > 0)
                {
                    GameManager.instance.goldKeys--;
                    GameManager.instance.goldKeyText.text = GameManager.instance.goldKeys.ToString();
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
            GameManager.instance.infoText.text = "Press E to Unlock";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            GameManager.instance.infoText.text = "";
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
