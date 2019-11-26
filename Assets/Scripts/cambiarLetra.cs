using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarLetra : MonoBehaviour
{

    public TextMesh letraCentral;
    // Start is called before the first frame update
    void Start()
    {
        letraCentral.GetComponent<TextMesh>().text = "B";
    }

    // Update is called once per frame
    void Update()
    {
        //Comentario de prueba commit
    }
}
