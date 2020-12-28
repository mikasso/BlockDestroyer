using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Ball defaultBall;
    public int amount = 5;
    public Shader lineShader;
    public float lineWidth = 0.02f;
    public Color lineColor = Color.gray;
    public float Speed = 3.0f;
    public float lineYStart = 0.3f;
    
    public int ballsAlive = 0;
    private Camera cam;
    private LineRenderer lr;
    private bool lineIsDrawn = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        GameObject myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(lineShader);
        lr.startColor = lr.endColor = lineColor;
        lr.startWidth = lr.endWidth = lineWidth;
        Vector3 start = new Vector3(0, 0, 0);
        lr.SetPosition(0, start);
        lr.SetPosition(1, start);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))
            return;

        if (Input.GetMouseButton(0)) {
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
        }
        else if (lineIsDrawn == true) {
            Vector3 velocity = Vector3.Normalize(lr.GetPosition(1) - lr.GetPosition(0)) * Speed;
            StartCoroutine(
                spawnBall(defaultBall, lr.GetPosition(0), velocity));
            lr.SetPosition(0, new Vector2(0, 0));
            lr.SetPosition(1, new Vector2(0, 0));
            lineIsDrawn = false;
        }
    }

    IEnumerator spawnBall(Ball ball, Vector3 position,Vector3 velocity)
    {
        for (int i = 0; i < amount; i++)
        {
            Ball newBall = Instantiate(ball, position, Quaternion.identity);
            newBall.Velocity = new Vector2(velocity.x, velocity.y);
            ballsAlive++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    Vector3 getTapPlace()
    {
        return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
    }

    public void DecrementBallsAlive() {
        ballsAlive--;
    }
}
