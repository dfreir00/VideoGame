﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CirculoExterior : MonoBehaviour
{
    private Datos config;
    private FuncionesBBDD bbdd;
    private static int fallos = 0;
    private bool quedaEstimulo = false;

    public GameObject prefab;
    public GameObject letraUsuario;
    public int velocidad;
    private static int numeroEstimulos;
    public int tamanyoLetra;
    private int tamanyoEstimulos;
    public string letra;
    public Color colorCirculo;
    public Color colorEstimulos;
    public Color colorNumeros;

    public int Fallos { get => fallos; set => fallos = value; }
    public int NumeroEstimulos { get => numeroEstimulos; set => numeroEstimulos = value; }

    //Lee el string del color lo divide por . y asigna al circulo el color
    public void asignarColorCirculo()
    {

        GameObject circulo = GameObject.Find("CirculoExterior");
        SpriteRenderer spriteRenderCirculo = circulo.GetComponent<SpriteRenderer>();
        string[] vec = config.colorCirculoExterior.Split('.');
        colorCirculo.r = float.Parse(vec[0]) / 255f;
        colorCirculo.g = float.Parse(vec[1]) / 255f;
        colorCirculo.b = float.Parse(vec[2]) / 255f;
        spriteRenderCirculo.color = new Color(colorCirculo.r, colorCirculo.g, colorCirculo.b);

    }
    public void asignarColorYTamEstimulos()
    {
        Renderer renderEstimulo = prefab.GetComponent<Renderer>();
        string[] vec = config.colorEstimulos.Split('.');
        colorEstimulos.r = float.Parse(vec[0]) / 255f;
        colorEstimulos.g = float.Parse(vec[1]) / 255f;
        colorEstimulos.b = float.Parse(vec[2]) / 255f;

        renderEstimulo.material.SetColor("_Color", new Color(colorEstimulos.r, colorEstimulos.g, colorEstimulos.b, 1));

        //este metodo me calcula el radio del estimulo en funcion del tamanyo elegido por el usuario en el launcher
        float radio = calcularTamanyoEscalado(config.tamanyoEstimulos);

        //este metodo me cambia el tamanyo de los estimulos
        prefab.transform.localScale = new Vector3(radio, 0.001f, radio);

        //ajusto el radio del collider de los estimulos
        prefab.GetComponent<BoxCollider>().size = new Vector3(1, 10f, 1);

    }

    public float calcularTamanyoEscalado(int tamanyo)
    {
        //25 es el tamanyo minimo del estimulo,
        //si se introduce un valor menor que 25 se asignará
        //al estimulo el radio generado cuando se introduce 25
        //por el contrario si se introduce un tamanyo mayor a 100
        //se le asignará al estimulo el radio generado con el tamanyo=100
        if (tamanyo <= 10)
        {
            tamanyo = 10;
        }
        else if (tamanyo > 10 && tamanyo <= 20 )
        {
            tamanyo = 12;
        }
        else if (tamanyo > 20 && tamanyo <= 30)
        {
            tamanyo = 14;
        }
        else if (tamanyo > 30 && tamanyo <= 40)
        {
            tamanyo = 16;
        }
        else if (tamanyo > 40 && tamanyo <= 50)
        {
            tamanyo = 17;
        }
        else if (tamanyo > 50 && tamanyo <= 60)
        {
            tamanyo = 19;
        }
        else if (tamanyo > 60 && tamanyo <= 70)
        {
            tamanyo = 20;
        }
        else if (tamanyo > 70 && tamanyo <= 80)
        {
            tamanyo = 22;
        }
        else if (tamanyo > 80 && tamanyo <= 90)
        {
            tamanyo = 23;

        }
        else
        {
            tamanyo = 25;
        }

        //ajustamos el tamaño maximo de los estimulos
        // float radio = (float)tamanyo / 1500;


        return tamanyo;
    }

    public void obtenemosColorNumeros()
    {

        string[] vec = config.colorLetra.Split('.');
        colorNumeros.r = float.Parse(vec[0]);
        colorNumeros.g = float.Parse(vec[1]);
        colorNumeros.b = float.Parse(vec[2]);

        colorEstimulos = new Color(colorNumeros.r, colorNumeros.g, colorNumeros.b);


    }
    public void asignamosLetra()
    {
        TextMesh letraMesh = letraUsuario.GetComponent<TextMesh>();
        letraMesh.text = letra;
    }
    //genera tantos estimulos como se introduzcan
    public void generarEstimulo()
    {
        for (int i = 0; i < NumeroEstimulos; i++)
        {
            //genero posiciones aleatorias
            //establecen la situacion dentro del circuloExterior
            //establecen la situacion dentro del circuloExterior
            float posicionX = Random.Range(-110f, 110f);
            float posicionZ = Random.Range(-110f, 110f);

            while (((posicionX < 40) && (-40 < posicionX)) && ((posicionZ < 40) && (-40 < posicionZ)))
            {
                posicionX = Random.Range(-110f, 110f);
                posicionZ = Random.Range(-110f, 110f);
            }

            //posicionY establece la altura a la que se genera el estimulo
            float posicionY = 6f;

            //estos estimulos son generados como hijos del circuloInterior
            //se les asigna una posicion aleatoria
            GameObject hijo = Instantiate(prefab) as GameObject;
            hijo.transform.parent = gameObject.transform;
            hijo.transform.position = new Vector3(posicionX, posicionY, posicionZ);
            

        }

    }
    //gira el circuloa a la velocidad indicada
    public void rotar()
    {
        gameObject.transform.Rotate(0, 0, (-1) * velocidad * Time.deltaTime);
    }
    //saber el numero de fallos
    void OnMouseDown()
    {
        Fallos++;

    }


    // Start is called before the first frame update
    void Start()
    {
        bbdd = new FuncionesBBDD();

        //Leemos la configuracion y la guardamos en json config
        config = bbdd.leerConfiguracion();

        //Asignamos los valores de la config a las variables publicas
        velocidad = config.velocidad;
        NumeroEstimulos = config.numeroEstimulos;
        tamanyoLetra = config.tamanyoLetra;
        tamanyoEstimulos = config.tamanyoEstimulos;
        letra = config.letra;

        asignamosLetra();

        //Asignamos los colores elegidos por el usuario
        asignarColorCirculo();

        //Guardamos en las variables publicas el resto de colores
        obtenemosColorNumeros();

        //Asignamos las variables del estimulo
        asignarColorYTamEstimulos();
        generarEstimulo();

    }

    // Update is called once per frame
    void Update()
    {
        rotar();
    }


}
