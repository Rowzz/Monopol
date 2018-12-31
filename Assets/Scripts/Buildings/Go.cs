using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : FieldDefinition
{
    public int Amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hover(PlayerFigure playerFigure)
    {
        playerFigure.Balance += Amount;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, DialogController DialogController)
    {
        ActivePlayer.Balance += Amount;
    }
}
