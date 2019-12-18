using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject uiPresentacion;//Panel de presentacion
    public GameObject uiFin;//Panel finalizado
    public GameObject circulo;//Circulo del juego
    public GameObject uiTextContador;//Cuenta atras
    public GameObject panel;

    private enum GameState { Idle, Playing, End };//Enum de estados del juego
    private GameState estado = GameState.Idle;//Estado actual del juego
    private Text contador;//Texto de la cuenta atras
    private FuncionesBBDD bbdd;
    private DestruirEstimulo destruirEstimulo;
    private CirculoExterior circuloScript;
    private float timeCuentaAtras = 9;//Segundos cuenta atras
    private float timeFin = 3;//Tiempo de espera al terminar
    private float timeTotal = 0;//Tiempo actual de juego
    private string[] args;//guardo los argumentos del proceso

    // Start is called before the first frame update
    void Start()
    {
        //Desactivo el circulo de juego y la pantalla final
        //circulo.SetActive(false);
        uiFin.SetActive(false);

        //Almaceno el componente de texto del contador
        contador = uiTextContador.GetComponent<Text>();

        //Instancio la clase de la base de datos
        bbdd = new FuncionesBBDD();
        circuloScript = new CirculoExterior();
        destruirEstimulo = new DestruirEstimulo();

    }

    // Update is called once per frame
    void Update()
    {
        //Controlamos el timer de la cuenta atras
        timeCuentaAtras -= Time.deltaTime;
        bool finContador = false;

        //Si es mayor que 0 voy mostrando
        if (timeCuentaAtras >= 0)
        {
            contador.text = timeCuentaAtras.ToString("f0");
        }
        //Cuando es menor que 0 indico que termine
        else
        {
            finContador = true;
        }

        //Si esta en estado parado y el contador ha terminado comienza
        if (GameState.Idle == estado && finContador)
        {
            //Cambiamos el estado y ocultamos el ui de presentacion
            estado = GameState.Playing;
            panel.SetActive(false);
            

            //Activamos el circulo de juego
            //circulo.SetActive(true);

        }
        //Si el juego esta en proceso
        else if (estado == GameState.Playing)
        {
            //Sumamos segundos al tiempo de juego
            timeTotal += Time.deltaTime;

            //Si el usuario ha destruido todos los estimulos finaliza
            if(circulo.transform.childCount == 1)
            {

                bbdd.insertarResultados(crearJSONResultados());
                bbdd.insertarEstado();

                //Ocultamos el circulo de juego y mostramos la pantalla final
                circulo.SetActive(false);
                uiFin.SetActive(true);

                //Cambiamos el estado de juego
                estado = GameState.End;
            }
            
        }
        //Si el juego ha terminado
        else if (estado == GameState.End)
        {
            //Espero los segundos de la variable timeFin
            timeFin -= Time.deltaTime;

            //Elimino el tiempo de preparacion del juego
            timeTotal -= timeCuentaAtras;

            //Cuando termine la espera finalizo el juego
            if (timeFin < 0)
            {
                Application.Quit();
            }



        }
    }

    //Creo el json resultados y lo devuelvo
    public Resultados crearJSONResultados()
    {
        Resultados resultadosJSON = new Resultados()
        {
            numeroClicks = circuloScript.NumeroEstimulos,
            fallos = circuloScript.Fallos + destruirEstimulo.Fallos,
            tiempo = timeTotal.ToString("f2")

        };

        return resultadosJSON;
    }
}
