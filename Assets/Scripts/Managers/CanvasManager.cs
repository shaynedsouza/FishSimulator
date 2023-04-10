using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager instance;
    [SerializeField] GameObject UIPanel;
    [SerializeField] GameObject rodHealthTile;
    [SerializeField] GameObject fishHealthTile;

    [SerializeField] Image rodHealthFill;

    [SerializeField] Image fishHealthFill;
    [SerializeField] Image bloodFill;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

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

    public void TakeDamage()
    {
        DOTween.Kill(bloodFill);
        bloodFill.DOFade(1f, 0f).OnComplete(() =>
        {
            bloodFill.DOFade(0f, 3f).SetDelay(0.3f);
        });

    }


    public void DisplayResult(bool didWin)
    {
        if (didWin)
        {
            winPanel.transform.localScale = Vector3.zero;
            winPanel.SetActive(true);
            winPanel.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InSine);
        }
        else
        {
            losePanel.transform.localScale = Vector3.zero;
            losePanel.SetActive(true);
            losePanel.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InSine);
        }
    }


    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
