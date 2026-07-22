using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujarObjeto : MonoBehaviour
{
    public float fuerza = 10; // Fuerza de empuje
    private Rigidbody rb;

    private void Start()
    {
        // Obtiene el Rigidbody al iniciar
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("El objeto no tiene un Rigidbody asignado.");
    }

    private void OnCollisionStay(Collision collision)
    {
        // Comprueba si colisiona con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada.");


            if (rb != null)
            {
                // Obtiene la dirección de movimiento del jugador
                float movH = Input.GetAxis("Horizontal");
                float movV = Input.GetAxis("Vertical");
                Vector3 direccionEmpuje = new Vector3(movH, 0, movV).normalized;

                // Aplica fuerza solo si hay movimiento
                if (direccionEmpuje != Vector3.zero)
                {
                    rb.AddForce(direccionEmpuje * fuerza, ForceMode.Impulse);
                    Debug.Log("¡Fuerza aplicada en la dirección del movimiento del jugador!");
                }
            }
        }
    }
}
