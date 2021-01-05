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

    private Camera cam;
    private LineRenderer lr;
    private bool lineIsDrawn;
    private int amount;
    private Ball ball;
    private bool endOfShooting;

    // Start is called before the first frame update
    void Start()
    {
        initShootingParameters();
        initLineRendering();
        clearLine();
        cam = Camera.main;
    }

    private void initShootingParameters()
    {
        PlayerManager player = GetComponent<PlayerManager>();
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
    void Update()
    {
        if (CheckIfCanShoot() == false)
            return;

        if (Input.GetMouseButton(0)) {
           Vector3 tapPoint = getTapPlace();
            if (checkIfYIsInScope(tapPoint.y) == false) 
                clearLine();
            else if (lineIsDrawn == false) 
                initLine(tapPoint);
            else
                reArrangeLine(tapPoint);
        } else if (lineIsDrawn == true)
            startShooting();
    }

    private bool CheckIfCanShoot()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))
            return false;
        else if (endOfShooting == true)
        {
            endOfShooting = false;
            onEndOfShooting();
            return true;
        }
        else
            return true;
    }

    private void initLine(Vector3 tapPoint)
    {
        tapPoint.x = adjustXPos(tapPoint.x);
        lr.SetPosition(0, new Vector2(tapPoint.x, minY));
        lineIsDrawn = true;
    }
    private void reArrangeLine(Vector3 tapPoint)
    {
        Vector2 origin = lr.GetPosition(0);
        Vector2 direction = new Vector2(tapPoint.x - origin.x, tapPoint.y - origin.y);
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction);
        lr.SetPosition(1, hit2D.point);
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

    private void onEndOfShooting()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        BlocksManager bm = GetComponent<BlocksManager>();
        if (bm.GenerateNewLineOfBlocks() == true)
        {
            amount += 1;
            pm.IncreaseBallsAmount();
        }
        else // it s a lost game, end..
        {
            pm.LostGame();
            enabled = false; // turn off this component.. no more shooting ;(
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

    private bool checkIfYIsInScope(float y)
    {
        if (y < minY || y > maxY)
            return false;
        return true;
    }

    public int Amount { get { return amount; } }
    public string BallName { get { return ball.name; } }
}
