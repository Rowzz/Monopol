using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public BuyBuilding BuyBuilding;

    public void BuyField(string Name, int Price, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        BuyBuilding.ShowDialog(Name, Price, Balance, ReadOnly, YesClick);
        //idk if I should differentiate between Building/Utility/Railroad
        //All of them have different information (Set bonus, houses, utility description...)
    }

}
