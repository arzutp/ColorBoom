using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform SpereParent;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public Transform SpereParentTransform()
    {
        return SpereParent;
    }
}
