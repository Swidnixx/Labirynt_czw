using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanim : MonoBehaviour
{
    public Transform door;
    public Transform closedPosition;
    public Transform openPosition;
    public bool open = false;
    public float speed = 1;

    void Start()
    {
        door.position = closedPosition.position;
    }

    void Update()
    {
        if (open == true && door.position != openPosition.position)
        {
            //Debug.Log("pracujê");
            door.position = Vector3.MoveTowards(door.position, openPosition.position, 
                Time.deltaTime * speed);
        }
        if (open == false && door.position != closedPosition.position)
        {
            //Debug.Log("pracujê");
            door.position = Vector3.MoveTowards(door.position, closedPosition.position,
                Time.deltaTime * speed);
        }
    }

    public void Open()
    {
        open = true;
    }
}
