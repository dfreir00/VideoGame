using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirEstimulo : MonoBehaviour
{
    private static int cont = 1;

    //este metodo se utiliza para destruir los estimulos cuando vas clicando sobre ellos
    public void OnMouseDown()
    {
        string nombre = "Estimulo "+cont;
        if (gameObject.name.Equals(nombre)){
            Destroy(gameObject);
            cont++;
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
