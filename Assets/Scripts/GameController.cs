using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    public static readonly string Currency = "€";
    public Dice[] Dices;
    private List<PlayerFigure> Players;
    public CashController CashController;
    public PlayerFigure ActivePlayer;
    public DialogController DialogController;

    // Start is called before the first frame update
    void Start()
    {
        Players = new List<PlayerFigure>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StayOnField(FieldDefinition field, int DiceValue)
    {
        field.Stay(Players, ActivePlayer, DiceValue, CashController);
    }

    public void SetDiceValue(int value)
    {
        ActivePlayer.MovePlayer(value);
    }

    public void AddPlayer(PlayerFigure player)
    {
        Players.Add(player);
        SetActivePlayer(0);
    }

    public void NextPlayer()
    {
        int index = Players.IndexOf(ActivePlayer)+1;
        index = index >= Players.Count ? 0 : index;
        SetActivePlayer(index);
    }

    private void SetActivePlayer(int index)
    {
        ActivePlayer = Players[index];
        if (IsOwnerTurn())
        {
            foreach(Dice dice in Dices)
            {
                dice.GetComponent<PhotonView>().RequestOwnership();
                dice.SetDiceLock(false);
            }
        }
    }

    public bool IsOwnerTurn()
    {
        //Return if Owner 
        return true;
    }

    public static string GetCurrency(int Price)
    {
        return Price + Currency;
    }
}
