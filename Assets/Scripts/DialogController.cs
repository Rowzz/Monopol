using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private BuyBuilding BuyBuildingDialog;
    private BuyUtility BuyUtilityDialog;
    private BuyRailroad BuyRailroadDialog;
    private PlayerBuildings PlayerBuildingDialog;
    private Button PlayerBuildingsButton;
    private Button EndTurn;
    private Text Balance;
    private GameController GameController;

    public void Awake()
    {
        PlayerBuildingsButton = InstanceController.GetActionBarBuildings();
        PlayerBuildingsButton.onClick.RemoveAllListeners();
        PlayerBuildingsButton.onClick.AddListener(delegate () { ShowBuildingsOfPlayer(); });
        Balance = InstanceController.GetActionBarBalance();
        GameController = InstanceController.GetGameController();
        EndTurn = InstanceController.GetActionBarEndTurn();
        EndTurn.onClick.RemoveAllListeners();
        EndTurn.onClick.AddListener(GameController.EndTurn);
        BuyBuildingDialog = InstanceController.GetBuyBuildingDialog();
        BuyRailroadDialog = InstanceController.GetBuyRailroadDialog();
        BuyUtilityDialog = InstanceController.GetBuyUtilityDialog();
        PlayerBuildingDialog = InstanceController.GetPlayerBuildingsDialog();
        InitDialogs(BuyBuildingDialog, BuyUtilityDialog, BuyRailroadDialog);
    }

    private void InitDialogs(params DialogDefinition[] Dialogs)
    {
        foreach(DialogDefinition Dialog in Dialogs)
        {
            Dialog.Init();
        }
    }

    public void SetPlayerBalance(int balance)
    {
        Balance.text = balance.ToString();
    }

    internal void ShowBuildingsOfPlayer(int? Amount = null)
    {
        PlayerBuildingDialog.Showdialog(GameController.GetPlayerOfOwner(), Amount);
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
