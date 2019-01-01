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

    public new void Awake()
    {
        base.Awake();
        string ColorPanelText = "Color Panel";
        Name = FindChild(ColorPanelText, "Name").GetComponent<Text>();
        YesButton = FindChild("Button Panel","Yes Button").GetComponent<Button>();
        NoButton = FindChild("Button Panel", "No Button").GetComponent<Button>();
        ColorPanel = FindChild(ColorPanelText).GetComponent<Image>();
    }


    public void ShowDialog(Building Building, int Balancre, bool ReadOnly, UnityAction YesClick)
    {
        ColorPanel.color = Building.ColorOfParent();
        Name.text = Building.Name;

        BuyDialog(YesButton, NoButton, ReadOnly, YesClick);
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
