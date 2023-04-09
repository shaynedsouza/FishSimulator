using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FishController : MonoBehaviour
{

    float forceMultiplier = 0.3f;
    bool canInteract = false;

    Rigidbody rigidBody;



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
        rigidBody = GetComponent<Rigidbody>();

    }



    private void OnTriggerEnter(Collider other)
    {
        float newAngle = 0f;

        if (other.tag == "Wall")
        {
            //Turn in the opposite direction
            newAngle = transform.rotation.y + 180f;
        }
        else if (other.tag == "Fish")
        {
            newAngle = Random.Range(0f, 360f);
        }

        rigidBody.transform.Rotate(0, newAngle, 0);
        rigidBody.velocity = transform.forward * forceMultiplier;

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

    #endregion
}
