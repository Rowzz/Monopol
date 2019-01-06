using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBuildingValues
{
    private BuyableField Field;
    private int Houses;
    internal int Result;
    private Text ResultText;
    internal bool Mortgage;
    private bool AllowedToBuy;
    private Action UpdateAction;
    private SettingsController settingsController { get { return InstanceController.GetSettingsController(); } }

    internal DialogBuildingValues(BuyableField field, Text resultText, bool allowedToBuy, Action updateAction)
    {
        Field = field;
        Mortgage = Field.Mortgage;
        ResultText = resultText;
        AllowedToBuy = allowedToBuy;
        UpdateAction = updateAction;

        if (Field.HasHouses()) {
            Houses = ((Building)Field).GetTotalHouseCount();
        }
    }

    

    internal bool SetHouses(int Count)
    {
        bool status;
        int HouseCount = ((Building)Field).GetTotalHouseCount();
        if (status = (AllowedToBuy || Count < HouseCount))
        {
            Houses = Count;
            UpdateResult();
        }
        return status;
    }

    internal bool SetMortgage()
    {
        bool status;
        if (status = (AllowedToBuy || !Field.Mortgage))
        {
            Mortgage = !Mortgage;
            UpdateResult();
        }
        return status;
    }

    private void UpdateResult()
    {
        settingsController.FormatNumber(ResultText, CalcValue());
        UpdateAction();
    }

    private int CalcValue()
    {
        return (Result = Field.CalcDifference(Mortgage,Houses));
    } 
}
