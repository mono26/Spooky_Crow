using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    private static CameraDrag instance;
    public static CameraDrag Instance
    {
        get { return instance; }
    }
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public float maxHorizontal;
    public float maxVertical;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
    }
    private void LateUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);

            if (pos.x < -0.035f || pos.x > 0.035f)
            {
                cameraDragging = true;
            }
            else if (pos.x > -0.035f && pos.x < 0.035f)
            {
                cameraDragging = false;
            }
            if (pos.y < -1f || pos.y > 1f)
            {
                cameraDragging = true;
            }
            else if (pos.y > -0.035f && pos.y < 0.035f)
            {
                cameraDragging = false;
            }

            if (cameraDragging)
            {
                if (!Input.GetMouseButton(0)) return;

                Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

                transform.Translate(move, Space.Self);

                transform.position = new Vector3
                    (
                        Mathf.Clamp(transform.position.x, -maxHorizontal, maxHorizontal),
                        transform.position.y,
                        Mathf.Clamp(transform.position.z, -maxVertical, maxVertical)
                    );
            }
        }
        else
        {
            cameraDragging = false;
        }
    }
}
