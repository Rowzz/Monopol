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
    internal Text[] RentText;
    internal Text Header;
    internal Transform DefaultParent;

    internal override void Init()
    {
        Init(0);
    }

    internal virtual void Init(int RentCount)
    {
        DefaultParent = transform.Find("Panel");
        Name = InstanceController.GetBuyFieldDialogName(transform);
        YesButton = InstanceController.GetBuyFieldDialogYesButton(transform);
        NoButton = InstanceController.GetBuyFieldDialogNoButton(transform);
        Price = InstanceController.GetBuyFieldDialogPrice(transform);
        ColorPanel = InstanceController.GetBuyFieldDialogColorPanel(transform);

        Header = InstanceController.GetBuyFieldDialogHeader(transform);

        RentText = new Text[RentCount];
        for (int i = 0; i < RentText.Length; i++)
        {
            RentText[i] = InstanceController.GetBuyFieldDialogRent(i, transform);
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
        //InstanceController.GetBuyFieldDialoPanel(transform).SetParent(DefaultParent);
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


    internal void ShowDialog(BuyableField Building, Transform Parent = null)
    {
        Reset(YesButton, NoButton);
        //InstanceController.GetBuyFieldDialoPanel(transform).position = Parent?.position ?? DefaultParent.position;
        //InstanceController.GetBuyFieldDialoPanel(transform).SetParent(Parent ?? DefaultParent);
        ResetGameObject();

        Header.text = BuildingInformation();
        SetBuildingInformation(Building);

        NoButton.gameObject.SetActive(false);
        SetButtonText(YesButton, "OK");
        AddCloseEvent(YesButton);
    }

    internal virtual void SetBuildingInformation(BuyableField Building)
    {
        Name.text = Building.Name;
        Price.text = settingsController.FormatNumber(Building.Price);
        SetRent(Building.Rent);
    }

    internal abstract void SetRent(int[] Rent);
    internal abstract string BuyBuildingInformation();
    internal abstract string BuildingInformation();

    internal override bool IsLocked()
    {
        return NoButton.gameObject.activeSelf && gameObject.activeSelf;
    }
}
