using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girarCirculo : MonoBehaviour
{
    public int velocidad;
    // Start is called before the first frame update
    void Start()
    {
       
    }
   

    // Update is called once per frame
    void Update()
    {
        rotar();
    }

    //metodo que hace girar el circulo 
    //interior y en consecuencia sus hijos
    public void rotar()
    {
        gameObject.transform.Rotate(0,0,(-1) * velocidad * Time.deltaTime);
    }
}
