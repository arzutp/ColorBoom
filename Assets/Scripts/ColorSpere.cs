using ENUMS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpere : MonoBehaviour
{
    public SpereColor spereColor;
    public Renderer spereRenderer;
    void Start()
    {
        ColorChange(spereColor);
    }

    public void ColorChange(SpereColor spereColor)
    {
        switch (spereColor)
        {
            case SpereColor.Red:
                spereRenderer.material.color = Color.red;
                break;
            case SpereColor.Blue:
                spereRenderer.material.color = Color.blue;
                break;
            case SpereColor.Green:
                spereRenderer.material.color = Color.green;
                break;
            case SpereColor.Yellow:
                spereRenderer.material.color = Color.yellow;
                break;
            default:
                Debug.LogWarning("Geçersiz renk seçimi");
                break;
        }

    }
}
