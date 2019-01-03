using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : BuyableField
{
    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    internal override int GetRent()
    {
        return Rent[GetRentPointer()];
    }

    private int GetRentPointer()
    {
        return OwnsEveryBuildingOfCategory() ? 1 : 0;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, CashController CashController)
    {
        if (Owner == null)
        {
            CashController.BuyUtility(this, ActivePlayer);
        }
        else if (ActivePlayer != Owner)
        {
            int Amount = GetRent() * Dicevalue;
            CashController.PayRent(this, Amount, ActivePlayer);
        }
    }

    internal override void OnMouseDown()
    {
        GameObject.Find("Game Controller").GetComponent<GameController>().DialogController.ShowUtility(this);
    }

    internal override int GetValue()
    {
        return Mortgage ? 0 : Price / 2;
    }

}
