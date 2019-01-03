using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildings : DialogDefinition
{

    internal override void Init()
    {
        
    }

    internal void Showdialog(PlayerFigure Player, int? Amount)
    {
        //Changes:
        //- Hypothek
        //- Häuser

        //if Amount == null: only Hypothek

    }

    internal override void SetNoButtonColor()
    {
        return;
    }

    internal override void SetYesButtonColor()
    {
        return;
    }
}
