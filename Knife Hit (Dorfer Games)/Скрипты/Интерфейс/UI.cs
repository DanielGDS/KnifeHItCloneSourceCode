﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject[] knifeUI;
    public GameObject[] stageUI;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int health)
    {
        for (int x = 0; x < hearts.Length; ++x)
        {
            hearts[x].SetActive(x < health);
        }

    }

    public void UpdateKnifeUI(int health)
    {
        for (int x = 0; x < knifeUI.Length; ++x)
        {
            knifeUI[x].SetActive(x < health);
        }
    }

    public void UpdateStage(int stage)
    {
        for (int x = 0; x < stageUI.Length; ++x)
        {
            stageUI[x].SetActive(x < stage);
        }
    }
}
