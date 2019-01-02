using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyRailroad : DialogDefinition
{
    public Button YesButton;
    public Button NoButton;

    public void ShowDialog(RailwayStation RailwayStation, int Balance, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
    {
        BuyDialog(YesButton, NoButton, ReadOnly, YesClick, NoClick);

    }
}
