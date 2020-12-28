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
    public float Speed = 3.0f;
    public float lineYStart = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(lineShader);
        lr.startColor = lr.endColor = lineColor;
        lr.startWidth = lr.endWidth = lineWidth;
        Vector3 start = new Vector3(0, 0, 0);
        lr.SetPosition(0, start);
        lr.SetPosition(1, start);
        lr.SetPosition(1, new Vector3(1.0f,1.0f,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector3 tapPlace = getTapPlace();
            if (lineIsDrawn == false)
            {
                lr.SetPosition(0, new Vector2(tapPlace.x, lineYStart));
                lineIsDrawn = true;
                return;
            }
            Vector2 direction = new Vector2(tapPlace.x, tapPlace.y);
            RaycastHit2D hit2D = Physics2D.Raycast(lr.GetPosition(0), direction);
            lr.SetPosition(1, hit2D.point);
            Debug.Log("Is down");
            Debug.Log(hit2D.point);
        } 
        else if(lineIsDrawn == true) {
            Ball newBall = spawnBall(defaultBall, lr.GetPosition(0));
            Vector3 velocity3D = Vector3.Normalize(lr.GetPosition(1) - lr.GetPosition(0)) * Speed;
            newBall.Velocity = new Vector2(velocity3D.x, velocity3D.y);
            lr.SetPosition(0, new Vector2(0,0));
            lr.SetPosition(1, new Vector2(0, 0));
            lineIsDrawn = false;
        }
    }

    Ball spawnBall(Ball ball,Vector3 position)
    {
        position.y = 0.3f;
        return Instantiate(ball, position, Quaternion.identity);
    }

    Vector3 getTapPlace()
    {
        return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
    }

}
