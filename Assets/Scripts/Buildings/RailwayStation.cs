using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayStation : FieldDefinition
{
    public int Price;
    public int[] Rent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, CashController CashController)
    {
        if(Owner == null)
        {
            CashController.BuyField(this, Price, ActivePlayer);
        }
        else if (ActivePlayer != Owner)
        {
            CashController.PayRent(this, GetRent(), ActivePlayer);
        }

    }

    private int GetRent()
    {
        return Rent[GetOwnedChildrenCount() - 1];
    }
}
