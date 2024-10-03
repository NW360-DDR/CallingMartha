using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalc : MonoBehaviour
{
    private Transform playerPos;
    private Vector3 targetPos;
    private Vector3 targetDir;

    public float angle;
    public int index;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player (Remake)").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate angle from camera to enemy
        targetPos = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        index = SendIndex(angle);
    }

    //calculate angle index
    private int SendIndex(float angle)
    {
        if (angle > -45f && angle < 45f)
            return 0;
        if (angle <= -45f && angle > -135f)
            return 1;
        if (angle <= -135f || angle > 135f)
            return 2;
        if (angle <= 135f && angle > 45f)
            return 3;

        return index;
    }
}