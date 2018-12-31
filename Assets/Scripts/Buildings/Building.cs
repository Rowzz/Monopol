using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : FieldDefinition
{
    public int Price;
    public int[] Rent;
    private int RentPointer=0; //0 rent, 1: rent + colour bonus, 2: 1 house, 3: 2 houses,...,6: hotel
    public int PricePerHouse;
    public int PricePerHotel;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFullyUpgraded()
    {
        return RentPointer == 6;
    }

    public void AddHouse()
    {
        IncreaseRent();
    }

    private void IncreaseRent()
    {
        RentPointer++;
    }

    public void FullSet()
    {
        IncreaseRent();
    }

    public int GetRent()
    {
        return Rent[RentPointer];
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, NotificationController notificationController)
    {
        //owned bei nobody and enough money to buy it
        if (Owner == null && ActivePlayer.Balance >= Price)
        {
            //show Buy-Dialog
            //if (form.ShowDialog() == DialogResult.Yes)
            //{
            //    pf.Balance -= Price;
            //    AddBuilding(pf);
            //    if (OwnsEveryBuildingOfCategory(activeBuilding, pf))
            //    {
            //        AddColourSetBonus();
            //    }
            //}
        }
        //pay rent
        else if (Owner != null && Owner != ActivePlayer)
        {
            int rent = GetRent();
            //show Pay-Dialog
            if (ActivePlayer.Balance >= rent)
            {
                ActivePlayer.Balance -= rent;
                Owner.Balance += rent;
            }
            else //sell buildings
            {
                //switch Player
                int Amount = rent - ActivePlayer.Balance;
                notificationController.SellFields(ActivePlayer, Owner,Amount);
                //and foreach(Building building in soldBuildings){ pf.removeBuilding(activeBuilding)};
            }
        }
        //want to buy houses/hotel?
        else if (OwnsEveryBuildingOfCategory(ActivePlayer) && !IsFullyUpgraded())
        {
            //ShowDialog
        }
        throw new System.NotImplementedException();
    }

    private void AddBuilding(PlayerFigure player)
    {
        Owner = player;
        player.AddBuilding(this);
    }

    private bool OwnsEveryBuildingOfCategory(PlayerFigure player)
    {
        bool valid = true;
        foreach (FieldDefinition building in GetParent().GetComponentsInChildren<FieldDefinition>())
        {
            valid = valid && building.Owner == player;
        }
        return valid;
    }

    private void AddColourSetBonus()
    {
        foreach (Building building in GetParent().GetComponentsInChildren<Building>())
        {
            building.FullSet();
        }
    }
}
