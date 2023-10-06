using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xSpd;
    float ySpd;
    public float SpdMult = 3.5f;

    public GameObject enemyObject;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer enemyCircSptRendr;
    private SpriteRenderer enemySqreSptRendr;
    private SpriteRenderer[] enemiesSqreSptRendr;
    private Vector2 circleCenter;
    private Vector2 enemyCircleCenter;
    private float playerRadius;
    private float enemyRadius;
    private Bounds squareBounds;
    private Bounds enemySquareBounds;
    private bool showDebug = true;

    // Start is called before the first frame update
    void Start()
    {
        xSpd = 0;
        ySpd = 0;
        enemyObject = GameObject.Find("Enemy");
        Debug.Log(enemyObject);
        // inicialização dos SpriteRenderers
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemiesSqreSptRendr = enemyObject.GetComponentsInChildren<SpriteRenderer>();
        enemyCircSptRendr = enemiesSqreSptRendr[0];
        enemySqreSptRendr = enemiesSqreSptRendr[1];

    }

    // Update is called once per frame
    void Update()
    {
        xSpd = Input.GetAxis("Horizontal") * SpdMult;
        ySpd = Input.GetAxis("Vertical") * SpdMult;
        move(xSpd, ySpd);

        CalculateCircleParameters();
        if (CheckCircleIntersection())
        {
            // UnityEditor.EditorApplication.isPlaying = false;
            if (showDebug == true)
            {
                Debug.Log("Ocorreu a primeira colisão");
                showDebug = false;
            }
            CalculateSquareBounds();

            if (CheckSquareIntersection())
                UnityEditor.EditorApplication.isPlaying = false;
            // Debug.Log("Ocorreu a segunda colisão");

        }

    }

    void move(float xSpd, float ySpd)
    {
        Vector2 playerPos = transform.position;

        Vector2 v = new Vector2(xSpd, ySpd);
        playerPos += v * Time.fixedDeltaTime;
        transform.position = playerPos;
    }

    private void CalculateCircleParameters()
    {
        circleCenter = transform.position;
        playerRadius = spriteRenderer.bounds.extents.x;

        enemyCircleCenter = enemyObject.transform.position;
        enemyRadius = enemyCircSptRendr.bounds.extents.x;
    }

    private void CalculateSquareBounds()
    {
        squareBounds = spriteRenderer.bounds;
        enemySquareBounds = enemySqreSptRendr.bounds;
    }

    private bool CheckCircleIntersection()
    {
        Vector2 distanceVector = circleCenter - enemyCircleCenter;
        float distance = distanceVector.magnitude;
        float totalRadius = playerRadius + enemyRadius;


        if (distance <= totalRadius)
            return true;

        return false;
    }

    private bool CheckSquareIntersection()
    {

        if (squareBounds.Intersects(enemySquareBounds))
            return true;

        return false;
    }
}