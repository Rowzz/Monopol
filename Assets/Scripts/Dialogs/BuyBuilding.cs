using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyBuilding : DialogDefinition
{
    private Button YesButton;
    private Button NoButton;
    private Text Name;
    private Image ColorPanel;
    private Text Price;
    private readonly string RentPanel = "Rent Panel";
    private Text[] RentText;
    private Text HousePrice;
    private Text HotelPrice;
    private Text Header;
    private readonly string BuyBuildingInformation = "Möchtest du das Haus kaufen?";
    private readonly string BuildingInformation = "Gebäudeinformation";

    public new void Awake()
    {
        base.Awake();
        string ButtonPanel = "Button Panel";
        string ColorPanelText = "Color Panel";
        string PricePanel = "Price Panel";
        string HousePanel = "Price House Panel";

        Name = FindChild(ColorPanelText, "Name").GetComponent<Text>();
        YesButton = FindChild(ButtonPanel, "Yes Button").GetComponent<Button>();
        NoButton = FindChild(ButtonPanel, "No Button").GetComponent<Button>();
        Price = FindChild(PricePanel, "Price").GetComponent<Text>();
        ColorPanel = FindChild(ColorPanelText).GetComponent<Image>();
        RentText = new Text[7];
        for (int i = 0; i < RentText.Length; i++)
        {
            RentText[i] = FindChild(RentPanel, "Rent", i.ToString()).GetComponent<Text>();
        }
        HousePrice = FindChild(HousePanel, "Price", "House").GetComponent<Text>();
        HotelPrice = FindChild(HousePanel, "Price", "Hotel").GetComponent<Text>();
        Header = FindChild("Header Panel", "Text").GetComponent<Text>();
    }


    public void ShowDialog(Building Building, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
    {
        if (!gameObject.activeSelf || !NoButton.gameObject.activeSelf)
        {
            Header.text = BuyBuildingInformation;
            SetBuildingInformation(Building);
            NoButton.gameObject.SetActive(true);
            SetButtonText(YesButton, "Ja");
            BuyDialog(YesButton, NoButton, ReadOnly, YesClick, NoClick);
        }
    }

    public void ShowDialog(Building Building)
    {
        if (!gameObject.activeSelf || !NoButton.gameObject.activeSelf)
        {
            Header.text = BuyBuildingInformation;
            SetBuildingInformation(Building);
            NoButton.gameObject.SetActive(false);
            SetGameObjectVisibility(true);
            SetButtonText(YesButton, "OK");
            AddCloseEvent(YesButton);
        }
    }

    private void SetBuildingInformation(Building Building)
    {
        ColorPanel.color = Building.ColorOfParent();
        Name.text = Building.Name;
        Price.text = GameController.GetCurrency(Building.Price);
        SetRent(Building.Rent);
        HotelPrice.text = GameController.GetCurrency(Building.PricePerHotel);
        HousePrice.text = GameController.GetCurrency(Building.PricePerHouse);
    }

    private void SetRent(int[] Rent)
    {
        for (int i = 0; i < Rent.Length; i++)
        {
            RentText[i].text = GameController.GetCurrency(Rent[i]);
        }
    }

    private GameObject FindChild(params string[] Names)
    {
        Transform result = gameObject.transform;

        foreach(string Name in Names)
        {
            result = result.Find(Name);
        }
        return result.gameObject;
    }
}
