using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
    public Light linterna;
    private bool linternaEncendida = false;

    void Start()
    {
        linterna.enabled = false; // Inicialmente, la linterna est� apagada
    }

    void Update()
    {
        // Obtener una referencia al control del Oculus (aseg�rate de haber importado el SDK de Oculus)
        OVRInput.Controller controlOculus = OVRInput.GetActiveController();

        // Verificar si se presiona el bot�n A del control Oculus
        if (OVRInput.GetDown(OVRInput.Button.One, controlOculus))
        {
            // Alternar el estado de la linterna
            linternaEncendida = !linternaEncendida;

            // Encender o apagar la linterna seg�n el estado actual
            linterna.enabled = linternaEncendida;
        }
    }
}
