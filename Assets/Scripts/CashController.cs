using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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
                    FieldDefinition Field = gameController.GetFieldThroughName(data[1].ToString(), data[2].ToString());
                    int Price = (int)data[3];
                    BuyFieldSync(Field, Price, ActivePlayer);
                    DialogController.HideDialogs();
                }
                break;

            //BuyBuilding: No
            case 11:
                {
                    DialogController.HideDialogs();
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
        if(EnoughMoney(ActivePlayer, Rent)) //show Dialog?
        {
            PayAmount(ActivePlayer, Rent, Field.Owner);
        }
        else
        {
            int Amount = Rent - ActivePlayer.Balance;
            SellFields(ActivePlayer, null, Amount);
            PayAmount(ActivePlayer, Amount, Field.Owner);
        }
    }

    private void PayAmount(PlayerFigure ActivePlayer, int Amount, PlayerFigure Owner)
    {
        ActivePlayer.Balance -= Amount;
        if(Owner != null)
        {
            Owner.Balance += Amount;
        }
    }

    public void BuyBuilding(Building Building, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer,Building.Price))
        {
            Debug.Log(ReadOnly);
            DialogController.BuyBuilding(Building, ReadOnly, BuyFieldYesAction(Building, Building.Price,ActivePlayer), BuyFieldNo);
        }
    }

    public void BuyUtility(Utility Utility, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer, Utility.Price))
        {
            DialogController.BuyUtility(Utility, ActivePlayer.Balance, ReadOnly, BuyFieldYesAction(Utility, Utility.Price, ActivePlayer), BuyFieldNo);
        }
    }

    public void BuyRailwayStation(RailwayStation RailwayStation, PlayerFigure ActivePlayer)
    {
        if (EnoughMoney(ActivePlayer, RailwayStation.Price))
        {
            DialogController.BuyRailwayStation(RailwayStation, ActivePlayer.Balance, ReadOnly, BuyFieldYesAction(RailwayStation, RailwayStation.Price, ActivePlayer), BuyFieldNo);
        }
    }

    private UnityAction BuyFieldYesAction(FieldDefinition Field, int Price, PlayerFigure ActivePlayer)
    {
        return delegate () { BuyFieldYes(Field, Price, ActivePlayer); };
    }

    private bool EnoughMoney(PlayerFigure ActivePlayer, int Price)
    {
        return ActivePlayer.Balance >= Price;
    }

    private void BuyFieldYes(FieldDefinition Field, int Price, PlayerFigure ActivePlayer)
    {
        NetworkingController.SendData(new object[] { ActivePlayer.ID, Field.GetParent().name, Field.name, Price}, 10, true);
    }

    private void BuyFieldNo()
    {
        NetworkingController.SendData(null, 11, false);
    }

    private void BuyFieldSync(FieldDefinition Field, int Price, PlayerFigure ActivePlayer)
    {
        ActivePlayer.Balance -= Price;
        AddField(ActivePlayer, Field);
    }

    private void AddField(PlayerFigure ActivePlayer, FieldDefinition Field)
    {
        ActivePlayer.AddBuilding(Field);
        Field.Owner = ActivePlayer;
    }

    public void SellFields(PlayerFigure PlayerFrom, PlayerFigure PlayerTo, int Amount)
    {
        if (PlayerTo != null)
        {
            //sell Fields till amount <=0
            List<FieldDefinition> soldFields = new List<FieldDefinition>();

            //Dictionary<Building,HouseHotelMatrix>
            //foreach Key in dict
            //key.removeHouses(val.HousesCount, val.HotelCount);

            //but first sell houses of buildings, then sell buildings
            foreach (FieldDefinition field in soldFields)
            {
                PlayerFrom.RemoveBuilding(field);
                field.Owner = PlayerTo;
            }
        }
        else //if null then he is paying tax or something
        {
            //Hypothek
            //soldFields.setLocked(true)
        }
        throw new System.NotImplementedException();
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
