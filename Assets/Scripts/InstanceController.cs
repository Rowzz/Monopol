using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceController : MonoBehaviour
{
    private readonly static string gameControllerString = "Game Controller";
    private readonly static string dialogControllerString = "DialogController";
    private readonly static string networkControllerString = "Networking Controller";
    private readonly static string DialogsString = "Dialogs";
    private readonly static string BuyBuildingsString = "BuyBuilding";
    private readonly static string BuyRailwayStationString = "BuyRailwayStation";
    private readonly static string BuyUtilityString = "BuyUtility";
    private readonly static string BuildingsOfPlayerString = "BuildingsOfPlayer";
    private readonly static string ActionBarString = "ActionBar";
    private readonly static string ActionBarPanelString = "Panel";
    private readonly static string BuildingsOfPlayerPanel1String = "Panel";
    private readonly static string BuildingsOfPlayerPanel2String = "MortgageAndHouses";
    private readonly static string[] ActionBarEndTurnString = new string[2] { ActionBarPanelString, "EndTurn" };
    private readonly static string[] ActionBarBuildingsString = new string[2] { ActionBarPanelString, "Buildings" };
    private readonly static string[] ActionBarBalanceString = new string[2] { ActionBarPanelString, "Balance" };
    private readonly static string[] BuildingsOfPlayerAcceptString = new string[3] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "BtnAccept" };
    private readonly static string[] BuildingsOfPlayerCancelString = new string[3] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "BtnCancel" };
    private readonly static string[] BuildingsOfPlayerBalanceString = new string[3] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "Balance" };
    private static GameController gameController;
    private static NetworkingController networkController;
    private static DialogController dialogController;
    private static CashController cashController;

    private void Awake()
    {
        gameController = GameObject.Find(gameControllerString).GetComponent<GameController>();
        dialogController = GameObject.Find(dialogControllerString).GetComponent<DialogController>();
        networkController = GameObject.Find(networkControllerString).GetComponent<NetworkingController>();
        cashController = GameObject.Find(networkControllerString).GetComponent<CashController>();
    }

    private static Transform GetTransform(Transform transform, params string[] Childs)
    {
        Transform result = transform;
        foreach(string child in Childs)
        {
            result = result.Find(child);
        }
        return result;
    }

    #region Controller
    //internal static GameController GetGameController()
    //{
    //    return gameController ?? (gameController = GameObject.Find(gameControllerString).GetComponent<GameController>());
    //}

    internal static GameController GetGameController()
    {
        return gameController;
    }

    internal static DialogController GetDialogController()
    {
        return dialogController;
    }

    internal static NetworkingController GetNetworkingController()
    {
        return networkController;
    }

    internal static CashController GetCashController()
    {
        return cashController;
    }

    #endregion


    #region Dialogs

    internal static Transform GetDialogs()
    {
        return GameObject.Find(DialogsString).transform;
    }

    #region ActionBar
    internal static Transform GetActionBar()
    {
        return GetDialogs().Find(ActionBarString);
    }

    internal static Button GetActionBarEndTurn()
    {
        return GetTransform(GetActionBar(),ActionBarEndTurnString).GetComponent<Button>();
    }

    internal static Text GetActionBarBalance()
    {
        return GetTransform(GetActionBar(), ActionBarBalanceString).GetComponent<Text>();
    }

    internal static Button GetActionBarBuildings()
    {
        return GetTransform(GetActionBar(), ActionBarBuildingsString).GetComponent<Button>();
    }
    #endregion

    internal static BuyBuilding GetBuyBuildingDialog()
    {
        return GetDialogs().Find(BuyBuildingsString).GetComponent<BuyBuilding>();
    }

    internal static BuyRailroad GetBuyRailroadDialog()
    {
        return GetDialogs().Find(BuyRailwayStationString).GetComponent<BuyRailroad>();
    }

    internal static BuyUtility GetBuyUtilityDialog()
    {
        return GetDialogs().Find(BuyUtilityString).GetComponent<BuyUtility>();
    }

    internal static PlayerBuildings GetPlayerBuildingsDialog()
    {
        return GetDialogs().Find(BuildingsOfPlayerString).GetComponent<PlayerBuildings>();
    }

    internal static Button GetPlayerBuildingsDialogAcceptButton()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerAcceptString).GetComponent<Button>();
    }

    internal static Button GetPlayerBuildingsDialogCancelButton()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerCancelString).GetComponent<Button>();
    }

    internal static Text GetPlayerBuildingsDialogBalance()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerBalanceString).GetComponent<Text>();
    }

    #endregion

}
