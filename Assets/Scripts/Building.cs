using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject Next;
    public GameObject Before;
    public PlayerFigure Owner;
    public int Price;
    public int[] Rent;
    private int RentPointer=0;
    public int PricePerHouse;
    public int PricePerHotel;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHouse()
    {
        increaseRent();
    }

    private void increaseRent()
    {
        RentPointer++;
    }

    public void FullSet()
    {
        increaseRent();
    }

    public int getRent()
    {
        return Rent[RentPointer];
    }
}
