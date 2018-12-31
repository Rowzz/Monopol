using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Button YesButton;
    public Button NoButton;
    public GameObject BuyFieldDialog;

    private void Awake()
    {
        SetGameObjectVisibility(BuyFieldDialog, false);
    }

    public void BuyField(string Name, int Price, int Balance, bool ReadOnly, UnityAction YesClick)
    {
        Reset();
        SetGameObjectVisibility(BuyFieldDialog, true);
        if (!ReadOnly)
        {
            UnityAction CloseAction = delegate () { Close(BuyFieldDialog); };

            YesButton.onClick.AddListener(YesClick);
            YesButton.onClick.AddListener(CloseAction);
            NoButton.onClick.AddListener(CloseAction);
        }
    }

    private void Close(GameObject gameObject)
    {
        SetGameObjectVisibility(gameObject, false);
    }

    private void SetGameObjectVisibility(GameObject gameObject, bool status)
    {
        gameObject.SetActive(status);
    }

    private void Reset()
    {
        YesButton.onClick.RemoveAllListeners();
        NoButton.onClick.RemoveAllListeners();
    }
}
