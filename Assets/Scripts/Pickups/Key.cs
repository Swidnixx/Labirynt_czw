using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyColor
{
    Red,
    Green,
    Gold
}
public class Key : Pickup
{
    public KeyColor color;

    public override void Pick()
    {
        GameManager.instance.AddKey(color);
        Destroy(this.gameObject);
    }
}
