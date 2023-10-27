using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /** Camera X,Y Movement variables*/
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;
    private bool drag = false;

    /** Camera zoom control variables*/
    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;
    private Camera cam;


    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
        cam = GetComponent<Camera>();
    }

    /** Camera Zoom Control */
    private void Update()
    {
        if (Input.mousePosition.x >= 1450) return;

        if (Input.mouseScrollDelta.y > 0)
        {
            cam.orthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
        }

        if(Input.mouseScrollDelta.y < 0)
        {
            cam.orthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinSize, MaxSize);
    }

    /** Camera X,Y Movement */
    private void LateUpdate()
    {
        if (Input.mousePosition.x >= 1450) return;

        if (Input.GetMouseButton(0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);

            if(drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        } else
        {
            drag = false;
        }

        if(drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }

        if(Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }

    }
}
