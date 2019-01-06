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
        InstanceController.GetDialogController().ShowUtility(this);
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
