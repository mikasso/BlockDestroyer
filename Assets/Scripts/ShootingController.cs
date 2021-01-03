using System;
using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public float shootingIntervals = 0.1f;
    public float lineWidth = 0.02f;
    public Material lineMaterial;
    public float speed = 3.0f;
    public float lineYStart = 0.3f;
    public int initialBallsAmount = 5;

    private Camera cam;
    private LineRenderer lr;
    private bool lineIsDrawn;
    private int amount;
    private Ball ball;

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
        amount = player.ReadAmount();
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
        if (GameObject.FindGameObjectWithTag("Ball"))
            return;

        if (Input.GetMouseButton(0)) {
            reArrangeLine(getTapPlace());
        }
        else if (lineIsDrawn == true) {
            Vector3 velocity = Vector3.Normalize(lr.GetPosition(1) - lr.GetPosition(0)) * speed;
            StartCoroutine(
                spawnBalls(ball, lr.GetPosition(0), velocity));
            clearLine();
        }
    }

    private void reArrangeLine(Vector3 tapPoint)
    {
        if (lineIsDrawn == false)
        {
            lr.SetPosition(0, new Vector2(tapPoint.x, lineYStart));
            lineIsDrawn = true;
            return;
        }
        RaycastHit2D hit2D = Physics2D.Raycast(lr.GetPosition(0), tapPoint);
        lr.SetPosition(1, hit2D.point);
    }

    IEnumerator spawnBalls(Ball ball, Vector3 position,Vector3 velocity)
    {
        for (int i = 0; i < amount; i++)
        {
            Ball newBall = Instantiate(ball, position, Quaternion.identity);
            newBall.Velocity = new Vector2(velocity.x, velocity.y);
            yield return new WaitForSeconds(shootingIntervals);
        }
        onEndOfShooting();
    }

    private void onEndOfShooting()
    {
        BlocksManager bm = GetComponent<BlocksManager>();
        if (bm.GenerateNewLineOfBlocks() == true)
        {
            amount += 1;
        }
        else // it s lost game
        {
            PlayerManager pm = GetComponent<PlayerManager>();
            pm.LostGame();
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

    public int Amount { get { return amount; } }
    public string BallName { get { return ball.name; } }
}
