using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogDefinition : MonoBehaviour
{
    public GameObject Dialog;

    private void Awake()
    {
        SetGameObjectVisibility(false);
    }

    public void Close()
    {
        SetGameObjectVisibility(false);
    }

    public void SetGameObjectVisibility(bool status)
    {
        Dialog.SetActive(status);
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
}
