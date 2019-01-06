using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyBuilding : BuyBuyableField
{
    private Text HousePrice;
    private Text HotelPrice;
    private readonly string BuyInformation = "Möchtest du das Haus kaufen?";
    private readonly string Information = "Gebäudeinformation";

    internal override void Init(int RentCount)
    {
        base.Init(7);
        string ColorPanelText = "Color Panel";
        string HousePanel = "Price House Panel";
        
        ColorPanel = FindChild(ColorPanelText).GetComponent<Image>();
        
        HousePrice = FindChild(HousePanel, "Price", "House").GetComponent<Text>();
        HotelPrice = FindChild(HousePanel, "Price", "Hotel").GetComponent<Text>();
    }

    internal override void SetBuildingInformation(BuyableField Field)
    {
        base.SetBuildingInformation(Field);
        Building Building = (Building)Field;
        ColorPanel.color = Building.ColorOfParent();
        HotelPrice.text = settingsController.FormatNumber(Building.PricePerHotel);
        HousePrice.text = settingsController.FormatNumber(Building.PricePerHouse);
    }

    internal override void SetRent(int[] Rent)
    {
        for (int i = 0; i < Rent.Length; i++)
        {
            RentText[i].text = settingsController.FormatNumber(Rent[i]);
        }
    }

    internal override string BuildingInformation()
    {
        return Information;
    }

    internal override string BuyBuildingInformation()
    {
        return BuyInformation;
    }
}
