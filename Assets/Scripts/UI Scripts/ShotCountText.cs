using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCountText : MonoBehaviour
{
    public AnimationCurve scaleCurve;

    private CanvasGroup cg;

    public Text topText, bottomText;

    private GameController gc;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        topText = transform.Find("TopText").GetComponent<Text>();
        bottomText = transform.Find("BottomText").GetComponent<Text>();
        
    }
    public void SetTopText(string text)
    {
        topText.text = text;
    }
    public void SetBottomText(string text)
    {
        bottomText.text = text;
    }
    //Yazýlarýn parlamasý ya da yanýp sönmesi için yazdýðým bir kod parçasý çok dagerekli deðil.
    public void Flash()
    {
        cg.alpha = 1.0f; //Tamamen görünür kýlar.
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine() //Yanýp Sönme efekti verir.
    {
        for (int i = 0; i < 60; i++)
        {
            if (i >= 40) //40 dan sonra alpha deðerini azaltmaya baþlar.
            {
                cg.alpha = (float)(60 - i) / 20;
                yield return null; //Bir falme bekle
            }
        }
        yield break;
    }
}
