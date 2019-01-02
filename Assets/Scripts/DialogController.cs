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
        PlayerBuildings = GetDialogThroughName("ActionBar").transform.Find("Panel").Find("Buildings").GetComponent<Button>();
        PlayerBuildings.onClick.RemoveAllListeners();
        PlayerBuildings.onClick.AddListener(ShowBuildingsOfPlayer);
        Balance = GameObject.Find("ActionBar").transform.Find("Panel").Find("Balance").GetComponent<Text>();
        GameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        EndTurn = GameObject.Find("ActionBar").transform.Find("Panel").Find("EndTurn").GetComponent<Button>();
        EndTurn.onClick.RemoveAllListeners();
        EndTurn.onClick.AddListener(GameController.EndTurn);
        BuyBuildingDialog = GetDialogThroughName("BuyBuilding").GetComponent<BuyBuilding>();
        BuyRailroadDialog = GetDialogThroughName("BuyRailwayStation").GetComponent<BuyRailroad>();
        BuyUtilityDialog = GetDialogThroughName("BuyUtility").GetComponent<BuyUtility>();
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

    private void ShowBuildingsOfPlayer()
    {
        PlayerFigure player = GameController.GetPlayerOfOwner();
        throw new System.NotImplementedException();
    }

    public void BuyBuilding(Building Building, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
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

    public void BuyRailwayStation(RailwayStation Railroad, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
    {
        BuyRailroadDialog.ShowDialog(Railroad, ReadOnly, YesClick, NoClick);
    }

    public void BuyUtility(Utility Utility, bool ReadOnly, UnityAction YesClick, UnityAction Noclick)
    {
        BuyUtilityDialog.ShowDialog(Utility, ReadOnly, YesClick, Noclick);
    }

    public static Transform GetDialogThroughName(string name)
    {
        return GameObject.Find("Dialogs").transform.Find(name);
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
