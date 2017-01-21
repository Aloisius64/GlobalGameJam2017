using System;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour {

    public int direction { get; set; }

    Rigidbody2D rigidBody;
    Vector2 force;
    [SerializeField]
    float power = 1.0f;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    void Update() {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        direction = getDirection(coins);

        if (direction < 0) return;
        
        float tmp = (float)(direction * Math.PI / 4.0f);
        force = new Vector2(Mathf.Cos(tmp), Mathf.Sin(tmp));
        rigidBody.AddForce(force * power);
    }

    private boolean isMyCoin(item){
        if (GameObject.tag.contains("sx") && item.transform.position.x <= 0)
            return true;

        if (GameObject.tag.contains("dx") && item.transform.position.x >= 0)
            return true;

        return false;
    }

    private int getDirection(GameObject[] coins) {
        double min_distance = Double.MaxValue;
        GameObject min_item = null;
        foreach (var item in coins) {

            if (isMyCoin(item))
            {
                float distance = (gameObject.transform.position - item.transform.position).magnitude;

                if (distance < min_distance) {
                    min_distance = distance;
                    min_item = item;
                }
            }

        }

        if (min_item) {
            double angle = getAngle(gameObject.transform.position.x, gameObject.transform.position.y,
                min_item.transform.position.x, min_item.transform.position.y);

            return (int)(angle / (Math.PI / 4));
        }

        return -1;
    }

    private double getAngle(float x1, float y1, float x2, float y2) {
        double angle = Math.Atan2(y2 - y1, x2 - x1);
        if (angle < 0)
            angle += 2 * Math.PI;
        return angle;
    }
}
