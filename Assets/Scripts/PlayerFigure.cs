using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public Building currentBuilding;
    public int balance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        currentBuilding = other.GetComponent<Building>();
        Debug.Log("Collider!!");
    }
}
