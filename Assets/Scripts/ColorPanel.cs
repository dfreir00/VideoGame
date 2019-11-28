using UnityEngine.UI;
using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    private Datos config;

    public Color colorFondo;

    public void obtenemosColorFondo()
    {

        string[] vec = config.colorFondo.Split('.');
        colorFondo.r = float.Parse(vec[0]) / 255f;
        colorFondo.g = float.Parse(vec[1]) / 255f;
        colorFondo.b = float.Parse(vec[2]) / 255f;

        colorFondo = new Color(colorFondo.r, colorFondo.g, colorFondo.b);


    }

    // Start is called before the first frame update
    void Start()
    {
        //Consigo la imagen del panel
        GameObject panel = GameObject.Find("Panel");
        Image imagen = panel.GetComponent<Image>();

        //Accedo a la base de datos y guardo la configuracion
        FuncionesBBDD bbdd = new FuncionesBBDD();
        config = bbdd.leerConfiguracion();

        //Guardo el color del fondo de la configuracion
        obtenemosColorFondo();

        //Asigno el color a la imagen
        imagen.color = colorFondo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
