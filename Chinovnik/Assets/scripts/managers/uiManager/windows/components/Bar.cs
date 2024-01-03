using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class Bar : RectComponent
{
    [SerializeField] private Image main;
    [SerializeField] private Image bar;
    [SerializeField] private Text text;

    public float Percent
    {
        set => bar.rectTransform.sizeDelta = new Vector2(main.rectTransform.sizeDelta.x * Mathf.Clamp01(value), bar.rectTransform.sizeDelta.y);
    }

    public string Text
    {
        get => text.text;
        set => text.text = value;
    }

}
