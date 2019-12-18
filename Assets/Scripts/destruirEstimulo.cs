using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DestruirEstimulo : MonoBehaviour
{
    private static int cont = 1;
    private CirculoExterior circulo;
    private static int fallos = 0;

    string directory = Directory.GetCurrentDirectory().ToString();
    string logName = @"\gameLog.txt";
    string path;

    public int Fallos { get => fallos; set => fallos = value; }

    public void WriteToLog(string input)
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(input);
            }

        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
        }
    }

    void Start() {
        circulo = new CirculoExterior();
        path = directory + logName;
    }

    //este metodo se utiliza para destruir los estimulos cuando vas clicando sobre ellos
    public void OnMouseDown()
    {
        WriteToLog("jeje");
        WriteToLog(circulo.Letra);

        if (circulo.Letra.Equals("A"))
        {
            Destroy(gameObject);

        }
        else
        {
            string nombre = "Estimulo " + cont;
            if (gameObject.name.Equals(nombre))
            {
                Destroy(gameObject);
                cont++;
            }
            else
            {
                Fallos++;
            }
        }
       



    }

    //Si dos estimulos colisionan recalcula la posicion 
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Estimulo"))
        {
            int posx = Random.Range(-110, 110);
            int posz = Random.Range(-110, 110);

            //Para que no se generen dentro del circulo interior
            while (((posx < 40) && (-40 < posx)) && ((posz < 40) && (-40 < posz)))
            {
                posx = Random.Range(-110, 110);
                posz = Random.Range(-110, 110);
            }

            //Asigno la nueva posicion
            collision.gameObject.transform.position = new Vector3(posx, collision.gameObject.transform.position.y, posz);

        }

    }
}
