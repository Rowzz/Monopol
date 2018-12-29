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

    public override bool Buyable()
    {
        return true;
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(PlayerFigure[] Players, int ActivePlayer, int Dicevalue)
    {
        PlayerFigure pf = Players[ActivePlayer];


        //owned bei nobody and enough money to buy it
        if (Owner == null && pf.Balance >= Price)
        {
            //show Buy-Dialog
            //if (form.ShowDialog() == DialogResult.Yes)
            //{
            //    pf.Balance -= activeBuilding.Price;
            //    AddBuilding(activeBuilding,pf);
            //    if (OwnsEveryBuildingOfCategory(activeBuilding, pf))
            //    {
            //        AddColourSetBonus(activeBuilding);
            //    }
            //}
        }
        //pay rent
        else if (Owner != null && Owner != pf)
        {
            int rent = GetRent();
            //show Pay-Dialog
            if (pf.Balance >= rent)
            {
                pf.Balance -= rent;
                Owner.Balance += rent;
            }
            else //sell buildings
            {
                //switch Player

                //and foreach(Building building in soldBuildings){ pf.removeBuilding(activeBuilding)};
            }
        }
        //want to buy houses/hotel?
        else if (OwnsEveryBuildingOfCategory(pf) && !IsFullyUpgraded())
        {
            //ShowDialog
        }
        throw new System.NotImplementedException();
    }

    private void AddBuilding(FieldDefinition building, PlayerFigure player)
    {
        building.Owner = player;
        player.AddBuilding(building);
    }

    private bool OwnsEveryBuildingOfCategory(PlayerFigure player)
    {
        bool valid = true;
        foreach (FieldDefinition building in gameObject.transform.parent.gameObject.GetComponentsInChildren<FieldDefinition>())
        {
            valid = valid && building.Owner == player;
        }
        return valid;
    }

    private void AddColourSetBonus(Building activeBuilding)
    {
        foreach (Building building in activeBuilding.gameObject.transform.parent.gameObject.GetComponentsInChildren<Building>())
        {
            building.FullSet();
        }
    }
}
