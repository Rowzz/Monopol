using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayStation : BuyableField
{

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, CashController CashController)
    {
        if(Owner == null)
        {
            CashController.BuyRailwayStation(this, ActivePlayer);
        }
        else if (ActivePlayer != Owner)
        {
            CashController.PayRent(this, GetRent(), ActivePlayer);
        }

    }

    internal override int GetRent()
    {
        return Rent[GetOwnedChildrenCount() - 1];
    }

    internal override void OnMouseDown()
    {
        GameObject.Find("Game Controller").GetComponent<GameController>().DialogController.ShowRailwayStation(this);
    }

    internal override int GetValue()
    {
        return Mortgage ? 0 : Price / 2;
    }
}
