using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirculoExterior : MonoBehaviour
{
    private Datos config;
    private FuncionesBBDD bbdd;
    private int fallos = 0;

    public GameObject prefab;
    public GameObject letraUsuario;
    public int velocidad;
    public int numeroEstimulos;
    public int tamanyoLetra;
    public int tamanyoEstimulos;
    public string letra;
    public Color colorCirculo;
    public Color colorEstimulos;
    public Color colorNumeros;


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

    public void asignarColorEstimulos()
    {
        Renderer renderEstimulo = prefab.GetComponent<Renderer>();
        string[] vec = config.colorEstimulos.Split('.');
        colorEstimulos.r = float.Parse(vec[0]) / 255f;
        colorEstimulos.g = float.Parse(vec[1]) / 255f;
        colorEstimulos.b = float.Parse(vec[2]) / 255f;

        renderEstimulo.material.SetColor("_Color", new Color(colorEstimulos.r, colorEstimulos.g, colorEstimulos.b, 1));


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

    // Start is called before the first frame update
    void Start()
    {
        bbdd = new FuncionesBBDD();

        //Leemos la configuracion y la guardamos en json config
        config = bbdd.leerConfiguracion();

        //Asignamos los valores de la config a las variables publicas
        velocidad = config.velocidad;
        numeroEstimulos = config.numeroEstimulos;
        tamanyoLetra = config.tamanyoLetra;
        tamanyoEstimulos = config.tamanyoEstimulos;
        letra = config.letra;


        asignamosLetra();

        //Asignamos los colores elegidos por el usuario
        asignarColorCirculo();
        asignarColorEstimulos();

        //Guardamos en las variables publicas el resto de colores
        obtenemosColorNumeros();

        //Asignamos las variables del estimulo
        generarEstimulo();

    }

    // Update is called once per frame
    void Update()
    {
        rotar();
    }

    public void rotar()
    {
        gameObject.transform.Rotate(0, 0, (-1) * velocidad * Time.deltaTime);
    }

    //este metodo me genera tantos estimulos como se introduzcan
    public void generarEstimulo()
    {
        for (int i = 0; i < numeroEstimulos; i++)
        {
            //genero posiciones aleatorias
            //establecen la situacion dentro del circuloExterior
            //establecen la situacion dentro del circuloExterior
            float posicionX = Random.Range(-110f, 110f);
            float posicionZ = Random.Range(-110f, 110f);
            //posicionY establece la altura a la que se genera el estimulo
            float posicionY = 6f;

            //estos estimulos son generados como hijos del circuloInterior
            //se les asigna una posicion aleatoria
            GameObject hijo = Instantiate(prefab) as GameObject;
            hijo.transform.parent = gameObject.transform;
            hijo.transform.position = new Vector3(posicionX, posicionY, posicionZ);

        }

    }

    //metodo para saber el numero de fallos
    void OnMouseDown()
    {

        fallos++;
    }
}
