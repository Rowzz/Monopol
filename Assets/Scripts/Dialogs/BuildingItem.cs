using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingItem : MonoBehaviour, IPointerEnterHandler
{
    internal BuyableField Field;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Field is Building)
        {
            InstanceController.GetDialogController().ShowBuilding((Building)Field, InstanceController.GetPlayerBuildingsDialogFieldInformation());
        }
    }
}
