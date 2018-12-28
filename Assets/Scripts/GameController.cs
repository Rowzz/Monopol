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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMoving)
        {
            Players[0].transform.position = Vector3.MoveTowards(Players[0].transform.position, toPosition, Time.deltaTime * 2);
            if(Players[0].transform.position == toPosition)
            {
                movingPositionsLeft--;
                if (movingPositionsLeft > 0)
                {
                    toPosition = Players[0].GetComponent<PlayerFigure>().currentBuilding.Next.transform.position;
                }
            }
        }
        else
        {
            bool valid = true;
            int result = 0;
            foreach (GameObject dice in Dices)
            {
                int value = (dice.GetComponent<Dice>()).diceValue;
                result += value;
                valid = valid && value > 0;
            }

            if (valid)//Dice rolled
            {
                resetDiceValues();
                //move Player
                movingPositionsLeft = result;
                PlayerMoving = true;
                toPosition = Players[0].GetComponent<PlayerFigure>().currentBuilding.Next.transform.position;
            }
        }
    }

    private void resetDiceValues()
    {
        foreach (GameObject dice in Dices)
        {
            (dice.GetComponent<Dice>()).diceValue = 0;
        }
    }
}
