using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking : FieldDefinition
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
        return false;
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        return;
    }

    public override void Stay(PlayerFigure[] Players, int ActivePlayer, int Dicevalue)
    {
        //something here?
        throw new System.NotImplementedException();
    }
}
