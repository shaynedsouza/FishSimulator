using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager instance;
    [SerializeField] GameObject UIPanel;
    [SerializeField] GameObject rodHealthTile;
    [SerializeField] GameObject fishHealthTile;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }


    public void ToggleUIForFight(bool value)
    {
        UIPanel.SetActive(value);
    }


}
