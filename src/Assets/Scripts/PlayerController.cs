using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private string up;
    [SerializeField]
    private string right;
    [SerializeField]
    private string down;
    [SerializeField]
    private string left;

    Rigidbody2D rigidBody;
    Vector2 force;
    [SerializeField]
    float power = 1.0f;

    public float Power {
        get {
            return power;
        }

        set {
            power = value;
        }
    }

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }
	
	// Update is called once per frame
	void Update () {
        force = Vector2.zero;

        if (Input.GetKey(up)) {
            //Debug.Log("Press key up");
            force.y += 1.0f;
        } else if (Input.GetKey(right)) {
            //Debug.Log("Press key right");
            force.x += 1.0f;
        } else if (Input.GetKey(down)) {
            //Debug.Log("Press key down");
            force.y -= 1.0f;
        } else if (Input.GetKey(left)) {
            //Debug.Log("Press key left");
            force.x -= 1.0f;
        }
        
        rigidBody.AddForce(force * Power);
    }
}
