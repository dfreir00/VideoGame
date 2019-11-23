using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modificarColor : MonoBehaviour
{

    //modificar el color del circulo del fondo
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
     }
}
