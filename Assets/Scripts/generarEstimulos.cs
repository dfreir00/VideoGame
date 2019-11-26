using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generarEstimulos : MonoBehaviour
{
    public GameObject prefab;
    public int numEstimulos;
    private int fallos = 0;

    // Start is called before the first frame update
    void Start()
    {
        generarEstimulo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //este metodo me genera tantos estimulos como se introduzcan
    public void generarEstimulo()
    {
        for (int i = 0; i < numEstimulos; i++)
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
