using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public float maxHorizontal;
    public float maxVertical;

    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.z);

        float left = Screen.width * 0.45f;
        float right = Screen.width - (Screen.width * 0.45f);
        float down = Screen.height * 0.45f;
        float up = Screen.width - (Screen.height * 0.45f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }
        if (mousePosition.y < down)
        {
            cameraDragging = true;
        }
        else if (mousePosition.y > up)
        {
            cameraDragging = true;
        }
        if (cameraDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }
            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
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


}
