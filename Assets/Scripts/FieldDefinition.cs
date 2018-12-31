using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract public class FieldDefinition : MonoBehaviour
{
    public FieldDefinition Next;
    public FieldDefinition Before;
    public PlayerFigure Owner;
    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Hover(PlayerFigure playerFigure);
    public abstract void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, CashController dialogController);

    public GameObject GetParent()
    {
        return gameObject.transform.parent.gameObject;
    }

    public bool OwnsEveryBuildingOfCategory()
    {
        return GetChildsOfParent().All(Field => Field.Owner == Owner);
    }

    public int GetOwnedChildrenCount()
    {
        return GetChildsOfParent().Where(station => station.Owner == Owner).Count();
    }

    private List<FieldDefinition> GetChildsOfParent()
    {
        return new List<FieldDefinition>(GetParent().GetComponentsInChildren<FieldDefinition>());
    }
}
