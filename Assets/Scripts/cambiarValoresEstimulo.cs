using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarValoresEstimulo : MonoBehaviour
{
    public int tamanyo;
    private Renderer colorEstimulo;
    // Start is called before the first frame update
    void Start()
    {
        cambiarTamanyo();
        cambiarColorEstimulo();
        etiquetarEstimulo();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //metodo que le pone un tag al estimulo
    public void etiquetarEstimulo()
    {
        gameObject.tag = "Estimulo";
    }

    //metodo que me cambia el tamanyo del estimulo generado
    public void cambiarTamanyo()
    {

        //este metodo me calcula el radio del estimulo en funcion del tamanyo elegido por el usuario en el launcher
        float radio = calcularTamanyoEscalado(tamanyo);

        float radio2 = radio * 10;
        //este metodo me cambia el tamanyo de los estimulos
        gameObject.transform.localScale = new Vector3(radio, 0.001f, radio);
        
        //ajusto el radio del collider de los estimulos
        gameObject.GetComponent<BoxCollider>().size = new Vector3(radio2, 10f, radio2); 
      
    }

    //metodo que me escala el tamanyo recibido del launcher 
    public float calcularTamanyoEscalado(int tamanyo)
    {
        //25 es el tamanyo minimo del estimulo,
        //si se introduce un valor menor que 25 se asignará
        //al estimulo el radio generado cuando se introduce 25
        //por el contrario si se introduce un tamanyo mayor a 100
        //se le asignará al estimulo el radio generado con el tamanyo=100
        if(tamanyo <= 25)
        {
            tamanyo = 25;
        }else if(tamanyo >= 100)
        {
            tamanyo = 100;
        }
        //ajustamos el tamaño maximo de los estimulos
        float radio = (float)tamanyo/ 1500;


        return radio;
    }

    //metodo que me cambia el color de los estimulos
    public void cambiarColorEstimulo()
    {
 
        colorEstimulo = GetComponent<Renderer>();
        colorEstimulo.material.SetColor("_Color", new Color(0f, 0f, 1f));

    }
}
