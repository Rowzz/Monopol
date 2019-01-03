using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyUtility : BuyBuyableField
{
    private Dictionary<int, string> dictCount;
    private readonly string BuyInformation = "Möchtest du dieses Spezialfeld kaufen?";
    private readonly string Information = "Spezialfeldinformation";

    public override void Init(int RentCount)
    {
        base.Init(2);
        dictCount = new Dictionary<int, string>
        {
            { 0, "eins" },
            { 1, "zwei" }
        };
    }

    internal override void SetRent(int[] Rent)
    {
        for (int i = 0; i < Rent.Length; i++)
        {
            dictCount.TryGetValue(i, out string value);
            RentText[i].text = $"Falls Sie {value} Spezial-Gebäude besitzen, ergibt sich die Miete aus {Rent[i]} mal der Würfelanzahl";
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
