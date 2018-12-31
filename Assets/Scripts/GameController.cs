using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Dice[] Dices;
    public List<PlayerFigure> Players;
    public DialogController dialogController;
    public PlayerFigure ActivePlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StayOnField(FieldDefinition field, int DiceValue)
    {
        field.Stay(Players, ActivePlayer, DiceValue, dialogController);
    }

    public void SetDiceValue(int value)
    {
        ActivePlayer.MovePlayer(value);
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
        if (IsOwnerTurn())
        {
            foreach(Dice dice in Dices)
            {
                dice.SetDiceLock(false);
            }
        }
        else
        {
            //get Random Force and apply it
            //dice1.RollDice(x, y, z);
            //dice2.RollDice(x2, y2, z2);
        }
    }

    public bool IsOwnerTurn()
    {
        //check if ActivePlayer == Owner
        return true;
    }
}
