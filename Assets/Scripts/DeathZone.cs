using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //float xInit, yInit,zInit;

    public Transform Jugador;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Jugador.position.x, -11.49f, Jugador.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Movimiento>().SendMessage("Resetear");
    }

    //public void Resetear()
    //{
    //    xInit = 0.4871041f;
    //    zInit = 2.71f;
    //    yInit = 4.299567f;
    //    Jugador.transform.position = new Vector3(xInit, yInit ,zInit);
    //}
}
