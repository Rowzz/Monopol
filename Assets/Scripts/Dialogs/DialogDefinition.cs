using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class DialogDefinition : MonoBehaviour
{

    public void Awake()
    {
        SetGameObjectVisibility(false);
    }

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
        }
    }

    public void AddCloseEvent(params Button[] Buttons)
    {
        foreach (Button button in Buttons)
        {
            button.onClick.AddListener(Close);
        }
    }

    public void BuyDialog(Button YesButton, Button NoButton, bool ReadOnly, UnityAction YesClick)
    {
        Reset(YesButton, NoButton);
        SetGameObjectVisibility(true);

        if (!ReadOnly)
        {
            YesButton.onClick.AddListener(YesClick);

            AddCloseEvent(YesButton, NoButton);
        }
    }
    
}
