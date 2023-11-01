using ENUMS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpere : MonoBehaviour
{
    [SerializeField] Rigidbody spereRB;
    [SerializeField] Transform parent;
    public SpereColor spereColor;
    public Renderer spereRenderer;
    public bool spereIsFire;
    int enumCount;
    int rand;
    float speed = 7f;
    void Start()
    {
        enumCount = Enum.GetValues(typeof(SpereColor)).Length;
        rand = UnityEngine.Random.Range(0, enumCount);
        ColorChange((SpereColor)rand);
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

    private void FixedUpdate()
    {
        if(spereIsFire)
        {
            spereRB.velocity = (Vector3.up * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (spereIsFire)
        {
            if (other.CompareTag("ColorSpere"))
            {
                spereIsFire = false;
                spereRB.velocity = Vector3.zero;
                this.transform.SetParent(GameManager.Instance.SpereParentTransform());
            }
        }
    }
}
