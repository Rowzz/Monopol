﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : FieldDefinition
{
    public int Amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Buyable()
    {
        return false;
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        playerFigure.Balance += Amount;
    }

    public override void Stay(PlayerFigure[] Players, int ActivePlayer, int Dicevalue)
    {
        Players[ActivePlayer].Balance += Amount;
    }
}
