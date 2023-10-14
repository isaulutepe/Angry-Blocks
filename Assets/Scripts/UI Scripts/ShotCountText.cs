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
    //Yaz�lar�n parlamas� ya da yan�p s�nmesi i�in yazd���m bir kod par�as� �ok dagerekli de�il.
    public void Flash()
    {
        cg.alpha = 1.0f; //Tamamen g�r�n�r k�lar.
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine() //Yan�p S�nme efekti verir.
    {
        for (int i = 0; i <= 60; i++)
        {
            transform.localScale = Vector3.one * scaleCurve.Evaluate((float)i / 50);
            if (i >= 40)
            {
                cg.alpha = (float)(60 - i) / 20;
            }
            yield return null;

        }

        yield break;
    }
}
