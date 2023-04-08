using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class FishAnimator : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidBody;
    float cooldownPeriod = 1f;
    float cooldownCounter;
    float forceMultiplier = 0.5f;
    bool canInteract = false;



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
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        cooldownCounter -= Time.deltaTime;

    }


    private void OnTriggerEnter(Collider other)
    {
        float newAngle = 0f;

        if (other.tag == "Wall")
        {
            //Turn in the opposite direction
            newAngle = transform.rotation.y + 180f;
        }
        else if (other.tag == "Fish")// && cooldownCounter <= 0f)
        {
            newAngle = Random.Range(0f, 360f);
        }

        cooldownCounter = cooldownPeriod;
        rigidBody.transform.Rotate(0, newAngle, 0);
        rigidBody.velocity = transform.forward * forceMultiplier;
        // rigidBody.AddForce(transform.forward * forceMultiplier, ForceMode.Acceleration);

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
            // rigidBody.AddForce(transform.forward * forceMultiplier, ForceMode.Acceleration);
        }
    }

    #endregion

}
