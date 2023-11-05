using ENUMS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpere : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField] List<ColorSpere> neightboorColorSpere = new List<ColorSpere>();
    public bool inLevel;
    [SerializeField] Rigidbody spereRB;
    [SerializeField] Transform parent;
    SpereColor color;
    public Renderer spereRenderer;
    public bool spereIsFire;
    int enumCount;
    int rand;
    float speed = 7f;
    [SerializeField]
    private float checkRadius;
    void Start()
    {
        if (inLevel)
            FindNeightbor();
        enumCount = Enum.GetValues(typeof(SpereColor)).Length;
        rand = UnityEngine.Random.Range(0, enumCount);
        ColorChange((SpereColor)rand);
    }

    private void SetPosition()
    {
        Vector3 target = parent.position;
        Vector3 pos = transform.position;
        Vector3 norm = target - pos;
        Vector3 force = norm.normalized * speed;
        spereRB.AddForce(force);
    }

    public void ColorChange(SpereColor spereColor)
    {
        switch (spereColor)
        {
            case SpereColor.Red:
                spereRenderer.material.color = Color.red;
                color = SpereColor.Red;
                break;
            case SpereColor.Blue:
                spereRenderer.material.color = Color.blue;
                color = SpereColor.Blue;
                break;
            case SpereColor.Green:
                spereRenderer.material.color = Color.green;
                color = SpereColor.Green;
                break;
            case SpereColor.Yellow:
                spereRenderer.material.color = Color.yellow;
                color = SpereColor.Yellow;
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

    Ray ray;
    public float spereCastRadius = 0.1f;
    Collider[] hits;
    public void FindNeightbor()
    {
        layer = LayerMask.NameToLayer("ColorSpere");

        hits = Physics.OverlapSphere(transform.position, transform.localScale.x * .5f);
        if (hits.Length > 0)
        {
            foreach (var item in hits)
            {

                if (item.gameObject.name != parent.gameObject.name && item.gameObject.name != this.gameObject.name)
                {
                    if (!neightboorColorSpere.Contains(item.transform.GetComponent<ColorSpere>()))
                    {
                        neightboorColorSpere.Add(item.transform.GetComponent<ColorSpere>());

                    }
                }
            }
        }
        //{
        //    neightboorColorSpere.Add(hit.transform.GetComponent<ColorSpere>());
        //    print(hit.transform.GetComponent<ColorSpere>().color + " spere color collider");
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.CompareTag("ColorSpere"))
        //{
        //    neightboorColorSpere.Add(collision.transform.GetComponent<ColorSpere>());   
        //    print(collision.transform.GetComponent<ColorSpere>().color + " spere color collider");
        //}
        if (spereIsFire)
        {
            if (collision.transform.CompareTag("ColorSpere"))
            {
                spereIsFire = false;
                spereRB.velocity = Vector3.zero;
                spereRB.constraints = RigidbodyConstraints.FreezeAll;
                this.transform.SetParent(GameManager.Instance.SpereParentTransform());
                FindNeightbor();
            }
        }
    }

}
