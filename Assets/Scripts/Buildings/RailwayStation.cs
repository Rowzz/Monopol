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
        InstanceController.GetDialogController().ShowRailwayStation(this);
    }

    internal override int GetValue()
    {
        return CalcValue(Mortgage);
    }

    private int CalcValue(bool mortgage)
    {
        return mortgage ? 0 : Price / 2;
    }

    internal override int CalcDifference(bool mortgage, int houseCount)
    {
        int MortgageFactor = !mortgage && Mortgage ? -1 : 1;
        return Price / 2 * MortgageFactor;
    }

}
