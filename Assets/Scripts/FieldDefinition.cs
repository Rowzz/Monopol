using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FieldDefinition : MonoBehaviour
{
    public FieldDefinition Next;
    public FieldDefinition Before;
    public PlayerFigure Owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract bool Buyable();
    public abstract void Hover(PlayerFigure playerFigure);
    public abstract void Stay(List<PlayerFigure> Players, PlayerFigure ActivePlayer, int Dicevalue, GameController gameController);

    public GameObject GetParent()
    {
        return gameObject.transform.parent.gameObject;
    }
}
