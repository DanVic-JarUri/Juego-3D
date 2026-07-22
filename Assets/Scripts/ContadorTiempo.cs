using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorTiempo : MonoBehaviour
{
    public TMP_Text Contador;

    public static float Tiempo { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        Tiempo = 0;
    }
    private void AumentarTiempo()
    {
        Tiempo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Tiempo += Time.deltaTime;

        //tiempo en segundos, aumenta de uno en uno
        //Contador.text = Tiempo.ToString("f0");
        //tiempo 00:00

        Contador.text = string.Format("{0:00}:{1:00}", (int)Tiempo / 60, (int)Tiempo % 60);
    }
}
