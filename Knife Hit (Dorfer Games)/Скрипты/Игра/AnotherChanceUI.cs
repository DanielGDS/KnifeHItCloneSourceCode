using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotherChanceUI : MonoBehaviour
{
    [Header("Images template")]
    public Text appleTemplate;
    public Image timerFillTemplate;
    public Image Knife;
    [Header("Timer Settings")]
    public Text timerText;
    public int timer;
    public float delayTimer = 1f;
    [Header("Template int")]
    public int tempApplePrice;

    public GameConroller _gameController;
    [Header("Apple Button")]
    public Button appleButton;

    private void Awake()
    {
        _gameController = GameObject
       .FindGameObjectWithTag("GameController")
       .GetComponent<GameConroller>();
    }

    private void Start()
    {
        appleTemplate.text = tempApplePrice.ToString();
        StartCoroutine(timerCount(timer));
    }

    public void AnotherChance(int applePrice, int timer)
    {

        if (_gameController.profile.PlayerKnife != null) { Knife.sprite = _gameController.profile.PlayerKnife; }
        timerFillTemplate.fillAmount = 1f;
        if (_gameController.appleCount < applePrice)
        {
            appleButton.interactable = false;
        }
        else if (_gameController.appleCount == applePrice) { appleButton.interactable = true; } 
        else { appleButton.interactable = true; }

        

        _gameController.m_counterValue = _gameController.appleCount;

        tempApplePrice = applePrice;
        appleTemplate.text = applePrice.ToString();
        StartCoroutine(timerCount(timer));
    }

    public void OnButtonClick()
    {
        if (_gameController.appleCount < tempApplePrice) { return; }
        _gameController.gameIsOver = false;
        _gameController.knifeCount++;
        _gameController.appleCount = _gameController.appleCount - tempApplePrice;
        //_gameController.appleCountText.text = _gameController.appleCount.ToString();
        PlayerPrefs.SetInt("AppleMax", _gameController.appleCount);

        _gameController.increaseScore();

        _gameController.GetSaveInfo();
        _gameController.AnotherChance();
        gameObject.SetActive(false);
    }

    IEnumerator timerCount(int timer)
    {
        timerText.text = timer.ToString();

        yield return new WaitForSeconds(delayTimer);
        int tempTime = timer;
        for (int i = 0; timer > i; i++)
        {
            tempTime--;

            float fill = (float)tempTime / timer;
            timerFillTemplate.fillAmount = fill;
            timerText.text = tempTime.ToString();
            yield return new WaitForSeconds(delayTimer);
        }

        _gameController.chanceToAnotherChance = 0;
        _gameController.GameOverScreen();
        gameObject.SetActive(false);
    }
}
