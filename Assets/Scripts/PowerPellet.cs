using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;
    protected override void Eat() // virtual allow to override
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }


}
