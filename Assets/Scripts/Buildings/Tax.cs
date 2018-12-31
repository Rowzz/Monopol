using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tax : FieldDefinition
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
        return;
    }

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, DialogController DialogController)
    {
        if(ActivePlayer.Balance >= Amount)
        {
            ActivePlayer.Balance -= Amount;
        }
        else
        {
            DialogController.SellFields(ActivePlayer, null, Amount - ActivePlayer.Balance);
        }
    }
}
