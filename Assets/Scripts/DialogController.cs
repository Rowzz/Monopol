using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public BuyBuilding BuyBuildingDialog;
    public BuyUtility BuyUtilityDialog;
    public BuyRailroad BuyRailroadDialog;
    public Button PlayerBuildings;
    private Button EndTurn;
    private Text Balance;
    private GameController GameController;

    public void Awake()
    {
        PlayerBuildings.onClick.RemoveAllListeners();
        PlayerBuildings.onClick.AddListener(ShowBuildingsOfPlayer);
        Balance = GameObject.Find("ActionBar").transform.Find("Panel").Find("Balance").GetComponent<Text>();
        GameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        EndTurn = GameObject.Find("ActionBar").transform.Find("Panel").Find("EndTurn").GetComponent<Button>();
        EndTurn.onClick.RemoveAllListeners();
        EndTurn.onClick.AddListener(GameController.EndTurn);
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

    public void BuyRailwayStation(RailwayStation Railroad, int Balance, bool ReadOnly, UnityAction YesClick, UnityAction NoClick)
    {
        BuyRailroadDialog.ShowDialog(Railroad, Balance, ReadOnly, YesClick, NoClick);
    }

    public void BuyUtility(Utility Utility, int Balance, bool ReadOnly, UnityAction YesClick, UnityAction Noclick)
    {
        BuyUtilityDialog.ShowDialog(Utility, Balance, ReadOnly, YesClick, Noclick);
    }

    public static DialogDefinition GetDialogThroughName(string name)
    {
        return GameObject.Find("Dialogs").transform.Find(name).GetComponent<DialogDefinition>();
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
