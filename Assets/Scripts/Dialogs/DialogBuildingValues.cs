using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBuildingValues
{
    private BuyableField Field;
    private int Houses;
    private int Result;
    private Text ResultText;
    private bool Mortgage;
    private bool AllowedToBuy; 

    internal DialogBuildingValues(BuyableField field, Text resultText, bool allowedToBuy)
    {
        Field = field;
        Mortgage = Field.Mortgage;
        ResultText = resultText;
        AllowedToBuy = allowedToBuy;

        if (Field.HasHouses()) {
            Houses = ((Building)Field).GetTotalHouseCount();
        }
    }

    

    internal void SetHouses(int Count)
    {
        if (AllowedToBuy && Count < ((Building)Field).GetTotalHouseCount())
        {
            Houses = Count;
            UpdateResult();
        }
    }

    internal void SetMortgage()
    {
        Mortgage = !Mortgage;
        UpdateResult();
    }

    private void UpdateResult()
    {
        ResultText.text = CalcValue().ToString();
    }

    private int CalcValue()
    {
        return (Result = Field.GetValue(Mortgage,Houses));
    } 
}
