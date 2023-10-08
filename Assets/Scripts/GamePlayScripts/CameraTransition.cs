using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    private Transform cameraContainer;

    private float rotateSemiAmount = 4; //DDairel harektte yar��ap i�in
    private float shakeAmout; //Kameran�n titreme hareketi i�in.

    private Vector3 startingLocalPos;

    private void Start()
    {
        cameraContainer = GameObject.Find("CameraContainer").transform;
    }
    private void Update()
    {
        //Titreme methotlar� shakeAmount de�erini artt�racak ve bu kodlar �al��acak ard�ndan tekrar shakeAmount de�eri d���r�ld���nden titreme duracak.
        if (shakeAmout > 0.01f) //Titreme hareketi varsa konumda olu�acak de�i�iklik.
        {
            Vector3 localPosition = startingLocalPos;
            localPosition.x += shakeAmout * Random.Range(3, 5);
            localPosition.y += shakeAmout * Random.Range(3, 5);
            localPosition.z = -10f;
            transform.position = localPosition;
            shakeAmout = 0.9f * shakeAmout;//Titreme miktar�n� her karede biraz azalt�r.Titreme efektinin zamanla azalmas�n� sa�lamak i�in.
        }
    }
    public void Shake() //titreme miktar�n� art�r�r, ancak belirli bir �st s�n�r olan 0.1f'yi ge�mez.
    {
        shakeAmout = Mathf.Min(0.1f, shakeAmout + 0.01f);
    }
    public void MediumShake() //titreme miktar�n� art�r�r, ancak bu sefer �st s�n�r 0.15f olarak belirlenmi�tir. 
    {
        shakeAmout = Mathf.Min(0.15f, shakeAmout + 0.015f);
    }

    public void RotateCameraToSide()
    {
        StartCoroutine(RotateCameraToSideRoutine());
    }
    public void RotateCameraToFront()
    {
        StartCoroutine(RotateCameraToFrontRoutine());
    }
    IEnumerator RotateCameraToSideRoutine()
    {
        int frames = 20; //Kameran�n belirli bir a��ya d�nmesi i�in gerekli zaman.
        float increment = rotateSemiAmount / (float)frames; //A��sal d�nme miktar�n�n e�it aral�klarla ger�ekle�mesini sa�lak i�in.
        for (int i = 0; i < frames; i++)//Kamera D�nd�rme i�lemi ger�ekle�tirilir.
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, increment);
            yield return null;
        }
        yield break;
    }
    IEnumerator RotateCameraToFrontRoutine()
    {
        int frames = 60;
        float increment = rotateSemiAmount / (float)frames;
        for (int i = 0; i < frames; i++)
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, -increment);
            yield return null;
        }
        cameraContainer.localEulerAngles = new Vector3(0, 0, 0);
        yield break;
    }

}
