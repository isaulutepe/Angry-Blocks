using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int count;

    private void Update()
    {
        if (transform.position.y <= -10) //Bloklar a�a�� d��erse silinecek.
        {
            Destroy(gameObject);
        }
    }
    public void SetStartingCount(int count)
    {
        this.count = count;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name=="Ball" && count >0) //Top temas ediyorsa.
        {
            count--;
            if (count == 0) 
                Destroy(gameObject); //Temas tamemen bitince yani �arpma s�f�ra inince blok nesnesi silinecek.
        }
    }
}
