using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuildings : DialogDefinition
{
    private Button BtnAccept;
    private Button BtnCancel;
    private Transform BuildingsPanel;
    private Text Balance;
    private Text Costs;
    private Text AmountText;
    private Text AmountLabel;
    private readonly static string Hypothek = "Hypothek";
    private readonly string[] HypothekText = new string[2] { Hypothek + " entfernen", Hypothek + " aufnehmen" };
    private List<DialogBuildingValues> DialogList;
    private int Result;
    private Color ActiveHouseColor = Color.green;
    private Color InactiveHouseColor = Color.grey;
    private bool Lock;

    internal override void Init()
    {
        BtnAccept = InstanceController.GetPlayerBuildingsDialogAcceptButton();
        BtnCancel = InstanceController.GetPlayerBuildingsDialogCancelButton();
        Balance = InstanceController.GetPlayerBuildingsDialogBalance();
        AmountText = InstanceController.GetPlayerBuildingsDialogAmount();
        AmountLabel = InstanceController.GetPlayerBuildingsDialogAmountLabel();
        BuildingsPanel = InstanceController.GetPlayerBuildingsDialogBuildingsPanel();
        Costs = InstanceController.GetPlayerBuildingsDialogCosts();
        DialogList = new List<DialogBuildingValues>();
    }

    internal void Showdialog(PlayerFigure Player, int? Amount)
    {
        Reset(BtnAccept, BtnCancel);

        settingsController.FormatNumber(Costs, 0);
        List<BuyableField> Buildings = Player.GetOwnedBuildings();
        Balance.text = Player.Balance.ToString();
        BtnAccept.onClick.AddListener(delegate ()
        {
            AllowedToClose(Amount, Player.Balance);
        });
        BtnCancel.onClick.AddListener(Close);

        bool AllowedToBuy = Amount == null;
        Lock = !AllowedToBuy;
        AmountLabel.gameObject.SetActive(Lock);
        AmountText.text = Amount?.ToString() ?? string.Empty;

        if (Buildings.Count > 0)
        {
            ResetChilds(BuildingsPanel);
            DialogList.Clear();

            foreach (BuyableField Field in Buildings)
            {
                Transform Row = Instantiate(Resources.Load<GameObject>("BuildingItem"), BuildingsPanel).transform;
                Transform HousePanel = Row.Find("HousePanel");
                Text ResultText = Row.Find("Result").GetComponent<Text>();
                ResultText.text = string.Empty;
                DialogBuildingValues Item = new DialogBuildingValues(Field, ResultText, AllowedToBuy, UpdateValues);
                Button Mortgage = Row.Find("BtnMortgage").GetComponent<Button>();
                Mortgage.onClick.RemoveAllListeners();
                Mortgage.onClick.AddListener(delegate ()
                {
                    if (Item.SetMortgage())
                    {
                        Mortgage.transform.Find("Text").GetComponent<Text>().text = Item.Mortgage ? HypothekText[0] : HypothekText[1];
                        if (Item.Mortgage) {
                            Item.SetHouses(0);
                            UpdateHousePanel(HousePanel, 0);
                        }
                        HousePanel.gameObject.SetActive(!Item.Mortgage);
                    }
                });
                
                HousePanel.gameObject.SetActive(Field.HasHouses());
                if (Field.HasHouses())
                {
                    for(int i = 0; i < HousePanel.childCount; i++)
                    {
                        int Count = i + 1;
                        Button House = HousePanel.GetChild(i).GetComponent<Button>();
                        House.onClick.RemoveAllListeners();
                        House.onClick.AddListener(delegate ()
                        {
                            if (Item.SetHouses(Count))
                            {
                                UpdateHousePanel(HousePanel, Count);
                            }
                        });
                    }
                    UpdateHousePanel(HousePanel, ((Building)Field).HouseCount);
                }


                Row.GetComponent<Image>().color = Field.GetParent().GetComponent<Category>().Color;
                Row.Find("Name").GetComponent<Text>().text = Field.Name;
                Mortgage.transform.Find("Text").GetComponent<Text>().text = Field.Mortgage ? HypothekText[0] : HypothekText[1];

                DialogList.Add(Item);
            }
        }
        BtnCancel.gameObject.SetActive(AllowedToBuy);
        SetGameObjectVisibility(true);
    }

    private void UpdateHousePanel(Transform Panel,int Count)
    {
        for (int i = 0; i < Panel.childCount; i++)
        {
            Panel.GetChild(i).GetComponent<Image>().color = (i + 1) <= Count ? ActiveHouseColor : InactiveHouseColor;
        }
    }

    private void AllowedToClose(int? Amount, int Balance)
    {
        if((Amount == null || Result >= Amount))
        {
            if((Balance + Result) >= 0)
            {
                //Take over DialogList
                //NetworkController send update
                Close();
            }
            else
            {
                //Error-Message too less money
            }
        }
    }

    private void UpdateValues()
    {
        settingsController.FormatNumber(Costs, Result = DialogList.Sum(Item => Item.Result));
    }

    private void ResetChilds(Transform Panel)
    {
        foreach(Transform Child in Panel)
        {
            Destroy(Child.gameObject);
        }
    }

    internal override void SetNoButtonColor()
    {
        return;
    }

    internal override void SetYesButtonColor()
    {
        return;
    }

    internal override bool IsLocked()
    {
        return Lock && gameObject.activeSelf;
    }
}
