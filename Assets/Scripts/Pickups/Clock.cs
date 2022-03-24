using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Pickup
{
    public int timeToAdd;

    public override void Pick()
    {
        GameManager.instance.AddTime(timeToAdd);
        Destroy(this.gameObject);
    }
}
