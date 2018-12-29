using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Dices;
    public List<PlayerFigure> Players;
    private int DiceResult;
    private bool PlayerMoving;
    private Vector3 toPosition;
    private int movingPositionsLeft;
    public PlayerFigure ActivePlayer;
    private readonly int PlayerMovementSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMoving)
        {
            if (MovePlayer())
            {
                movingPositionsLeft--;
                FieldDefinition activeField = GetCurrentBuilding();
                activeField.Hover(ActivePlayer);
                if (PlayerMoving = movingPositionsLeft > 0)
                {
                    SetNextBuildingPosition();
                }
                else
                {
                    activeField.Stay(Players,ActivePlayer,DiceResult, this);
                }
            }
        }
        else
        {
            if (ValidRoll(out int result))//Dice rolled
            {
                ResetDiceValues();
                //move Player
                movingPositionsLeft = DiceResult = result;
                PlayerMoving = true;
                
                SetNextBuildingPosition();
            }
        }
    }

    public void SellFields(PlayerFigure PlayerFrom, PlayerFigure PlayerTo, int Amount)
    {
        if (PlayerTo != null)
        {
            //sell Fields till amount <=0
            List<FieldDefinition> soldFields = new List<FieldDefinition>();
            //but first sell houses of buildings, then sell buildings. don't forget to remove or to add "FullColourSet" of building
            foreach (FieldDefinition field in soldFields)
            {
                PlayerFrom.RemoveBuilding(field);
                field.Owner = PlayerTo;
            }
        }
        else
        {
            //Hypothek
            //soldFields.setLocked(true)
        }
        throw new System.NotImplementedException();
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
    }


    private bool MovePlayer()
    {
        Transform PlayerTransform = ActivePlayer.transform;
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, toPosition, Time.deltaTime*PlayerMovementSpeed);
        return Vector3.Distance(PlayerTransform.position,toPosition) < 0.25;
    }

    private void SetNextBuildingPosition()
    {
        toPosition = GetCurrentBuilding().Next.transform.position;
    }

    private FieldDefinition GetCurrentBuilding()
    {
        return ActivePlayer.currentField;
    }

    private bool ValidRoll(out int result)
    {
        bool valid = true;
        result = 0;
        foreach (GameObject dice in Dices)
        {
            int value = (dice.GetComponent<Dice>()).diceValue;
            result += value;
            valid = valid && value > 0;
        }
        return valid;
    }

    private void ResetDiceValues()
    {
        foreach (GameObject dice in Dices)
        {
            (dice.GetComponent<Dice>()).diceValue = 0;
        }
    }
}
