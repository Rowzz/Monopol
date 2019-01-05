using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public FieldDefinition currentField;
    private int balance;
    public int Balance {get { return balance; } set {
            if (BelongsToOwner)
            {
                InstanceController.GetDialogController().SetPlayerBalance(value);
            }
            balance = value;
        } }
    private List<BuyableField> OwnedBuildings; // when you want to list all your/your opponents buildings. redundancy for speed
    public int PlayerMovementSpeed;
    private Vector3 NextPosition;
    private int PositionsToGo;
    private readonly float MoveFieldTolerance = 0.2f;
    private int DiceResult;
    public bool BelongsToOwner;
    public int? ProgrammaticVal = null;
    public int ID;
    //Cards (e.g. escape jail)

    private void Awake()
    {
        OwnedBuildings = new List<BuyableField>();
    }

    void Update()
    {
        if (PositionsToGo > 0)
        {
            Move();
            if (NextFieldReached())
            {
                PositionsToGo--;
                SetNextPosition();

                if (PositionsToGo == 0)
                {
                    //gameController.StayOnField(currentField, DiceResult);
                    NetworkingController.SendData(new object[] { currentField.GetParent().name, currentField.name, DiceResult }, 0, true);
                    DiceResult = 0;
                }
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextPosition, Time.deltaTime * PlayerMovementSpeed);
    }

    private void SetNextPosition()
    {
        NextPosition = currentField.Next.transform.position;
    }

    private bool NextFieldReached()
    {
        return Vector3.Distance(transform.position, NextPosition) < MoveFieldTolerance;
    }

    public void MovePlayer(int ToGo)
    {
        if (DiceResult == 0) {
            DiceResult = ToGo;
        }
        else
        {
            SetNextPosition();
            PositionsToGo = DiceResult = ProgrammaticVal ?? ToGo + DiceResult;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentField = other.GetComponent<FieldDefinition>();
        currentField.Hover(this);
    }

    public void AddBuilding(BuyableField building)
    {
        OwnedBuildings.Add(building);
    }

    public void RemoveBuilding(BuyableField building)
    {
        OwnedBuildings.Remove(building);
    }

    internal int GetTotalValue() //without Balance
    {
        return OwnedBuildings.Sum(Field => Field.GetValue());
    }

    internal List<BuyableField> GetOwnedBuildings()
    {
        return OwnedBuildings.OrderBy(building => building.GetParent().GetComponent<Category>().Order).ThenBy(building => building.Order).ToList();
    }
}
