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
                else
                {
                    Building activeBuilding = getCurrentBuilding(activePlayer);
                    PlayerFigure pf = getActivePlayer(activePlayer);
                    

                    //owned bei nobody and enough money to buy it
                    if(activeBuilding.Owner == null && pf.balance >= activeBuilding.Price)
                    {
                        //show Buy-Dialog
                        //if (form.ShowDialog() == DialogResult.Yes)
                        //{
                        //    pf.balance -= activeBuilding.Price;
                        //    activeBuilding.Owner = pf;
                        //    if(ownsEveryBuildingOfCategory(activeBuilding, pf))
                        //    {
                        //        addColourSetBonus(activeBuilding);
                        //    }
                        //}
                    }
                    else if(activeBuilding.Owner != null && activeBuilding.Owner != pf)
                    {
                        int rent = activeBuilding.getRent();
                        //show Pay-Dialog
                        if (pf.balance >= rent)
                        {
                            pf.balance -= rent;
                        }
                        else //sell buildings
                        {

                        }
                    }
                    else if(ownsEveryBuildingOfCategory(activeBuilding,pf))
                    {
                        //want to buy houses/hotel?
                        //ShowDialog
                    }
                    
                }
            }
        }
        else
        {
            if (validRoll(out int result))//Dice rolled
            {
                resetDiceValues();
                //move Player
                movingPositionsLeft = result;
                PlayerMoving = true;
                
                setNextBuildingPosition(activePlayer);
            }
        }
    }

    private bool ownsEveryBuildingOfCategory(Building activeBuilding, PlayerFigure player)
    {
        bool valid = true;
        foreach(Building building in activeBuilding.gameObject.transform.parent.gameObject.GetComponentsInChildren<Building>())
        {
            valid = valid && building.Owner == player;
        }
        return valid;
    }

    private void addColourSetBonus(Building activeBuilding)
    {
        foreach (Building building in activeBuilding.gameObject.transform.parent.gameObject.GetComponentsInChildren<Building>())
        {
            building.FullSet();
        }
    }

    private PlayerFigure getActivePlayer(int playerIndex)
    {
        return Players[playerIndex].GetComponent<PlayerFigure>();
    }

    private bool movePlayer(int playerIndex)
    {
        Transform PlayerTransform = Players[playerIndex].transform;
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, toPosition, Time.deltaTime*10);
        return Vector3.Distance(PlayerTransform.position,toPosition) < 0.25;
    }

    private void setNextBuildingPosition(int playerIndex)
    {
        toPosition = getCurrentBuilding(playerIndex).Next.transform.position;
    }

    private Building getCurrentBuilding(int playerIndex)
    {
        return getActivePlayer(playerIndex).currentBuilding;
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
