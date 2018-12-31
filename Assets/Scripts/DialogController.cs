using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellFields(PlayerFigure PlayerFrom, PlayerFigure PlayerTo, int Amount)
    {
        bool ReadOnlyDialog = !gameController.IsOwnerTurn();
        
        if (PlayerTo != null)
        {
            //sell Fields till amount <=0
            List<FieldDefinition> soldFields = new List<FieldDefinition>();
            //but first sell houses of buildings, then sell buildings. don't forget to remove or to add "FullColourSet" of building
            foreach (FieldDefinition field in soldFields)
            {
                PlayerFrom.RemoveBuilding(field);
                field.Owner = PlayerTo;
            }
        }
        else
        {
            //Hypothek
            //soldFields.setLocked(true)
        }
        throw new System.NotImplementedException();
    }
}
