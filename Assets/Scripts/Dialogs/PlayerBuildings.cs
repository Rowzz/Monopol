using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuildings : DialogDefinition
{
    private Button BtnAccept;
    private Button BtnCancel;
    private Text Balance;
    private readonly static string Hypothek = "Hypothek";
    private readonly string[] HypothekText = new string[2] { Hypothek + " entfernen", Hypothek + " aufnehmen" };

    internal override void Init()
    {
        BtnAccept = InstanceController.GetPlayerBuildingsDialogAcceptButton();
        BtnCancel = InstanceController.GetPlayerBuildingsDialogCancelButton();
        Balance = InstanceController.GetPlayerBuildingsDialogBalance();
    }

    internal void Showdialog(PlayerFigure Player, int? Amount)
    {
        Reset(BtnAccept, BtnCancel);

        List<BuyableField> Buildings = Player.GetOwnedBuildings();
        Balance.text = Player.Balance.ToString();

        bool AllowedToBuy = Amount == null;
        
        if(Buildings.Count > 0)
        {
            List<DialogBuildingValues> DialogList = new List<DialogBuildingValues>();

            foreach (BuyableField Field in Buildings)
            {
                //later in the dialog there are 5 different pictures (for adding houses)
                //Field.HasHouses(); --> then create or show images for Houses/Hotels


                //Instanciate Row
                //DialogBuildingValues Item = new DialogBuildingValues(Field, Row.Find("Result").GetComponent<Text>(), AllowedToBuy);
                //each house of "Row" onclick(new delegate(){Item.SetHouses(1);}), onclick(new delegate(){Item.SetHouses(2);}),...
                //Mortgage button of "Row" onClick(Item.SetMortgage)
                
                //Row.GetComponent<Image>().color = Field.GetParent().GetComponent<Category>().Color;
                //Row.Find("Name").GetComponent<Text>().text = Field.Name;
                //Row.Find("BtnMortgage").GetComponent<Text>().text = Field.Mortgage ? HypothekText[0] : HypothekText[1];

                //DialogList.Add(Item);
            }
        }
        if(!AllowedToBuy)
        {
            BtnCancel.gameObject.SetActive(false);
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
}
