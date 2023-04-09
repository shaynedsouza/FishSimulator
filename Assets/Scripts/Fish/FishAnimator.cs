using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]


public class FishAnimator : MonoBehaviour
{
    Animator animator;

    bool canInteract = false;

    string swimNormal = "Fish_Armature_Swimming_Normal";
    string swimImpusle = "Fish_Armature_Swimming_Impulse";
    string swimFast = "Fish_Armature_Swimming_Fast";
    string swimOutOfWater = "Fish_Armature_Out_Of_Water";
    string swimAttack = "Fish_Armature_Attack";
    string swimDeath = "Fish_Armature_Death";



    private void OnEnable()
    {
        GameplayManager.CanInteractNotifier += CanInteractListener;
    }

    private void OnDisable()
    {
        GameplayManager.CanInteractNotifier -= CanInteractListener;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    #region Listeners

    //Listens to GameManager for interaction detections
    void CanInteractListener(bool value)
    {
        canInteract = value;
    }

    #endregion

}
