using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : FieldDefinition
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Buyable()
    {
        return true;
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, GameController gameController)
    {
        if (Owner == null)
        {
            //Buy?
        }
        else if (ActivePlayer != Owner)
        {
            //Rent
        }
        else
        {
            //Buy House/Hotel?
        }
        throw new System.NotImplementedException();
    }
}
