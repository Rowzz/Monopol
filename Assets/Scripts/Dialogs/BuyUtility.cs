using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyUtility : DialogDefinition
{
    private Button YesButton;
    private Button NoButton;
    private Text Name;
    private Image ColorPanel;
    private Text Price;
    private readonly string RentPanel = "Rent Panel";
    private Text[] RentText;
    private Text Header;
    private readonly string BuyBuildingInformation = "Möchtest du das den Bahnhof kaufen?";
    private readonly string BuildingInformation = "Bahnhofinformation";
    private Dictionary<int, string> dictCount;


    public override void Init()
    {
        string ButtonPanel = "Button Panel";
        string ColorPanelText = "Color Panel";
        string PricePanel = "Price Panel";

        Name = FindChild(ColorPanelText, "Name").GetComponent<Text>();
        YesButton = FindChild(ButtonPanel, "Yes Button").GetComponent<Button>();
        NoButton = FindChild(ButtonPanel, "No Button").GetComponent<Button>();
        Price = FindChild(PricePanel, "Price").GetComponent<Text>();
        ColorPanel = FindChild(ColorPanelText).GetComponent<Image>();
        RentText = new Text[2];
        for (int i = 0; i < RentText.Length; i++)
        {
            RentText[i] = FindChild(RentPanel, "Rent", i.ToString()).GetComponent<Text>();
        }
        Header = FindChild("Header Panel", "Text").GetComponent<Text>();
        dictCount = new Dictionary<int, string>();
        dictCount.Add(0, "eins");
        dictCount.Add(1, "zwei");
    }


    public void ShowDialog(Utility Building, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
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

    public void ShowDialog(Utility Building)
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

    private void SetBuildingInformation(Utility Building)
    {
        Name.text = Building.Name;
        Price.text = GameController.GetCurrency(Building.Price);
        SetRent(Building.Rent);
    }

    private void SetRent(int[] Rent)
    {
        for (int i = 0; i < Rent.Length; i++)
        {
            dictCount.TryGetValue(i, out string value);
            RentText[i].text = $"Falls Sie {value} Spezial-Gebäude besitzen, ergibt sich die Miete aus {Rent[i]} mal der Würfelanzahl";
        }
    }
}
