using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FishController : MonoBehaviour
{

    float forceMultiplier;
    [SerializeField] float swimSpeedMultiplier = 0.3f;
    [SerializeField] float struggleSpeedMultiplier = 1f;
    [SerializeField] float minFrequencyDirChange = 2f;
    [SerializeField] float maxFrequencyDirChange = 5f;
    [SerializeField] bool canInteract = false, canTargetBait = false;
    [SerializeField] bool isTargetingBait = false, isFighting = false;
    Rigidbody rigidBody;
    FishAnimator fishAnimator;
    float timeToTargetCompletion = 1f;
    float elapsedTimeToTargetCompletion;

    private void OnEnable()
    {
        GameplayManager.CanInteractNotifier += CanInteractListener;
        GameplayManager.CanTargetBaitNotifier += CanTargetBaitListener;
    }


    private void OnDisable()
    {
        GameplayManager.CanInteractNotifier -= CanInteractListener;
        GameplayManager.CanTargetBaitNotifier -= CanTargetBaitListener;

    }


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        fishAnimator = GetComponent<FishAnimator>();
        forceMultiplier = swimSpeedMultiplier;
    }


    private void Update()
    {
        if (isTargetingBait)
        {
            elapsedTimeToTargetCompletion -= Time.deltaTime;

            if (elapsedTimeToTargetCompletion <= 0f)
            {
                isTargetingBait = false;
                StartCoroutine(FightItOff());
            }
        }
    }

    //Rod vs fish fighting for a win
    IEnumerator FightItOff()
    {
        isFighting = true;
        GameplayManager.instance.FightItOff(gameObject);
        FishRodHandler.instance.SetLastPointParent(gameObject);
        forceMultiplier = struggleSpeedMultiplier;
        fishAnimator.FightItOff();

        while (GameplayManager.instance.fightInProgress)
        {
            rigidBody.transform.Rotate(0, Random.Range(0f, 360f), 0);
            rigidBody.velocity = transform.forward * forceMultiplier;
            yield return new WaitForSeconds(Random.Range(minFrequencyDirChange, maxFrequencyDirChange));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isTargetingBait) return;
        float newAngle = 0f;

        if (other.tag == "Wall")
        {
            //Turn in the opposite direction
            newAngle = transform.rotation.y + 180f;
            rigidBody.transform.Rotate(0, newAngle, 0);
            rigidBody.velocity = transform.forward * forceMultiplier;
        }

        else if (other.tag == "Bait")
        {
            // if (!canTargetBait) return;
            if (GameplayManager.instance.CanTargetBait())
            {
                elapsedTimeToTargetCompletion = timeToTargetCompletion;
                isTargetingBait = true;
            }
        }

        else if (other.tag == "Fish")
        {
            newAngle = Random.Range(0f, 360f);
            rigidBody.transform.Rotate(0, newAngle, 0);
            rigidBody.velocity = transform.forward * forceMultiplier;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bait" && isTargetingBait)
        {
            //Release isTargetingBait and gameplaymanager
            isTargetingBait = false;
            GameplayManager.instance.ReleaseTargetBait();
            forceMultiplier = swimSpeedMultiplier;
        }
    }



    #region Listeners

    //Listens to GameManager for interaction detections
    void CanInteractListener(bool value)
    {
        canInteract = value;

        if (canInteract)
        {
            rigidBody.transform.Rotate(0, Random.Range(0f, 360f), 0);
            rigidBody.velocity = transform.forward * forceMultiplier;
        }
    }



    void CanTargetBaitListener(bool value)
    {
        canTargetBait = value;
    }

    #endregion
}
