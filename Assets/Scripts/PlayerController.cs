using ENUMS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ColorSpere Player;
    int enumCount;

    private void Start()
    {
        enumCount = Enum.GetValues(typeof(SpereColor)).Length;
        RandomSpere();
    }
    void RandomSpere()
    {
        
        int rand = UnityEngine.Random.Range(0, enumCount);
        Player.ColorChange((SpereColor)rand);
    }

}
