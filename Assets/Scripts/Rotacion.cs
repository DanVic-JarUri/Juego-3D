using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    void Update()
    {
        //Rotamos el elemento en un acantidad diferente en cada dirección y en 
        //cada intervalo de tiempo
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
