using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    internal string Currency = "€";


    internal string FormatNumber(int Number)
    {
        return Number + Currency;
    }

    internal PlayerFigure BuildPlayer(PlayerFigure PlayerInstance, int ID)
    {
        PlayerFigure Player = PlayerInstance ?? new PlayerFigure();
        Player.ID = ID;
        Player.currentField = GameObject.Find("Start").GetComponent<FieldDefinition>();
        Player.BelongsToOwner = PlayerInstance != null;
        Player.Balance = 1900;
        Player.PlayerMovementSpeed = 10;


        foreach (Transform transform in GameObject.Find("Brown").transform)
        {
            BuyableField Field = transform.GetComponent<BuyableField>();
            Field.Owner = Player;
            Player.AddBuilding(Field);
        }


        return Player;
    }

    internal void FormatNumber(Text text, int result)
    {
        if (result > 0)
        {
            text.color = Color.green;
        }
        else if (result < 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.black;
        }
        text.text = FormatNumber(result);
        
    }
}
