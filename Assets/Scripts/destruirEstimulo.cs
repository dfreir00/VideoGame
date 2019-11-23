using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruirEstimulo : MonoBehaviour
{
     // Update is called once per frame
    void Update()
    {
        destruir();
    }

    //este metodo se utiliza para destruir los estimulos cuando vas clicando sobre ellos
    public void destruir()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Destroy(GameObject.FindWithTag("Estimulo"));
        }
    }
}
