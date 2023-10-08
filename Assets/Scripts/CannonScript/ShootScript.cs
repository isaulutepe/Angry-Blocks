using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private GameController gc;
    public float power = 2f; //Fýrlatma günü
    private int dots = 15; //Top sayýsý

    private Vector2 startPosition;

    private bool shoot, aiming;

    private GameObject Dots;
    private List<GameObject> projectilesPack;

    private Rigidbody2D ballBody;

    public GameObject ballPrefab;
    public GameObject ballContainer;

    private void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        Dots = GameObject.Find("Dots"); //An game obje içinde 15 adet dot nesnesi var.
    }
    private void Start()
    {
        projectilesPack = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        //Bu iþlemde, Dots adlý bir GameObject’in tüm alt nesnelerinin (children) bir listesini oluþturuyorsunuz. Bu listeyi projectilesPack adlý bir deðiþkene atýyorsunuz. Dost nesnesinin transform özelliðini transform nesnesine dönüþtüyüoruz sonra da bunu bir transorm listesi yapýyoruz ardýndan bu listedeki bütün elemanlarý tekrar bir gameonjeye çeviriyoruz.

        //Bu iþlem alt nesnelerin transformlarýný deðil, doðrudan GameObject’lerini kullanabilmemizi saðlar.

        HideDost(); //Toplarý gizle.
    }
    private void Update()
    {
        ballBody = ballPrefab.GetComponent<Rigidbody2D>();

        if (gc.shotCount <= 3)
        {
            Aim();
            Rotate();
        }
    }

    private void Aim()
    {
        if (shoot)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            if (!aiming)
            {
                aiming = true;
                startPosition = Input.mousePosition; //Mouse poziyonunu baþlangýç pozisyonu kabul et.
                gc.CheckShotCount();
            }
            else
            {
                PathCalculation();
            }
        }
        else if (aiming && !shoot)
        {
            aiming = false;
            HideDost();
            StartCoroutine(Shoot());
            if (gc.shotCount == 1)
                Camera.main.GetComponent<CameraTransition>().RotateCameraToSide(); //Kamerayý yana çevir.
        }
    }
    Vector2 ShootForce(Vector2 force)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(force.x, force.y)) * power;
    }
    Vector2 DotPath(Vector2 startP, Vector2 startVel, float t)
    //Baþlangýç posizyonu, baþlangýç hýzý,birde zaman parametreleri alýr.
    {
        return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
        //Bu kodlar, bir cismin 2 boyutlu bir yörünge üzerindeki konumunu hesaplamak için kullanýlýyor
    }
    void PathCalculation()
    {
        Vector2 vel = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / ballBody.mass;
        //Topun hareketini ayarlýyoruz. fixidDeltaTime topun sabit hýzla gitmesini saðlayacak. ballBody.mass deðeride topun ivmesini kontrol etmek için.

        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = true; //Toplarýn görünürlüðünü aktif ettik.

            float t = i / 15f;
            Vector3 point = DotPath(transform.position, vel, t); //Her bir nokta için konumunu hesaplar.
            point.z = 1; // z de hareket sabit olmalý.
            projectilesPack[i].transform.position = point; //Her noktanýn trasformlarýný deðiþtir.
        }
    }
    void ShowDots()
    {
        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = true;
        }
    }
    void HideDost()
    {
        for (int i = 0; i < projectilesPack.Count; i++)
        {
            projectilesPack[i].GetComponent<Renderer>().enabled = false;
        }
    }
    void Rotate() //Connon nesnesinin dönmesi için.
    {
        var direction = GameObject.Find("dot (1)").transform.position - transform.position;
        //Cannon nesnesi ile birinci top arasýndaki mesafeyi ölçtük. 
        var angle = Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Burada açý hesaplama iþlemi yapýlýr.
        //direction.y ve direction.x deðerleri bir noktadan baþka bir noktaya giden bir doðruyu temsil ediyo.
        //Math.Atan2 --> bu ön vectörünün x eksni ile yaptýðý açýyý Ranyan olarak hesaplar.
        //Mathf.Rad2Deg--> bu da randyan olan deðeri açý cinsine dönüþtürür.

        transform.rotation = Quaternion.AngleAxis((float)angle, Vector3.forward);
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.07f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.name = "Ball";
            ball.transform.SetParent(ballContainer.transform); //Oluþan toplarý bu nesne altýnda toplar.
            ballBody = ball.GetComponent<Rigidbody2D>();
            ballBody.AddForce(ShootForce(Input.mousePosition));
        }
        gc.shotCount++;
    }
}
