using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayersGenerators : MonoBehaviour {

    [SerializeField]
    private GameObject playerSX;
    [SerializeField]
    private GameObject playerDX;
    [SerializeField]
    private GameObject node;
    [SerializeField]
    int ropeSize = 0;
    [SerializeField]
    float offset = 1.5f;
    [SerializeField]
    float nodeSize = 0.5f;

    // Use this for initialization
    void Start() {
        GameObject pSx = Instantiate(playerSX, new Vector3((-(ropeSize) / 2) * offset, 0.0f, 0.0f), Quaternion.identity);
        pSx.transform.parent = gameObject.transform;
        pSx.name = "Player One";
        GameObject pDx = Instantiate(playerDX, new Vector3(((ropeSize+2) / 2) * offset, 0.0f, 0.0f), Quaternion.identity);
        pDx.transform.parent = gameObject.transform;
        pDx.name = "Player Two";

        GameObject[] nodes = new GameObject[ropeSize];
        for (int i = 0; i < ropeSize; i++) {
            float x = ((-ropeSize / 2) * offset) + ((i + 1) * offset);
            nodes[i] = Instantiate(node, new Vector3(x, 0.0f, 0.0f), Quaternion.identity);
            nodes[i].transform.parent = gameObject.transform;
            nodes[i].name = "Node " + (i + 1);
            nodes[i].transform.localScale = Vector3.one * nodeSize;
        }

        // Forward
        for (int i = 0; i < nodes.Length; i++) {
            SpringJoint2D[] spring = nodes[i].GetComponents<SpringJoint2D>();
            if (i == 0) {
                spring[0].connectedBody = pSx.GetComponent<Rigidbody2D>();
            } else {
                spring[0].connectedBody = nodes[i-1].GetComponent<Rigidbody2D>();
            }
        }

        // Backward
        for (int i = nodes.Length-1; i >=0; i--) {
            SpringJoint2D[] spring = nodes[i].GetComponents<SpringJoint2D>();
            if (i == nodes.Length - 1) {
                spring[1].connectedBody = pDx.GetComponent<Rigidbody2D>();
            } else {
                spring[1].connectedBody = nodes[i + 1].GetComponent<Rigidbody2D>();
            }
        }

    }

}
