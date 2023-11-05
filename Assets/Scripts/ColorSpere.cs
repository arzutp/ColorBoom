using ENUMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSpere : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField] List<ColorSpere> neighborColorSpere = new List<ColorSpere>();
    public bool inLevel;
    [SerializeField] Rigidbody spereRB;
    SpereColor color;
    public Renderer spereRenderer;
    public bool spereIsFire;
    int enumCount;
    int rand;
    float speed = 7f;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private float checkDistance;
    public List<ColorSpere> nearSpere = new List<ColorSpere>();
    void Start()
    {
        if (inLevel)
        {
            FindNeighbor();
        }
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
        FindNeighbor();
    }

    Ray ray;
    public float spereCastRadius = 0.1f;
    private List<Collider> hits = new List<Collider>();
  
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColorSpere"))
        {
            if (other.gameObject == gameObject) return;
            
            
          
        }
    }
    public void FindNeighbor()
    {
        Debug.Log("Check Neighbours");
        layer = LayerMask.NameToLayer("ColorSpere");

        hits = Physics.OverlapSphere(transform.position, checkRadius).Where(t=>Vector3.Distance(t.transform.position, transform.position) < checkDistance).ToList();
        if (hits.Count > 0)
        {
            if (hits.Contains(GetComponent<Collider>()))
            {
                hits.Remove(GetComponent<Collider>());
            }
            foreach (var item in hits)
            {
                if (!neighborColorSpere.Contains(item.transform.GetComponent<ColorSpere>()))
                {
                    neighborColorSpere.Add(item.transform.GetComponent<ColorSpere>());
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
                FindNeighbor();

                collision.transform.GetComponent<ColorSpere>().FindNeighbor();
                nearSpere = new List<ColorSpere>();
                GetNeighborColor(collision.transform.GetComponent<ColorSpere>(), color, nearSpere, collision.transform.GetComponent<ColorSpere>().neighborColorSpere);
                print(nearSpere.Count);
                if (nearSpere.Count >= 3)
                {
                    foreach (ColorSpere c in nearSpere)
                    {
                        Destroy(c.gameObject);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }


    public void GetNeighborColor(ColorSpere colorSpere, SpereColor targetColor, List<ColorSpere> nearSpere, List<ColorSpere> neighborColorSpere)
    {
        
        if (colorSpere == null || nearSpere.Contains(colorSpere)|| colorSpere.color != targetColor)
        {
            return;
        }
        
        nearSpere.Add(colorSpere);
      //  print(colorSpere.name);
        foreach (var item in neighborColorSpere) { 
            GetNeighborColor(item, targetColor, nearSpere, item.neighborColorSpere);
            
        }
    }
}
