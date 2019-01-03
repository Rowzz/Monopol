﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BuyableField
{
    public int HouseCount;
    public int HotelCount;
    public int PricePerHouse;
    public int PricePerHotel;
    public readonly int MaxHouses = 4;
    public readonly int MaxHotels = 1;

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
        return HotelCount == 1;
    }

    public void AddHouse(int HouseCounter, int HotelCounter)
    {
        HouseCount += (HotelCount += HotelCounter) >= 1 ? 0 : HouseCounter;
    }

    public void removeHouse(int HouseCounter, int HotelCounter)
    {
        HouseCount = (HotelCounter > 0 && (HotelCount -= HotelCounter) == 0) ? 4 - HouseCounter : HouseCount - HouseCounter;
    }

    internal override int GetRent()
    {
        return Rent[GetRentPointer()];
    }

    private int GetRentPointer()
    {
        return OwnsEveryBuildingOfCategory() ? (1 + HouseCount + (HotelCount == 0 ? 0 : HotelCount + MaxHouses)) : 0;
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, CashController CashController)
    {
        if (Owner == null)
        {
            CashController.BuyBuilding(this, ActivePlayer);
        }
        else if (Owner != ActivePlayer)
        {
            CashController.PayRent(this, GetRent(), ActivePlayer);
        }
        else if (OwnsEveryBuildingOfCategory() && !IsFullyUpgraded())
        {
            CashController.BuyHouse(this, ActivePlayer);
        }
    }

    public Color ColorOfParent()
    {
        return GetParent().GetComponent<Category>().Color;
    }

    internal override int GetValue()
    {
        return Mortgage ? 0 : Price / 2 + HotelCount * PricePerHotel / 2 + HouseCount * PricePerHouse / 2;
    }


    internal override void OnMouseDown()
    {
        GameObject.Find("Game Controller").GetComponent<GameController>().DialogController.ShowBuilding(this);
    }


}
