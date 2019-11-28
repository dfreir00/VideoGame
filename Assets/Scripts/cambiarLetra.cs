using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarLetra : MonoBehaviour
{

    public TextMesh letraCentral;
    public CirculoExterior circulo;

    // Start is called before the first frame update
    void Start()
    {
        letraCentral.GetComponent<TextMesh>().text = circulo.obtenerLetra();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
