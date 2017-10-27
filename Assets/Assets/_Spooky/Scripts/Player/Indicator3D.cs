using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Indicator3D : MonoBehaviour

{
    //Singleton part
    private static Indicator3D instance;
    public static Indicator3D Instance
    {
        get { return instance; }
    }
    public int maxTracked;

    public Image[] flechas;

    private Camera cam;

    public int trackedIndex;
    private Plane[] CameraPlanes;
    public GameObject[] colisionPlanes;

    private GameObject trackedObj;

	// Use this for initialization
	void Awake()
    {

        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else Destroy(gameObject);

        //DESACTIVA LAS IMAGENES DEL CANVAS AL EMPEZAR, PARA QUE SIEMPRE EXISTAN, PERO NO SE VEAN.
        for (int j = 0; j < flechas.Length; j++)
        {
            flechas[j].enabled = false;
        }
        trackedIndex = 0;
        CameraPlanes = new Plane[6];


        //MANEJO DE PLANOS DEL BORDE DE CAMARAS
        cam = Camera.main;
        CameraPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        //crearPlanos();
        //colisionPlanes = new GameObject[6];
        // GameObject[] trackedObjects = new GameObject[maxTracked];
        //trackedObjects = new Queue<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
       //SOLO PARA TESTEAR MOVIENDO LA CAMARA
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cam.orthographicSize += 0.01f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cam.orthographicSize -= 0.01f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cam.transform.Rotate(0,0,1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam.transform.Rotate(0,0,-1f);
        }

        //PARA QUE LOS PLANOS CON LOS QUE VA A COLISIONAR EL RAYO, SIEMPRE ESTEN SOBRE EL BORDE DE LA PANTALLA.
        CameraPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
        for (int i = 0; i< CameraPlanes.Length; i++)
        {
            colisionPlanes[i].transform.position = -CameraPlanes[i].normal * CameraPlanes[i].distance;
            colisionPlanes[i].transform.rotation = Quaternion.FromToRotation(Vector3.up, CameraPlanes[i].normal);
        }
    }

    public IEnumerator TurnOffCR()
    {
        yield return new WaitForSeconds(5f);
        
    }
    public void addObject(GameObject obj)
    {
        if (trackedIndex >= maxTracked-1)
            trackedIndex = 0;
        trackedIndex++;
        StartCoroutine(TurnOffCR());
        StartRayCast(obj);

    }
   

    public void StartRayCast(GameObject obj)
    {
        Debug.Log("lanze el rayo a: " +obj.gameObject.name);
        trackedObj = obj;
        RaycastHit rayHit;
        Vector3 dir = obj.transform.position - this.transform.position;
        Ray ray = new Ray(this.transform.position, dir);
        dir = dir.normalized;
        if (Physics.Raycast(ray, out rayHit, Screen.width * 2))
        {
            MostrarFlecha(rayHit);
        }
    }

    public void MostrarFlecha(RaycastHit hit)
    {
        var dir = hit.point-this.transform.position;
        dir = dir.normalized;
        var pos = cam.WorldToScreenPoint(hit.point - dir);
        flechas[trackedIndex].enabled = true;
        flechas[trackedIndex].transform.position = pos;

        StartCoroutine(ApagarFlechaCR(trackedIndex));
    }

    public IEnumerator ApagarFlechaCR(int flechaApagar)
    {
        yield return new WaitForSeconds(5);
        flechas[flechaApagar].enabled = false;
    }

    //ESTO ERA PARA CREAR LOS PLANOS QUE SE MOVERIAN CON LA CAMARA, PERO SON MUY PEQUEÑOS
    //HABRIA QUE CAMBIARLES EL TAMAÑO EN EJECUCION Y ES MAS COSTOSO.
    //public void crearPlanos()
    //{

    //    //int i = 0;
    //    //while (i < CameraPlanes.Length) 
    //    //{
    //    //    GameObject p = GameObject.CreatePrimitive(PrimitiveType.Plane);
    //    //    p.name = "Plane " + i.ToString();
    //    //    p.transform.position = -CameraPlanes[i].normal * CameraPlanes[i].distance;
    //    //    p.transform.rotation = Quaternion.FromToRotation(Vector3.up, CameraPlanes[i].normal);
    //    //    colisionPlanes[i] = p;
    //    //    i++;
    //    //}
    //}
}
