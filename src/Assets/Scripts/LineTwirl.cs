using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class LineTwirl : MonoBehaviour {

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Twirl effect;
    [SerializeField]
    private int twirlStep = 2;

    private float width = 40.6f;
    private float minY = -19;
    private float maxY = 24;

    private Vector2 center = new Vector2(0.5f, 0.5f);

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Contains("Player")) {

            if (Mathf.Abs(collision.gameObject.transform.position.x) < 2.0f) {
                StartCoroutine(TwirlEffect(collision.gameObject));
            }

        }
    }

    // Use this for initialization
    void Start() {
        effect = camera.GetComponent<Twirl>();
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator TwirlEffect(GameObject player) {
        effect.center.x = (player.transform.position.x - (-width)) / (width - (-width));
        effect.center.y = (player.transform.position.y - (minY)) / (maxY - (minY));

        effect.angle = 0;
        while (effect.angle < 360) {
            effect.angle += twirlStep;
            yield return null;
        }

        yield return null;
    }
}
