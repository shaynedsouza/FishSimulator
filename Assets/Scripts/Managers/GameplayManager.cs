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
    public bool fightInProgress { get; private set; } = false;



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
    public bool CanTargetBait()
    {
        if (isTargetingBait) return false;

        isTargetingBait = true;
        CanTargetBaitNotifier?.Invoke(false);
        return true;
    }


    //When the fight for survial begins
    public void FightItOff()
    {
        fightInProgress = true;
        CanvasManager.instance.ToggleUIForFight(true);

    }


    public void ReleaseTargetBait()
    {
        isTargetingBait = false;
        fightInProgress = false;
        CanTargetBaitNotifier?.Invoke(true);
        CanvasManager.instance.ToggleUIForFight(false);

    }
}
