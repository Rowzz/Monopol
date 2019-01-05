using System.Collections;
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

    internal int GetTotalHouseCount()
    {
        return HouseCount + HotelCount * 5;
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
        return CalcValue(Mortgage, HouseCount, HotelCount);
    }

    private int CalcValue(bool mortgage, int houseCount, int hotelCount)
    {
        return mortgage ? 0 : Price / 2 + hotelCount * PricePerHotel / 2 + houseCount * PricePerHouse / 2;
    }

    internal override int GetValue(bool mortgage, int houseCount)
    {
        int hotels= 0, houses=0;
        if(houseCount == (MaxHotels + MaxHouses))
        {
            hotels = 1;
        }
        else
        {
            houses = houseCount;
        }
        return CalcValue(mortgage, houses, hotels);
    }


    internal override void OnMouseDown()
    {
        InstanceController.GetDialogController().ShowBuilding(this);
    }

    internal override bool HasHouses()
    {
        return true;
    }


}
