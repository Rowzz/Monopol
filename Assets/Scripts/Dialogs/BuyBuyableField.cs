using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyBuyableField : DialogDefinition
{
    internal Button YesButton;
    internal Button NoButton;
    internal Text Name;
    internal Image ColorPanel;
    internal Text Price;
    internal readonly string RentPanel = "Rent Panel";
    internal Text[] RentText;
    internal Text Header;

    internal override void Init()
    {
        Init(0);
    }

    internal virtual void Init(int RentCount)
    {
        string ButtonPanel = "Button Panel";
        string ColorPanelText = "Color Panel";
        string PricePanel = "Price Panel";

        Name = FindChild(ColorPanelText, "Name").GetComponent<Text>();
        YesButton = FindChild(ButtonPanel, "Yes Button").GetComponent<Button>();
        NoButton = FindChild(ButtonPanel, "No Button").GetComponent<Button>();
        Price = FindChild(PricePanel, "Price").GetComponent<Text>();
        ColorPanel = FindChild(ColorPanelText).GetComponent<Image>();
        
        Header = FindChild("Header Panel", "Text").GetComponent<Text>();

        RentText = new Text[RentCount];
        for (int i = 0; i < RentText.Length; i++)
        {
            RentText[i] = FindChild(RentPanel, "Rent", i.ToString()).GetComponent<Text>();
        }
    }

    internal override void SetYesButtonColor()
    {
        YesButton.GetComponent<Image>().color = Color.green;
        EnableFadeOut();
    }

    internal override void SetNoButtonColor()
    {
        NoButton.GetComponent<Image>().color = Color.red;
        EnableFadeOut();
    }

    internal void ShowDialog(BuyableField Building, bool ReadOnly, Action<string> YesClick, Action<string> NoClick)
    {
        Header.text = BuyBuildingInformation();
        SetBuildingInformation(Building);
        SetButtonText(YesButton, "Ja");

        Reset(YesButton, NoButton);
        ResetGameObject();

        if (!ReadOnly)
        {
            YesButton.onClick.AddListener(delegate () { YesClick(name); });
            NoButton.onClick.AddListener(delegate () { NoClick(name); });
            //AddCloseEvent(YesButton, NoButton);
        }
    }


    internal void ShowDialog(BuyableField Building)
    {
        if (!DialogController.DialogsLocked())
        {
            DialogController.HideDialogs();
            Reset(YesButton, NoButton);
            ResetGameObject();

            Header.text = BuildingInformation();
            SetBuildingInformation(Building);

            NoButton.gameObject.SetActive(false);
            SetButtonText(YesButton, "OK");
            AddCloseEvent(YesButton);
        }
    }

    internal virtual void SetBuildingInformation(BuyableField Building)
    {
        Name.text = Building.Name;
        Price.text = GameController.GetCurrency(Building.Price);
        SetRent(Building.Rent);
    }

    internal abstract void SetRent(int[] Rent);
    internal abstract string BuyBuildingInformation();
    internal abstract string BuildingInformation();

    internal bool IsLocked()
    {
        return NoButton.gameObject.activeSelf && gameObject.activeSelf;
    }
}
