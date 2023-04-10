using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager instance;
    [SerializeField] GameObject UIPanel;
    [SerializeField] GameObject rodHealthTile;
    [SerializeField] GameObject fishHealthTile;

    [SerializeField] Image rodHealthFill;

    [SerializeField] Image fishHealthFill;


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

    //Out of 1
    public void UpdateHealth(float rodHealth, float fishHealth)
    {
        rodHealthFill.fillAmount = rodHealth;
        fishHealthFill.fillAmount = fishHealth;
    }
}
