using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Dices;
    public GameObject[] Players;
    private int DiceResult;
    private bool PlayerMoving;
    private Vector3 toPosition;
    private int movingPositionsLeft;
    private int activePlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMoving)
        {
            if(movePlayer(activePlayer))
            {
                movingPositionsLeft--;
                if (PlayerMoving = movingPositionsLeft > 0)
                {
                    setNextBuildingPosition(activePlayer);
                }
            }
        }
        else
        {
            if (validRoll(out int result))//Dice rolled
            {
                resetDiceValues();
                //move Player
                movingPositionsLeft = 12;
                PlayerMoving = true;
                
                setNextBuildingPosition(activePlayer);
            }
        }
    }

    private bool movePlayer(int playerIndex)
    {
        Transform PlayerTransform = Players[playerIndex].transform;
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, toPosition, Time.deltaTime*10);
        return Vector3.Distance(PlayerTransform.position,toPosition) < 0.25;
    }

    private void setNextBuildingPosition(int playerIndex)
    {
        toPosition = Players[playerIndex].GetComponent<PlayerFigure>().currentBuilding.Next.transform.position;
    }

    private bool validRoll(out int result)
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

    private void resetDiceValues()
    {
        foreach (GameObject dice in Dices)
        {
            (dice.GetComponent<Dice>()).diceValue = 0;
        }
    }
}
