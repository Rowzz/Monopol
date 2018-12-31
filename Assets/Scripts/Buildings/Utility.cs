using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : FieldDefinition
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

    private int GetRent()
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
            CashController.BuyField(this, Price, ActivePlayer);
        }
        else if (ActivePlayer != Owner)
        {
            int Amount = GetRent() * Dicevalue;
            CashController.PayRent(this, Amount, ActivePlayer);
        }
    }

}
