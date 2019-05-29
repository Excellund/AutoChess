using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float panBorderThickness = 10f, panSpeed = 10f;
    [SerializeField]
    private Vector2 panLimitHorizontal, panLimitVertical;
    [SerializeField]
    private float scrollSpeed = 10f, minY = 20f, maxY = 40f;

    // Constant
    private static float SCROLL_MULTIPLIER = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.CamPan();
    }

    private void CamPan()
    {
        Vector3 camPosition = this.transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // If not already by the limit
        if (!((camPosition.y == minY && scroll > 0) || (camPosition.y == maxY && scroll < 0)))
        {
            Vector3 previousPos = camPosition;
            float distance = scroll * scrollSpeed * SCROLL_MULTIPLIER * Time.deltaTime;
            camPosition += this.transform.forward * distance;

            /* TODO: Fix camera bugs
            if (camPosition.y > maxY)
            {
                float change = camPosition.y - previousPos.y;
                float top = camPosition.y - maxY;
                float multiplier = 2 - top / change;
                camPosition = Vector3.Scale(camPosition, new Vector3(multiplier, multiplier, multiplier));
            }
            if (camPosition.y < minY)
            {
                float change = previousPos.y - camPosition.y;
                float top = minY - camPosition.y;
                float multiplier = 2 - top / change;
                camPosition = Vector3.Scale(camPosition, new Vector3(multiplier, multiplier, multiplier));
            }
            */
        }

        // If the mouse is by the edges, adjust the desired position
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            camPosition.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            camPosition.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            camPosition.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            camPosition.x -= panSpeed * Time.deltaTime;
        }

        camPosition.x = Mathf.Clamp(camPosition.x, panLimitHorizontal.x, panLimitHorizontal.y);
        camPosition.y = Mathf.Clamp(camPosition.y, minY, maxY);
        camPosition.z = Mathf.Clamp(camPosition.z, panLimitVertical.x, panLimitVertical.y);

        // Change the position 
        this.transform.position = camPosition;
    }
}


