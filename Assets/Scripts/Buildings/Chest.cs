using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : FieldDefinition
{

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

    public override void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, NotificationController notificationController)
    {
        //Draw Chest-Card
        throw new System.NotImplementedException();
    }
}
