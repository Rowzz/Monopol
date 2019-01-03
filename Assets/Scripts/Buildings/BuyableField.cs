using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyableField : FieldDefinition
{
    public int Price;
    public int[] Rent;
    internal bool Mortgage;


    internal abstract int GetRent();
    internal abstract void OnMouseDown();

    internal abstract int GetValue();
}
