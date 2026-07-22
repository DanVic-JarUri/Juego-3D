using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad = 10; // Velocidad normal
    public float velocidadAcelerada; // Velocidad durante la aceleración
    public bool acelerando = false; // Estado de aceleración
    private float duracionAceleracion = 8f; // Duración del efecto acelerador en segundos
    private float tiempoAceleracion; // Tiempo límite de aceleración

    private Renderer jugadorRenderer; // Cambiar color del jugador para indicar el estado

    public float fuerzaSalto = 5f; // Fuerza del salto
    private bool enElSuelo = true; // Indica si el jugador está tocando el suelo


    public static int contador { get; set; } = 0;

    public TMP_Text Foot;

    public Ganar ganarScript; // Referencia al script Ganar


    public GameObject puerta1, puerta2, puerta3, puerta4;

    public GameObject footF;
    public GameObject pedestal;

    public GameObject accel;

    float xInit, yInit, zInit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jugadorRenderer = GetComponent<Renderer>();

        xInit = 0.4871041f;
        zInit = 2.71f;
        yInit = 4.299567f;

        //MonedasObtenidas = 0;
    }


    private void FixedUpdate()
    {
        Mover();
        VerificarFinAceleracion();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && enElSuelo)
        {
            Saltar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            contador++;
            Foot.text = contador.ToString();

            if (contador >= 10) // Si se han recogido 10 cubos
            {
                ganarScript.AbrirMenu(); // Abre el panel
            }
        }

        if (other.gameObject.CompareTag("Acelerar"))
        {
            ActivarAceleracion();
        }
        if (contador == 3)
        {
            //GUI.Label(new Rect(5, 5, 200, 50), "¡Acelerador activado!");
            puerta1.SetActive(false);
            accel.SetActive(true);
        }
        if (contador == 6)
        {
            //GUI.Label(new Rect(5, 5, 200, 50), "¡Acelerador activado!");
            puerta2.SetActive(false);
            
        }
        if (contador == 9)
        {
            //GUI.Label(new Rect(5, 5, 200, 50), "¡Acelerador activado!");
            puerta3.SetActive(false);
            puerta4.SetActive(false);
            footF.SetActive(true);
            pedestal.SetActive(true);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rampa"))
        {
            enElSuelo = true;
            ActivarAceleracion();
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
            //Debug.Log("Jugador en el suelo.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo") && collision.gameObject.CompareTag("Rampa"))
        {
            enElSuelo = false;
            //Debug.Log("Jugador dejó el suelo o rampa.");
        }
    }


    void OnGUI()
    {
        if (acelerando)
        {
            // Cálculo de posición para centrar
            float labelX = 10; // Pequeño margen desde el borde izquierdo
            float labelY = Screen.height - 10 -20; // Pequeño margen desde el borde inferior

            GUI.Label(new Rect(labelX, labelY, 200, 20), "¡Acelerador activado!");
        }
        if (contador == 9)
        {
            float labelWidth = 200; // Ancho del label
            float labelHeight = 20; // Alto del label

            // Posición en la esquina inferior izquierda
            float labelX = 10; // Margen desde el borde izquierdo
            float labelY = Screen.height - labelHeight - 20; // Margen desde el borde inferior

            // Definir estilo personalizado
            GUIStyle estilo = new GUIStyle(GUI.skin.label);
            estilo.fontSize = 15; // Tamaño de fuente grande
            estilo.alignment = TextAnchor.MiddleLeft; // Alinear el texto hacia la izquierda
            estilo.normal.textColor = Color.green; // Cambiar color del texto a verde

            // Dibujar el label
            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "¡Vuelve al Inicio!", estilo);
        }



        if (contador == 10)
        {
            // Dimensiones del label
            float labelWidth = 400; // Ancho del label
            float labelHeight = 100; // Alto del label

            // Cálculo de posición para centrar
            float labelX = (Screen.width - labelWidth) / 2; // Centro en X
            float labelY = (Screen.height - labelHeight) / 2; // Centro en Y

            // Definir estilo personalizado
            GUIStyle estilo = new GUIStyle(GUI.skin.label);
            estilo.fontSize = 32; // Tamaño de fuente grande
            estilo.alignment = TextAnchor.MiddleCenter; // Centrar el texto dentro del rectángulo
            estilo.normal.textColor = Color.green; // Cambiar color del texto a verde

            // Dibujar el label
            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "¡Objetivo Completado!", estilo);
           
        }
    }

    private void Mover()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        // Calcula la fuerza según el estado de aceleración
        float velocidadActual = acelerando ? velocidadAcelerada : velocidad;
        Vector3 movimiento = new Vector3(movH, 0, movV);
       
        // Aplica fuerza al Rigidbody
        rb.AddForce(movimiento * velocidadActual, ForceMode.Force);

        // Debug opcional para monitorear velocidad
        //Debug.Log($"Velocidad actual: {rb.velocity.magnitude}");
    }

    public void Resetear()
    {
        //xInit = 0.4871041f;
        //zInit = 2.71f;
        //yInit = 4.299567f;
        transform.position = new Vector3(xInit, yInit, zInit);
    }

    private void VerificarFinAceleracion()
    {
        // Desactivar aceleración si ha pasado la duración
        if (acelerando && Time.time > tiempoAceleracion)
        {
            acelerando = false;
            jugadorRenderer.material.color = Color.white; // Cambia a color normal
            //Debug.Log("Aceleración terminada.");
        }
    }

    private void ActivarAceleracion()
    {
        acelerando = true;
        tiempoAceleracion = Time.time + duracionAceleracion; // Configura el tiempo límite
        jugadorRenderer.material.color = Color.red; // Cambia a color acelerado
        //Debug.Log("¡Acelerador activado!");
    }

    private void Saltar()
    {
        // Aplica una fuerza hacia arriba para simular el salto
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        enElSuelo = false; // Marca que el jugador ya no está en el suelo
        //Debug.Log("¡Salto realizado!");
    }

}
