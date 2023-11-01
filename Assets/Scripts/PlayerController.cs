using ENUMS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ColorSpere Player;
    ColorSpere player;
    bool isFire; 

    private void Start()
    {
        RandomSpere();
    }
    void RandomSpere()
    {
        player = Instantiate(Player);
        player.transform.SetParent(this.gameObject.transform,false);
        isFire = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFire)
        {
            player.spereIsFire = true;
            isFire = true;
            player.transform.SetParent(null);
            StartCoroutine(InstantiateNewColorSpere());
        }
    }

    IEnumerator InstantiateNewColorSpere()
    {
        yield return new WaitForSeconds(0.5f);
        RandomSpere();
    }

}
