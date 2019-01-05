using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyableField : FieldDefinition
{
    public int Price;
    public int[] Rent;
    internal bool Mortgage;
    public int Order;


    internal abstract int GetRent();
    internal abstract void OnMouseDown();

    internal abstract int GetValue();
    internal abstract int GetValue(bool mortgage, int houseCount);
    internal virtual bool HasHouses()
    {
        return false;
    }
}
