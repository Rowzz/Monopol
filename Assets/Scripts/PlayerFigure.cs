using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public FieldDefinition currentField;
    public int Balance;
    private List<FieldDefinition> OwnedBuildings;
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
        currentField = other.GetComponent<FieldDefinition>();
    }

    public void AddBuilding(FieldDefinition building)
    {
        OwnedBuildings.Add(building);
    }

    public void RemoveBuilding(FieldDefinition building)
    {
        OwnedBuildings.Remove(building);
    }
}
