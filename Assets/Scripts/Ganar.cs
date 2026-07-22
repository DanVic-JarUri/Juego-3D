using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganar : MonoBehaviour
{
    public GameObject PanelGanar;

    public TMP_Text TextoFoot;
    public TMP_Text TextoTiempo;

    private void actualizarPanel()
    {
        float tiempo = ContadorTiempo.Tiempo;
        TextoTiempo.text = string.Format("{0:00}:{1:00}", (int)tiempo / 60, (int)tiempo % 60);

        int foot = Movimiento.contador;
        TextoFoot.text = foot.ToString();
    }
    public void AbrirMenu()
    {
        actualizarPanel();
        Time.timeScale = 0;
        PanelGanar.SetActive(true);
    }
    
}
