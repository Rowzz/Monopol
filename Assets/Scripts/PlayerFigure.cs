using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public FieldDefinition currentField;
    private int balance;
    public int Balance {get { return balance; } set {
            if (BelongsToOwner)
            {
                gameController.DialogController.SetPlayerBalance(value);
            }
            balance = value;
        } }
    private List<FieldDefinition> OwnedBuildings = new List<FieldDefinition>(); // when you want to list all your/your opponents buildings. redundancy for speed
    public int PlayerMovementSpeed;
    public GameController gameController;
    private Vector3 NextPosition;
    private int PositionsToGo;
    private readonly float MoveFieldTolerance = 0.2f;
    private int DiceResult;
    public bool BelongsToOwner;
    public int? ProgrammaticVal = null;
    public int ID;
    //Cards (e.g. escape jail)

    // Start is called before the first frame update
    void Start()
    {
        OwnedBuildings = new List<FieldDefinition>();
    }

    // Update is called once per frame
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

    public void AddBuilding(FieldDefinition building)
    {
        OwnedBuildings.Add(building);
    }

    public void RemoveBuilding(FieldDefinition building)
    {
        OwnedBuildings.Remove(building);
    }
}
