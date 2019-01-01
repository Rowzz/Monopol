using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public BuyBuilding BuyBuildingDialog;
    public BuyUtility BuyUtilityDialog;
    public BuyRailroad BuyRailroadDialog;

    public void BuyBuilding(Building Building, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        BuyBuildingDialog.ShowDialog(Building, Balance, ReadOnly, YesClick);
    }

    public void BuyRailwayStation(RailwayStation Railroad, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        BuyRailroadDialog.ShowDialog(Railroad, Balance, ReadOnly, YesClick);
    }

    public void BuyUtility(Utility Utility, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        BuyUtilityDialog.ShowDialog(Utility, Balance, ReadOnly, YesClick);
    }

}
