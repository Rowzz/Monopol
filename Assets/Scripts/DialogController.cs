using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private BuyBuilding BuyBuildingDialog;
    private BuyUtility BuyUtilityDialog;
    private BuyRailroad BuyRailroadDialog;
    private Button PlayerBuildings;
    private Button EndTurn;
    private Text Balance;
    private GameController GameController;

    public void Awake()
    {
        PlayerBuildings = GetDialogTransformThroughName("ActionBar").transform.Find("Panel").Find("Buildings").GetComponent<Button>();
        PlayerBuildings.onClick.RemoveAllListeners();
        PlayerBuildings.onClick.AddListener(delegate () { ShowBuildingsOfPlayer(); });
        Balance = GameObject.Find("ActionBar").transform.Find("Panel").Find("Balance").GetComponent<Text>();
        GameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        EndTurn = GameObject.Find("ActionBar").transform.Find("Panel").Find("EndTurn").GetComponent<Button>();
        EndTurn.onClick.RemoveAllListeners();
        EndTurn.onClick.AddListener(GameController.EndTurn);
        BuyBuildingDialog = GetDialogTransformThroughName("BuyBuilding").GetComponent<BuyBuilding>();
        BuyRailroadDialog = GetDialogTransformThroughName("BuyRailwayStation").GetComponent<BuyRailroad>();
        BuyUtilityDialog = GetDialogTransformThroughName("BuyUtility").GetComponent<BuyUtility>();
        InitDialogs(BuyBuildingDialog, BuyUtilityDialog, BuyRailroadDialog);
    }

    private void InitDialogs(params DialogDefinition[] Dialogs)
    {
        foreach(DialogDefinition Dialog in Dialogs)
        {
            Dialog.Init(0);
        }
    }

    public void SetPlayerBalance(int balance)
    {
        Balance.text = balance.ToString();
    }

    internal void ShowBuildingsOfPlayer(int? Amount = null)
    {
        //Changes:
        //- Hypothek
        //- Häuser
        
        //if Amount == null: only Hypothek

        PlayerFigure player = GameController.GetPlayerOfOwner();
        if (Amount != null)
        {
            //Player has to pay the amount
            //--> Form cannot be closed
        }
        throw new NotImplementedException();
    }

    public void BuyBuilding(Building Building, bool ReadOnly, Action<string> YesClick, Action<string> NoClick)
    {
        BuyBuildingDialog.ShowDialog(Building, ReadOnly, YesClick, NoClick);
    }

    public void ShowBuilding(Building Building)
    {
        BuyBuildingDialog.ShowDialog(Building);
    }

    public void ShowUtility(Utility Building)
    {
        BuyUtilityDialog.ShowDialog(Building);
    }

    public void ShowRailwayStation(RailwayStation Building)
    {
        BuyRailroadDialog.ShowDialog(Building);
    }

    public void BuyRailwayStation(RailwayStation Railroad, bool ReadOnly, Action<string> YesClick, Action<string> NoClick)
    {
        BuyRailroadDialog.ShowDialog(Railroad, ReadOnly, YesClick, NoClick);
    }

    internal void PayRent(int Rent, FieldDefinition Field)
    {
        throw new NotImplementedException();
    }

    public void BuyUtility(Utility Utility, bool ReadOnly, Action<string> YesClick, Action<string> NoClick)
    {
        BuyUtilityDialog.ShowDialog(Utility, ReadOnly, YesClick, NoClick);
    }

    public static Transform GetDialogTransformThroughName(string name)
    {
        return GameObject.Find("Dialogs").transform.Find(name);
    }

    public static DialogDefinition GetDialogThroughName(string name)
    {
        return GameObject.Find("Dialogs").transform.Find(name).GetComponent<DialogDefinition>();
    }

    public static bool DialogsLocked()
    {
        var Dialogs = GameObject.Find("Dialogs").transform;
        for (int i = 0; i < Dialogs.childCount; i++)
        {
            var Transform = Dialogs.GetChild(i);
            DialogDefinition buyableField = Transform.GetComponent<DialogDefinition>();
            if (buyableField is BuyBuyableField && ((BuyBuyableField)buyableField).IsLocked())
            {
                return true;
            }
        }
        return false;
    }

    public static void HideDialogs()
    {
        var Dialogs = GameObject.Find("Dialogs").transform;
        for(int i = 0; i < Dialogs.childCount; i++)
        {
            var Transform = Dialogs.GetChild(i);
            if (Transform.gameObject.activeSelf && Transform.name != "ActionBar")
            {
                Transform.gameObject.SetActive(false);
            }
        }
    }

}
