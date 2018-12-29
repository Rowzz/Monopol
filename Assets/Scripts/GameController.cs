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
            if (MovePlayer(activePlayer))
            {
                movingPositionsLeft--;
                FieldDefinition activeField;
                Debug.Log((activeField = GetCurrentBuilding(activePlayer)).GetType());
                if (PlayerMoving = movingPositionsLeft > 0)
                {
                    SetNextBuildingPosition(activePlayer);
                }
                else if ((activeField = GetCurrentBuilding(activePlayer)).GetType() == typeof(Building))
                {
                    Building activeBuilding = (Building)activeField;
                    PlayerFigure pf = GetActivePlayer(activePlayer);


                    //owned bei nobody and enough money to buy it
                    if (activeBuilding.Owner == null && pf.Balance >= activeBuilding.Price)
                    {
                        //show Buy-Dialog
                        //if (form.ShowDialog() == DialogResult.Yes)
                        //{
                        //    pf.Balance -= activeBuilding.Price;
                        //    AddBuilding(activeBuilding,pf);
                        //    if (OwnsEveryBuildingOfCategory(activeBuilding, pf))
                        //    {
                        //        AddColourSetBonus(activeBuilding);
                        //    }
                        //}
                    }
                    //pay rent
                    else if (activeBuilding.Owner != null && activeBuilding.Owner != pf)
                    {
                        int rent = activeBuilding.GetRent();
                        //show Pay-Dialog
                        if (pf.Balance >= rent)
                        {
                            pf.Balance -= rent;
                            activeBuilding.Owner.Balance += rent;
                        }
                        else //sell buildings
                        {
                            //switch Player

                            //and foreach(Building building in soldBuildings){ pf.removeBuilding(activeBuilding)};
                        }
                    }
                    //want to buy houses/hotel?
                    else if (OwnsEveryBuildingOfCategory(activeBuilding, pf) && !activeBuilding.IsFullyUpgraded())
                    {
                        //ShowDialog
                    }

                }
            }
        }
        else
        {
            if (ValidRoll(out int result))//Dice rolled
            {
                ResetDiceValues();
                //move Player
                movingPositionsLeft = 25;
                PlayerMoving = true;
                
                SetNextBuildingPosition(activePlayer);
            }
        }
    }

    private void AddBuilding(FieldDefinition building, PlayerFigure player)
    {
        building.Owner = player;
        player.AddBuilding(building);
    }

    private bool OwnsEveryBuildingOfCategory(FieldDefinition activeBuilding, PlayerFigure player)
    {
        bool valid = true;
        foreach(FieldDefinition building in activeBuilding.gameObject.transform.parent.gameObject.GetComponentsInChildren<FieldDefinition>())
        {
            valid = valid && building.Owner == player;
        }
        return valid;
    }

    private void AddColourSetBonus(Building activeBuilding)
    {
        foreach (Building building in activeBuilding.gameObject.transform.parent.gameObject.GetComponentsInChildren<Building>())
        {
            building.FullSet();
        }
    }

    private PlayerFigure GetActivePlayer(int playerIndex)
    {
        return Players[playerIndex].GetComponent<PlayerFigure>();
    }

    private bool MovePlayer(int playerIndex)
    {
        Transform PlayerTransform = Players[playerIndex].transform;
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, toPosition, Time.deltaTime);
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
