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
    private readonly static string cashControllerString = "Cash Controller";
    private readonly static string settingsControllerString = "SettingsController";
    private readonly static string DialogsString = "Dialogs";
    private readonly static string BuyBuildingsString = "BuyBuilding";
    private readonly static string BuyRailwayStationString = "BuyRailwayStation";
    private readonly static string BuyUtilityString = "BuyUtility";
    private readonly static string BuildingsOfPlayerString = "BuildingsOfPlayer";
    private readonly static string ActionBarString = "ActionBar";
    private readonly static string ActionBarPanelString = "Panel";
    private readonly static string BuildingsOfPlayerPanel1String = "Panel";
    private readonly static string BuildingsOfPlayerPanel2String = "MortgageAndHouses";
    private readonly static string BuyFieldDialogPanelString = "Panel";
    private readonly static string BuyFieldDialogButtonPanel = "Button Panel";
    private readonly static string BuyFieldDialogColorPanelString = "Color Panel";
    private readonly static string BuyFieldDialogHousePanelString = "Price House Panel";
    private readonly static string BuyFieldDialogHousePanel2 = "Price";
    private readonly static string[] BuyFieldDialogName = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogColorPanelString, "Name"};
    private readonly static string[] BuyFieldDialogPanel = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString};
    private readonly static string[] BuyFieldDialogHotel = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogHousePanelString, BuyFieldDialogHousePanel2, "Hotel" };
    private readonly static string[] BuyFieldDialogHouse = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogHousePanelString, BuyFieldDialogHousePanel2, "House" };
    private readonly static string[] BuyFieldDialogHeader = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, "Header Panel", "Text" };
    private readonly static string[] BuyFieldDialogRent = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, "Rent Panel", "Rent"};
    private readonly static string[] BuyFieldDialogPrice = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, "Price Panel", "Price" };
    private readonly static string[] BuyFieldDialogColorPanel = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogColorPanelString };
    private readonly static string[] BuyFieldDialogYesButton = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogButtonPanel, "Yes Button" };
    private readonly static string[] BuyFieldDialogNoButton = new string[] { BuyFieldDialogPanelString, BuyFieldDialogPanelString, BuyFieldDialogButtonPanel, "No Button" };
    private readonly static string[] ActionBarEndTurnString = new string[] { ActionBarPanelString, "EndTurn" };
    private readonly static string[] ActionBarBuildingsString = new string[] { ActionBarPanelString, "Buildings" };
    private readonly static string[] ActionBarBalanceString = new string[] { ActionBarPanelString, "Balance" };
    private readonly static string[] BuildingsOfPlayerAcceptString = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "BtnAccept" };
    private readonly static string[] BuildingsOfPlayerCancelString = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "BtnCancel" };
    private readonly static string[] BuildingsOfPlayerBalanceString = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "Balance" };
    private readonly static string[] BuildingsOfPlayerBuildingsPanel = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "Buildings", "Grid" };
    private readonly static string[] BuildingsOfPlayerCosts = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "Costs" };
    private readonly static string[] BuildingsOfPlayerAmount = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "Amount" };
    private readonly static string[] BuildingsOfPlayerAmountLabel = new string[] { BuildingsOfPlayerPanel1String, BuildingsOfPlayerPanel2String, "AmountLabel" };
    private readonly static string[] BuildingsOfPlayeFieldInformation = new string[] { BuildingsOfPlayerPanel1String, "FieldInformation"};
    private static GameController gameController;
    private static NetworkingController networkController;
    private static DialogController dialogController;
    private static CashController cashController;
    private static InstanceController instanceController;
    private static SettingsController settingsController;

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
    internal static GameController GetGameController()
    {
        return gameController ?? (gameController = GameObject.Find(gameControllerString).GetComponent<GameController>());
    }

    internal static DialogController GetDialogController()
    {
        return dialogController ?? (dialogController = GameObject.Find(dialogControllerString).GetComponent<DialogController>());
    }

    internal static NetworkingController GetNetworkingController()
    {
        return networkController ?? (networkController = GameObject.Find(networkControllerString).GetComponent<NetworkingController>());
    }

    internal static CashController GetCashController()
    {
        return cashController ?? (cashController = GameObject.Find(cashControllerString).GetComponent<CashController>());
    }

    internal static SettingsController GetSettingsController()
    {
        return settingsController ?? (settingsController = GameObject.Find(settingsControllerString).GetComponent<SettingsController>());
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

    internal static Text GetBuyFieldDialogName(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogName).GetComponent<Text>();
    }

    internal static Transform GetBuyFieldDialoPanel(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogPanel);
    }

    internal static Text GetBuyFieldDialogHeader(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogHeader).GetComponent<Text>();
    }

    internal static Text GetBuyFieldDialogHotelPrice(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogHotel).GetComponent<Text>();
    }

    internal static Text GetBuyFieldDialogHousePrice(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogHouse).GetComponent<Text>();
    }

    internal static Text GetBuyFieldDialogRent(int Counter, Transform transform)
    {
        List<string> temp = new List<string>(BuyFieldDialogRent);
        temp.Add(Counter.ToString());
        return GetTransform(transform, temp.ToArray()).GetComponent<Text>();
    }

    internal static Button GetBuyFieldDialogYesButton(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogYesButton).GetComponent<Button>();
    }

    internal static Button GetBuyFieldDialogNoButton(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogNoButton).GetComponent<Button>();
    }

    internal static Text GetBuyFieldDialogPrice(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogPrice).GetComponent<Text>();
    }

    internal static Image GetBuyFieldDialogColorPanel(Transform transform)
    {
        return GetTransform(transform, BuyFieldDialogColorPanel).GetComponent<Image>();
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

    internal static Transform GetPlayerBuildingsDialogBuildingsPanel()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerBuildingsPanel);
    }
    internal static Text GetPlayerBuildingsDialogCosts()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerCosts).GetComponent<Text>();
    }

    internal static Text GetPlayerBuildingsDialogAmount()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerAmount).GetComponent<Text>();
    }

    internal static Text GetPlayerBuildingsDialogAmountLabel()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayerAmountLabel).GetComponent<Text>();
    }

    internal static Transform GetPlayerBuildingsDialogFieldInformation()
    {
        return GetTransform(GetDialogs().Find(BuildingsOfPlayerString), BuildingsOfPlayeFieldInformation);
    }

    #endregion

}
