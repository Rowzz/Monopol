using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Dices;
    public PlayerFigure[] Players;
    private int DiceResult;
    private bool PlayerMoving;
    private Vector3 toPosition;
    private int movingPositionsLeft;
    private int activePlayer = 0;
    private int PlayerMovementSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMoving)
        {
            if (MovePlayer(activePlayer))
            {
                movingPositionsLeft--;
                FieldDefinition activeField = GetCurrentBuilding(activePlayer);
                activeField.Hover(GetActivePlayer(activePlayer));
                if (PlayerMoving = movingPositionsLeft > 0)
                {
                    SetNextBuildingPosition(activePlayer);
                }
                else
                {
                    activeField.Stay(Players,activePlayer,DiceResult);
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
                
                SetNextBuildingPosition(activePlayer);
            }
        }
    }

    private PlayerFigure GetActivePlayer(int playerIndex)
    {
        return Players[playerIndex].GetComponent<PlayerFigure>();
    }

    private bool MovePlayer(int playerIndex)
    {
        Transform PlayerTransform = Players[playerIndex].transform;
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, toPosition, Time.deltaTime*PlayerMovementSpeed);
        return Vector3.Distance(PlayerTransform.position,toPosition) < 0.25;
    }

    private void SetNextBuildingPosition(int playerIndex)
    {
        toPosition = GetCurrentBuilding(playerIndex).Next.transform.position;
    }

    private FieldDefinition GetCurrentBuilding(int playerIndex)
    {
        return GetActivePlayer(playerIndex).currentField;
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
