using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(FishRodStringRenderer))]
public class FishRodHandler : MonoBehaviour
{
    bool canInteract = false;
    [SerializeField] float maxXRotation = 10f;
    [SerializeField] float maxYRotation = 40f;

    // Range for rotation of the rod
    Vector2 xRange, yRange;


    Vector3 myRotation;
    InputHandler inputHandler;
    FishRodStringRenderer fishRodStringRenderer;


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
        myRotation = transform.rotation.eulerAngles;
        inputHandler = GetComponent<InputHandler>();
        fishRodStringRenderer = GetComponent<FishRodStringRenderer>();

        //Getting the rotation range for the fish rod
        xRange.x = myRotation.x + maxXRotation;
        xRange.y = myRotation.x - maxXRotation;

        yRange.x = myRotation.y - maxYRotation;
        yRange.y = myRotation.y + maxYRotation;

    }


    private void Update()
    {
        if (!canInteract) return;

        //Remapping the ranges and setting the rotation
        float xRot = Map(inputHandler.mousePosition.y, 0f, Screen.height, xRange.x, xRange.y);
        float yRot = Map(inputHandler.mousePosition.x, 0f, Screen.width, yRange.x, yRange.y);
        transform.rotation = Quaternion.Euler(xRot, yRot, transform.rotation.z);
    }



    //Function to remap the ranges 
    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }




    #region Listeners

    //Listens to GameManager for interaction detections
    void CanInteractListener(bool value)
    {
        canInteract = value;

    }

    #endregion
}
