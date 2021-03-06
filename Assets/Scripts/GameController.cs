﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;

public class GameController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static readonly string Currency = "€";
    public Dice[] Dices;
    public List<PlayerFigure> Players;
    public CashController CashController;
    public PlayerFigure ActivePlayer;
    private SettingsController settingsController { get { return InstanceController.GetSettingsController(); } }

    // Start is called before the first frame update
    void Start()
    {
        Players = new List<PlayerFigure>();
        DisableDice();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DisableDice()
    {
        foreach( Dice dice in Dices)
        {
            dice.SetDiceLock(true);
        }
    }

    private bool RolledDice()
    {
        var valid = true;
        foreach (Dice dice in Dices)
        {
            valid = valid && dice.GetDiceLock();
        }
        return valid;
    }

    public void OnEvent(EventData photonEvent)
    {
        switch(photonEvent.Code)
        {
            //0 - 9 reserved for GameController
            //10 - x reserved for Cashcontroller
            //Trigger Stay On Field
            case 0:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    DialogController.HideDialogs();
                    StayOnField(GetFieldThroughName(data[0].ToString(), data[1].ToString()), (int)data[2]);
                }
                break;

            //Player joined, Add him
            case 1:
                {
                    AddPlayer(null, (int)photonEvent.CustomData);
                }
                break;
            
            //EndTurn
            case 2:
                {
                    NextPlayer();
                }
                break;

                //Request All Players
            case 3:
                {
                    NetworkingController.SendData(GetPlayerOfOwner().ID, 4, false);
                }
                break;

            case 4: //Received All Players
                {
                    CheckPlayer((int)photonEvent.CustomData);
                }
                break;
        }
    }

    public FieldDefinition GetFieldThroughName(string category, string name)
    {
        return GameObject.Find("Fields").transform.Find(category).Find(name).GetComponent<FieldDefinition>();
    }

    public BuyableField GetBuyableFieldThroughName(string category, string name)
    {
        return GameObject.Find("Fields").transform.Find(category).Find(name).GetComponent<BuyableField>();
    }

    private void CheckPlayer(int ID)
    {
        if(!Players.Any(player => player.ID == ID))
        {
            AddPlayer(null, ID);
        }
    }


    public void EndTurn()
    {
        if (ActivePlayer.BelongsToOwner && RolledDice())
        {
            NetworkingController.SendData(null, 2, true);
        }
    }

    public void StayOnField(FieldDefinition field, int DiceValue)
    {
        field.Stay(Players, ActivePlayer, DiceValue, CashController);
    }

    public void SetDiceValue(int value)
    {
        ActivePlayer.MovePlayer(value);
    }

    public void AddPlayer(PlayerFigure Player, int ID)
    {
        Players.Add(CreatePlayer(Player, ID));
        
        Players = SortPlayers(Players);
        
        SetActivePlayer(0);
        //Should be refactored
        //always the turn of the first Player on start
    }

    private List<PlayerFigure> SortPlayers(List<PlayerFigure> Players)
    {
        return Players.OrderBy(player => player.ID).ToList();
    }

    public PlayerFigure CreatePlayer(PlayerFigure PlayerInstance, int ID)
    {
        return settingsController.BuildPlayer(PlayerInstance, ID);
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
        foreach (Dice dice in Dices)
        {
            if (IsOwnerTurn())
            {
                dice.GetComponent<PhotonView>().RequestOwnership();
            }
            dice.SetDiceLock(!IsOwnerTurn());
        }
    }

    public bool IsOwnerTurn()
    {
        return ActivePlayer.BelongsToOwner;
    }

    public PlayerFigure GetPlayerOfOwner()
    {
        return Players.Where(player => player.BelongsToOwner).Single();
    }

    public PlayerFigure GetPlayerThroughID(int ID)
    {
        return Players.Where(player => player.ID == ID).Single();
    }
}
