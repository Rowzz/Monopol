using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class DialogDefinition : MonoBehaviour
{
    internal bool FadeOut = false;
    private readonly float Step = 0.4f;

    internal abstract void Init();

    private void FixedUpdate()
    {
        if (FadeOut)
        {
            var comp = gameObject.GetComponent<CanvasGroup>();
            float step = Time.fixedDeltaTime * Step;

            if (comp.alpha <= step)
            {
                SetGameObjectVisibility(false);
                DisableFadeOut();
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().alpha -= step;
            }
        }
    }

    internal void EnableFadeOut()
    {
        SetFadeOutStatus(true);
    }

    internal void DisableFadeOut()
    {
        SetFadeOutStatus(false);
    }

    private void SetFadeOutStatus(bool Status)
    {
        FadeOut = Status;
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
            button.GetComponent<Image>().color = Color.white;
            button.gameObject.SetActive(true);
        }
    }

    internal void ResetGameObject()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        SetGameObjectVisibility(true);
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
