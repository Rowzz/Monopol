using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BuyableField
{
    public int HouseCount;
    
    public int PricePerHouse;
    public int PricePerHotel;
    public readonly int MaxHouses = 5;

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
        return HouseCount;
    }

    public bool IsFullyUpgraded()
    {
        return HouseCount == MaxHouses;
    }

    public void SetHouseCount(int Count)
    {
        HouseCount = Count;
    }

    internal override int GetRent()
    {
        return Rent[GetRentPointer()];
    }

    private int GetRentPointer()
    {
        return OwnsEveryBuildingOfCategory() ? (1 + HouseCount) : 0;
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
        //else if (OwnsEveryBuildingOfCategory() && !IsFullyUpgraded())
        //{
        //    CashController.BuyHouse(this, ActivePlayer);
        //}
    }

    public Color ColorOfParent()
    {
        return GetParent().GetComponent<Category>().Color;
    }

    internal override int GetValue()
    {
        return CalcValue(Mortgage, HouseCount);
    }

    private int CalcValue(bool mortgage, int houses)
    {
        GetHousesAndHotels(houses, out int houseCount, out int hotelCount);
        return mortgage ? 0 : Price / 2 + hotelCount * PricePerHotel / 2 + houseCount * PricePerHouse / 2;
    }

    private void GetHousesAndHotels(int houses, out int houseCount, out int hotelCount)
    {
        hotelCount = 0;
        houseCount = houses;
        if (houseCount == MaxHouses)
        {
            houseCount--;
            hotelCount++;
        }
    }

    internal override int CalcDifference(bool mortgage, int houses)
    {
        if (mortgage)
        {
            houses = 0;
        }
        int MortgageFactor = !mortgage && Mortgage ? -1 : mortgage && !Mortgage ? 1 : 0;
        GetHousesAndHotels(houses, out int houseCount, out int hotelCount);
        GetHousesAndHotels(HouseCount, out int houseCountOld, out int hotelCountOld);

        int HouseFactor = houseCount < houseCountOld ? 2 : -1;
        int HotelFactor = hotelCount < hotelCountOld ? 2 : -1;
        
        
        return MortgageFactor * Price / 2 + (houseCount * PricePerHouse / HouseFactor) + hotelCount * PricePerHotel / HotelFactor;
    }


    internal override void OnMouseDown()
    {
        InstanceController.GetDialogController().ShowBuilding(this);
    }

    internal override bool HasHouses()
    {
        return OwnsEveryBuildingOfCategory();
    }


}
