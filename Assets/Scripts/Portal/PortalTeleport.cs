using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    Transform player;
    public Transform receiver;

    bool playerIsOverlapping;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsOverlapping = false;
    }

    private void FixedUpdate()
    {
        if(playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;

            float dot = Vector3.Dot(transform.up, portalToPlayer);

            if(dot < 0)
            {
                player.position = new Vector3(receiver.position.x, player.position.y, receiver.position.z);
                player.rotation = Quaternion.LookRotation(receiver.up, Vector3.up);
            }
        }
    }
}
