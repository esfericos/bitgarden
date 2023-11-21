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

    private int top = 100;
    private int bottom = 0;
    private int left = 0;
    private int right = 100;


    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
        cam = GetComponent<Camera>();
    }

    /** Camera Zoom Control */
    private void Update()
    {
        if (Input.mousePosition.x >= 1450) return;
        if (Input.mousePosition.y >= 974) return;

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
        if (Input.mousePosition.y >= 974) return;

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
            if ((Origin - Difference).x > right || (Origin - Difference).x < left) return;
            if ((Origin - Difference).y > top || (Origin - Difference).y < bottom) return;
            Camera.main.transform.position = Origin - Difference;
        }

        if(Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }

    }

    public void SetRange(int top, int bottom, int right, int left)
    {
        Debug.Log("Received t:" + top + " r:" + right + " l:" + left + " b:" + bottom);
        this.top = top + 8;
        this.right = right + 15;
        this.left = left - 6;
        this.bottom = bottom - 6;
    }
}
