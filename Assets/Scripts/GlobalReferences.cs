using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance;

    public GameObject BulletHolePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
