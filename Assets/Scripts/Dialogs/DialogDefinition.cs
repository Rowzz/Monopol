﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class DialogDefinition : MonoBehaviour
{
    public abstract void Init();

    public void Close()
    {
        SetGameObjectVisibility(false);
    }

    public void SetGameObjectVisibility(bool status)
    {
        gameObject.SetActive(status);
    }

    public void Reset(params Button[] Buttons)
    {
        foreach (Button button in Buttons)
        {
            button.onClick.RemoveAllListeners();
            button.GetComponent<Image>().color = Color.white;
        }
    }

    public void AddCloseEvent(params Button[] Buttons)
    {
        foreach (Button button in Buttons)
        {
            button.onClick.AddListener(Close);
        }
    }

    internal abstract void SetYesButtonColor();
    internal abstract void SetNoButtonColor();

    public void BuyDialog(Button YesButton, Button NoButton, bool ReadOnly, Action<string> YesClick, Action<string> NoClick)
    {
        Reset(YesButton, NoButton);
        SetGameObjectVisibility(true);

        if (!ReadOnly)
        {
            YesButton.onClick.AddListener(delegate() { YesClick(name); });
            NoButton.onClick.AddListener(delegate () { NoClick(name); });
            AddCloseEvent(YesButton, NoButton);
        }
    }

    public void SetButtonText(Button button, string Text) {
        button.transform.GetChild(0).GetComponent<Text>().text = Text;
    }

    internal GameObject FindChild(params string[] Names)
    {
        Transform result = gameObject.transform;

        foreach (string Name in Names)
        {
            result = result.Find(Name);
        }
        return result.gameObject;
    }

}