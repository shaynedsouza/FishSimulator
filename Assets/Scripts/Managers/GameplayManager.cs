using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public static Action<bool> CanInteractNotifier;
    public static Action<bool> CanTargetBaitNotifier;

    InputHandler inputHandler;
    bool isTargetingBait = false;
    public bool fightInProgress { get; private set; } = false;
    GameObject fishInFight;
    float fishHealth = 1f;
    float rodHealth = 1f;

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

        try
        {
            inputHandler = FindObjectOfType<InputHandler>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

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
    public void FightItOff(GameObject fishInFight)
    {
        fightInProgress = true;
        CanvasManager.instance.ToggleUIForFight(true);
        this.fishInFight = fishInFight;

        if (inputHandler && fishInFight)
            StartCoroutine(FightLogic());
    }


    IEnumerator FightLogic()
    {
        fishHealth = 1f;
        rodHealth = 1f;
        int counter = 1, score = 0;
        Vector2 mousePosition;
        CanvasManager.instance.UpdateHealth(rodHealth, fishHealth);

        while (fishHealth > 0.01f && rodHealth > 0.01f)
        {
            yield return new WaitForSeconds(0.2f);



            //Get mouse position in graph axis with unit magnitude 
            mousePosition.x = (inputHandler.mousePosition.x / Screen.width * 2f) - 1f;
            mousePosition.y = (inputHandler.mousePosition.y / Screen.height * 2f) - 1f;

            //Perform dot product to check if directions align
            if (Vector3.Dot(mousePosition, fishInFight.transform.forward) >= 0f)
                score++;
            else
                score--;



            //Perform action every 2secs (0.2f x 10)
            if (counter % 10 == 0)
            {
                if (score > 0)
                    fishHealth -= 0.2f;
                else
                {
                    rodHealth -= 0.2f;
                    CanvasManager.instance.TakeDamage();
                }

                CanvasManager.instance.UpdateHealth(rodHealth, fishHealth);
                score = 0;
            }
            counter++;

        }

        FishRodHandler.instance.KillVibrations();
        CanInteractNotifier?.Invoke(false);

        if (fishHealth > 0.01f)
        {
            fightInProgress = false;
            CanvasManager.instance.DisplayResult(false);
        }
        else
        {
            Destroy(fishInFight);
            CanvasManager.instance.DisplayResult(true);
        }
    }



    public void ReleaseTargetBait()
    {
        isTargetingBait = false;
        fightInProgress = false;
        CanTargetBaitNotifier?.Invoke(true);
        CanvasManager.instance.ToggleUIForFight(false);

    }




}
