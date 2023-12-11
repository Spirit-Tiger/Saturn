using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool CanMoveCamera = true;
    public bool CanShoot = true;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
