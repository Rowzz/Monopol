using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyBuilding : DialogDefinition
{
    public Button YesButton;
    public Button NoButton;

    public void ShowDialog(string Name, int Price, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        Reset(YesButton,NoButton);
        SetGameObjectVisibility(true);

        if (!ReadOnly)
        {
            YesButton.onClick.AddListener(YesClick);

            AddCloseEvent(YesButton, NoButton);
        }
    }
}
