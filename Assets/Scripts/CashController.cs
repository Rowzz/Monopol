using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CashController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public GameController gameController;
    private DialogController DialogController { get {return gameController.DialogController; } }
    private bool ReadOnly { get { return !gameController.IsOwnerTurn(); } }

    //DON'T FORGET TO SET READONLY!!
    //can be called with ReadOnly()

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            //BuyBuilding: Yes
            case 10:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    PlayerFigure ActivePlayer = gameController.GetPlayerThroughID((int)data[0]);
                    BuyableField Field = gameController.GetBuyableFieldThroughName(data[1].ToString(), data[2].ToString());
                    int Price = (int)data[3];
                    BuyFieldSync(Field, Price, ActivePlayer);
                    DialogDefinition Dialog = DialogController.GetDialogThroughName(data[4].ToString());
                    Dialog.SetYesButtonColor();
                }
                break;

            //BuyBuilding: No
            case 11:
                {
                    DialogDefinition Dialog = DialogController.GetDialogThroughName(photonEvent.CustomData.ToString());
                    Dialog.SetNoButtonColor();
                }
                break;

            //Pay Rent
            case 12:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    PlayerFigure ActivePlayer = gameController.GetPlayerThroughID((int)data[0]);
                    int Amount = (int)data[1];
                    FieldDefinition Field = gameController.GetFieldThroughName(data[2].ToString(), data[3].ToString());

                    PayAmountSync(ActivePlayer,Amount,Field.Owner);
                    DialogController.PayRent(Amount, Field);
                }
                break;

            //Sell Fields
            case 13:
                {

                }
                break;
        }
    }

    public void BuyHouse(Building building, PlayerFigure ActivePlayer)
    {
        int Houses = building.HouseCount;
        int Hotels = building.HotelCount;
        int PricePerHotel = building.PricePerHotel;
        int PricePerHouse = building.PricePerHouse;
        int MaxHouses = building.MaxHouses;
        int MaxHotels = building.MaxHotels;
        int BoughtHotels = 0, BoughtHouses = 0;
        //showDialog

        ActivePlayer.Balance -= BoughtHouses * PricePerHouse + BoughtHotels * PricePerHotel;
        building.AddHouse(Houses, Hotels);

        throw new System.NotImplementedException();
    }

    public void PayRent(FieldDefinition Field, int Rent, PlayerFigure ActivePlayer)
    {
        //FieldDefinition because Tax is seen as Rent
        if(EnoughMoney(ActivePlayer, Rent))
        {
            PayAmount(ActivePlayer, Rent, Field.GetParent().name, Field.name);
        }
        else
        {
            int Amount = Rent - ActivePlayer.Balance;
            SellFields(ActivePlayer, Amount);

            PayAmount(ActivePlayer, Rent, Field.GetParent().name, Field.name);
        }
    }


    private void PayAmount(PlayerFigure ActivePlayer, int Amount, string Category, string Field)
    {
        if (!ReadOnly) //Only ActivePlayer sends this message. else n players send this message to n players
        {
            NetworkingController.SendData(new object[] { ActivePlayer.ID, Amount, Category, Field }, 12, true);
        }
    }

    private void PayAmountSync(PlayerFigure ActivePlayer, int Amount, PlayerFigure Owner)
    {
        ActivePlayer.Balance -= Amount;
        if (Owner != null)
        {
            Owner.Balance += Amount;
        }
    }

    public void BuyBuilding(Building Building, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer,Building.Price))
        {
            DialogController.BuyBuilding(Building, ReadOnly, BuyFieldYesAction(Building, Building.Price,ActivePlayer), BuyFieldNo);
        }
    }

    public void BuyUtility(Utility Utility, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer, Utility.Price))
        {
            DialogController.BuyUtility(Utility, ReadOnly, BuyFieldYesAction(Utility, Utility.Price, ActivePlayer), BuyFieldNo);
        }
    }

    public void BuyRailwayStation(RailwayStation RailwayStation, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer, RailwayStation.Price))
        {
            DialogController.BuyRailwayStation(RailwayStation, ReadOnly, BuyFieldYesAction(RailwayStation, RailwayStation.Price, ActivePlayer), BuyFieldNo);
        }
    }

    private Action<string> BuyFieldYesAction(BuyableField Field, int Price, PlayerFigure ActivePlayer)
    {
        return delegate(string Name) { BuyFieldYes(Field, Price, ActivePlayer, Name); };
    }

    private void BuyFieldNo(string Name)
    {
         NetworkingController.SendData(Name, 11, true);
    }

    private bool EnoughMoney(PlayerFigure ActivePlayer, int Price)
    {
        return ActivePlayer.Balance >= Price;
    }

    private void BuyFieldYes(BuyableField Field, int Price, PlayerFigure ActivePlayer, string Name)
    {
        NetworkingController.SendData(new object[] { ActivePlayer.ID, Field.GetParent().name, Field.name, Price, Name}, 10, true);
    }

    private void BuyFieldSync(BuyableField Field, int Price, PlayerFigure ActivePlayer)
    {
        ActivePlayer.Balance -= Price;
        AddField(ActivePlayer, Field);
    }

    private void AddField(PlayerFigure ActivePlayer, BuyableField Field)
    {
        ActivePlayer.AddBuilding(Field);
        Field.Owner = ActivePlayer;
    }

    public void SellFields(PlayerFigure PlayerFrom, int Amount)
    {
        //Hypothek
        //Amount = Amount left
        //sell Fields till amount <=0
        if (!ReadOnly) {
            if (PlayerFrom.GetTotalValue() < Amount)
            {
                //Player lost
                throw new NotImplementedException();
            }
            else
            {
                DialogController.ShowBuildingsOfPlayer(Amount);
            }
        }
    }

    public void DrawChanceCard(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int DiceValue)
    {
        //Draw Chance Card
        throw new System.NotImplementedException();
    }

    public void DrawChestCard(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int DiceValue)
    {
        //Draw Chest Card
        throw new System.NotImplementedException();
    }
}
