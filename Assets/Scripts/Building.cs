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
    private int RentPointer=0; //0 rent, 1: rent + colour bonus, 2: 1 house, 3: 2 houses,...,6: hotel
    public int PricePerHouse;
    public int PricePerHotel;
    public bool IsPayableBuilding;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFullyUpgraded()
    {
        return RentPointer == 6;
    }

    public void AddHouse()
    {
        IncreaseRent();
    }

    private void IncreaseRent()
    {
        RentPointer++;
    }

    public void FullSet()
    {
        IncreaseRent();
    }

    public int GetRent()
    {
        return Rent[RentPointer];
    }
}
