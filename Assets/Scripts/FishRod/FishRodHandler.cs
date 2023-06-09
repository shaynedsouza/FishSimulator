using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(FishRodStringRenderer))]
public class FishRodHandler : MonoBehaviour
{
    bool canInteract = false, canTargetBaitListener = false;


    [SerializeField] Transform rodModel;

    [Header("Rod settings")]
    [SerializeField] float maxXRotationRod = 10f;
    [SerializeField] float maxYRotationRod = 40f;


    [Header("String settings")]
    [SerializeField] float maxLocalXOffsetString = 3f;
    [SerializeField] float maxLocalZOffsetString = 1f;


    // Range for rotation of the rod 
    Vector2 xRodRange, yRodRange;
    //Range for position of the string
    Vector2 xLocalStringRange, zLocalStringRange;


    Vector3 myRotation;
    InputHandler inputHandler;
    FishRodStringRenderer fishRodStringRenderer;
    Transform lastPoint;

    public static FishRodHandler instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }


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
        myRotation = transform.rotation.eulerAngles;
        inputHandler = GetComponent<InputHandler>();
        fishRodStringRenderer = GetComponent<FishRodStringRenderer>();

        //Getting the rotation range for the fish rod
        xRodRange.x = myRotation.x + maxXRotationRod;
        xRodRange.y = myRotation.x - maxXRotationRod;

        yRodRange.x = myRotation.y - maxYRotationRod;
        yRodRange.y = myRotation.y + maxYRotationRod;



        //Getting the position range for the string
        lastPoint = fishRodStringRenderer.lastPoint;
        xLocalStringRange.x = lastPoint.localPosition.x - maxLocalXOffsetString;
        xLocalStringRange.y = lastPoint.localPosition.x + maxLocalXOffsetString;

        zLocalStringRange.x = lastPoint.localPosition.z - maxLocalZOffsetString;
        zLocalStringRange.y = lastPoint.localPosition.z + maxLocalZOffsetString;


    }


    private void Update()
    {

        if (!canInteract) return;

        //Remapping the ranges and setting the rotation
        float xRot = Map(inputHandler.mousePosition.x, 0f, Screen.width, yRodRange.x, yRodRange.y);
        float yRot = Map(inputHandler.mousePosition.y, 0f, Screen.height, xRodRange.x, xRodRange.y);
        transform.rotation = Quaternion.Euler(yRot, xRot, transform.rotation.z);


        //Allow the user to move the rod but not the lastpoint, since it will be attached to the fish
        if (!canTargetBaitListener) return;

        //Remapping the ranges and setting the position
        float xLocalPos = Map(inputHandler.mousePosition.x, 0f, Screen.width, xLocalStringRange.x, xLocalStringRange.y);
        float zLocalPos = Map(inputHandler.mousePosition.y, 0f, Screen.height, zLocalStringRange.x, zLocalStringRange.y);
        lastPoint.localPosition = new Vector3(xLocalPos, lastPoint.localPosition.y, zLocalPos);
    }



    //Function to remap the ranges 
    float Map(float s, float a1, float a2, float b1, float b2)
    {
        // Debug.Log("s:" + s + " range1min:" + a1 + " range1max:" + a2 + " range2min:" + b1 + " range2max" + b2);
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }



    public void SetLastPointParent(GameObject fishGO)
    {
        lastPoint.parent = fishGO.transform;
        lastPoint.localPosition = Vector3.zero;
        rodModel.DOShakePosition(0.1f, 0.01f, 1, 10).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
    public void KillVibrations()
    {
        lastPoint.parent = transform.parent;
        DOTween.Kill(rodModel);
    }

    #region Listeners

    //Listens to GameManager for interaction detections
    void CanInteractListener(bool value)
    {
        canInteract = value;
    }

    void CanTargetBaitListener(bool value)
    {
        canTargetBaitListener = value;
    }

    #endregion
}
