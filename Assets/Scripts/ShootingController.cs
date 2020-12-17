using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Ball defaultBall;
    private Camera cam;
    public Shader lineShader;
    public float lineWidth = 0.02f;
    public Color lineColor = Color.gray;
    private LineRenderer lr;
    private bool lineIsDrawn = false;
    private GameObject myLine;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    Vector3 initTap = default;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))  //if is 
        {
            Vector3 tapPlace = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

            if (myLine == null)
            {
                initTap = tapPlace;
                Vector3 endPoint = tapPlace;
                initTap.y = -4.0f;
                DrawLine(initTap, endPoint);
            }
            else
            {
                Debug.Log(initTap);
                Vector3 end = tapPlace;
                LineRenderer lr = myLine.GetComponent<LineRenderer>();
                lr.SetPosition(1, end);
            }
                    
        }
        else if (myLine != null)
        {
            Vector3 tapPlace = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
            Ball newBall = spawnBall(defaultBall, initTap);
            Vector3 velocity3D = Vector3.Normalize(tapPlace - initTap) * 3.0f;
            newBall.Velocity = new Vector2(velocity3D.x, velocity3D.y);
            Debug.Log(newBall.Velocity);
            GameObject.Destroy(myLine);
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(lineShader);
        lr.startColor = lr.endColor = lineColor;
        lr.startWidth = lr.endWidth = lineWidth;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Debug.Log("create line");
    }

    Ball spawnBall(Ball ball,Vector3 position)
    {
        return Instantiate(ball, position, Quaternion.identity);
    }

}
