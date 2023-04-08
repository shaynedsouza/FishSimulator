using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    bool canInteract = false;
    public Vector3 mousePosition { get; private set; }


    private void OnEnable()
    {
        GameplayManager.CanInteractNotifier += CanInteractListener;
    }

    private void OnDisable()
    {
        GameplayManager.CanInteractNotifier -= CanInteractListener;

    }

    private void Update()
    {
        if (!canInteract) return;

        Vector3 newMousePosition = Input.mousePosition;
        newMousePosition.x = Mathf.Clamp(newMousePosition.x, 0f, Screen.width);
        newMousePosition.y = Mathf.Clamp(newMousePosition.y, 0f, Screen.height);
        mousePosition = newMousePosition;


    }



    #region Listeners

    //Listens to GameManager for interaction detections
    void CanInteractListener(bool value)
    {
        canInteract = value;

    }

    #endregion
}
