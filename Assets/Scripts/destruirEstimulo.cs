using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruirEstimulo : MonoBehaviour
{
    //este metodo se utiliza para destruir los estimulos cuando vas clicando sobre ellos
    public void OnMouseDown()
    {
        Destroy(gameObject);
        
    }
}
