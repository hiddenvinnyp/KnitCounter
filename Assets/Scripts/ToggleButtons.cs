using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtons : MonoBehaviour
{
    [Header("Изнаночная")]
    [SerializeField] private Image purlButton;
    [Header("Лицевая")]
    [SerializeField] private Image knitButton;

    public static event Action<int> OnStitchClicked;

    private Color colorUnactive = Color.white;
    private Color colorActive = Color.red;

    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.StitchChanged += StitchUpdate;
    }

    private void StitchUpdate(int stitch)
    {
        if (stitch < 0 || stitch > 1) { return; }
        if (stitch == 0) 
        {
            PurlStitchButton();
        }
        else
        {
            KnitStitchButton();
        }        
    }

    public void KnitStitchButton()
    {
        OnStitchClicked?.Invoke(1);
        purlButton.color = colorUnactive;
        knitButton.color = colorActive;
    }

    public void PurlStitchButton()
    {
        OnStitchClicked?.Invoke(0);
        purlButton.color = colorActive;
        knitButton.color = colorUnactive;
    }

    private void OnDestroy()
    {
        SaveSystem.StitchChanged -= StitchUpdate;
    }
}
