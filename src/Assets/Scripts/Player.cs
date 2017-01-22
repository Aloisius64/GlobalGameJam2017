using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int weight = 1;
    [SerializeField]
    private int minWeight = 1;
    [SerializeField]
    private int maxWeight = 10;
    [SerializeField]
    private float minSize = 1.5f;
    [SerializeField]
    private float maxSize = 4.0f;
    [SerializeField]
    private float minPower = 3.0f;
    [SerializeField]
    private float maxPower = 20.0f;

    private PlayerController playerController;
    private Rigidbody2D rigidBody;

    public int Weight {
        get {
            return weight;
        }

        set {
            weight = value;
            weight = weight < minWeight ? minWeight : weight;
            weight = weight > maxWeight ? maxWeight : weight;
        }
    }
    
    // Use this for initialization
    void Start () {
        playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        float factor = (float)(weight - minWeight) / (float)(maxWeight - minWeight);
        float newSize = minSize + (maxSize - minSize) * factor;
        transform.localScale = Vector3.one * newSize;
        playerController.Power = minPower + (maxPower - minPower) * (1.0f - factor);
    }

    public void IncreaseScore() {
        score++;
        Weight = score+1;
    }

    public void DecreaseScore() {
        score--;
        Weight = score+1;
    }
}
