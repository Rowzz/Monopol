using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyRailroad : BuyBuyableField
{
    private readonly string BuyInformation = "Möchtest du das den Bahnhof kaufen?";
    private readonly string Information = "Bahnhofinformation";

    internal override void Init(int RentCount)
    {
        base.Init(4);
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
