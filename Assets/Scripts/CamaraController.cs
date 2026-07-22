using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //Diferencia entre la posición de la cámara y la del jugador
        offset = transform.position - player.transform.position;
    }
    //se ejecuta cada frame, pero después de haber procesado toda. Es mucho más exacto
    private void LateUpdate()
    {
        //actualizar la posición de la cámara
        transform.position = player.transform.position + offset;
    }
}
