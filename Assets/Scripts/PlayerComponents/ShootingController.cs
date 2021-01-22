using System;
using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public float shootingIntervals = 0.1f;
    public float lineWidth = 0.02f;
    public Material lineMaterial;
    public float speed = 3.0f;
    public float minY = 0.3f;
    public float maxY = 9.0f;
    public float minX = 0.15f;
    public float maxX = 8.77f;
    public float minDegree = 5.0f;
    public int Amount { get { return amount; } }
    public string BallName { get { return ball.name; } }
    private PlayerManager player;
    private Camera cam;
    private LineRenderer lr;
    private bool lineIsDrawn;
    private int amount;
    private Ball ball;
    private bool endOfShooting;

    // Start is called before the first frame update
    public void Start()
    {
        cam = Camera.main;
        player = GetComponent<PlayerManager>();
        initShootingParameters();
        initLineRendering();
        clearLine();
    }

    private void initShootingParameters()
    {
        amount = player.ReadInteger(PlayerManager.Key.BallAmount, player.InitalBallsAmount);
        string ballPath = "Prefabs/" + player.ReadBallName();
        ball = Resources.Load<GameObject>(ballPath).GetComponent<Ball>();
    }

    void initLineRendering() {
        GameObject myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = lr.endWidth = lineWidth;
    }
    // Update is called once per frame
    public void Update()
    {
        if (CheckIfCanShoot() == false)
            return;

        if (Input.GetMouseButtonDown(0)) {          //user has clicked somewhere
            Debug.Log("clicked");
            Vector3 tapPoint = getTapPlace();
            if (checkIfYIsInScope(tapPoint) == false)
                clearLine();
            else if (lineIsDrawn == false)
                initLine(tapPoint);
        }else if (Input.GetMouseButton(0) && lineIsDrawn)          
                reArrangeLine(getTapPlace());   //user has dragged pointer to somewhere
        else if (lineIsDrawn == true)
            startShooting();
    }

    private bool CheckIfCanShoot()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))
            return false;
        else if (endOfShooting == true)
        {
            endOfShooting = false;
            amount++;
            player.AfterShootingJobs();
            return true;
        }
        else
            return true;
    }

    private void initLine(Vector3 tapPoint)
    {
        tapPoint.x = adjustXPos(tapPoint.x);
        lr.SetPosition(0, new Vector2(tapPoint.x, minY));
        lr.SetPosition(1, new Vector2(tapPoint.x, minY+0.01f));
        lineIsDrawn = true;
    }
    private void reArrangeLine(Vector3 tapPoint)
    {
        Vector2 origin = lr.GetPosition(0);
        Vector2 direction = new Vector2(tapPoint.x - origin.x, tapPoint.y - origin.y);
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction);
        Vector2 hitPoint = hit2D.point;
        float yDistance = Mathf.Abs(hitPoint.y - origin.y);
        float xDistance = Mathf.Abs(hitPoint.x - origin.x);
        float angle = Mathf.Atan2(yDistance, xDistance);
        if (Mathf.Rad2Deg * angle >= minDegree)
            lr.SetPosition(1, hitPoint);
        else
        {
            Debug.Log("Angle is too small");
            clearLine();
        }
    }

    private void startShooting()
    {
        Vector3 velocity = Vector3.Normalize(lr.GetPosition(1) - lr.GetPosition(0)) * speed;
        StartCoroutine(
            spawnBalls(ball, lr.GetPosition(0), velocity));
        clearLine();
        endOfShooting = true;
    }
    IEnumerator spawnBalls(Ball ball, Vector3 position,Vector3 velocity)
    {
        for (int i = 0; i < amount; i++)
        {
            Ball newBall = Instantiate(ball, position, Quaternion.identity);
            newBall.Velocity = new Vector2(velocity.x, velocity.y);
            yield return new WaitForSeconds(shootingIntervals);
        }
    }
    void clearLine()
    {
        Vector2 point = new Vector2(0, 0);
        lr.SetPosition(0, point);
        lr.SetPosition(1, point);
        lineIsDrawn = false;
    }
    Vector3 getTapPlace()
    {
        return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
    }

    private float adjustXPos(float x)
    {
        if (x < minX)
            return minX;
        if (x > maxX)
            return maxX;
        return x;
    }

    private bool checkIfYIsInScope(Vector3 tapPlace)
    {
        float y = tapPlace.y;
        if (y < minY || y > maxY)
            return false;

        return true;
    }
}
