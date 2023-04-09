using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public static Action<bool> CanInteractNotifier;
    public static Action<bool> CanTargetBaitNotifier;
    bool isTargetingBait = false;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        CanInteractNotifier?.Invoke(true);
        CanTargetBaitNotifier?.Invoke(true);
    }


    //Returns true if no fish is targetting the bait else false
    public bool TargetBait()
    {
        if (isTargetingBait) return false;

        CanTargetBaitNotifier?.Invoke(false);
        return true;
    }


    public void ReleaseTargetBait()
    {
        CanTargetBaitNotifier?.Invoke(true);
    }
}
