using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Pickup
{
    public int points = 5;

    public override void Pick()
    {
        GameManager.instance.AddDiamonds(points);
        Destroy(this.gameObject);
    }
}
