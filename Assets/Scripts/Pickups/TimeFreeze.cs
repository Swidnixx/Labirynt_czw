using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : Pickup
{
    public int freezeTime = 10;

    public override void Pick()
    {

        GameManager.instance.FreezeTime(freezeTime);
        Destroy(this.gameObject);
    }
}
