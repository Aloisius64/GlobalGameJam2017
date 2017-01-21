using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour {

    [SerializeField]
    private GameObject manager = null;
    private AssetsPool assetsPool = null;
    [SerializeField]
    private float explosionTime = 4.0f;
    [SerializeField]
    private float explosionRadius = 10.0f;
    [SerializeField]
    private float explosionPower = 10.0f;

    void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        assetsPool = manager.GetComponent<AssetsPool>();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag.Contains("Player")) {
            Debug.Log("Boom!");

            foreach (ContactPoint2D missileHit in coll.contacts) {
                Vector2 hitPoint = missileHit.point;

                StartCoroutine(ExplosionParticlesEffect(hitPoint));

                Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint, explosionRadius);

                foreach (var item in colliders) {
                    if (item.tag == "Coin") {
                        //Debug.Log("Object: " + item.name);
                        Rigidbody2D rigidBody = item.GetComponent<Rigidbody2D>();
                        
                        Vector2 coinPos = rigidBody.gameObject.transform.position;
                        Vector2 dir = (coinPos - hitPoint);
                        dir.Normalize();
                        
                        rigidBody.AddForce(dir * explosionPower);
                    }
                }

                break;
            }

        }
    }

    private IEnumerator ExplosionParticlesEffect(Vector2 hitPoint) {
        GameObject effect = null;
        if (assetsPool.GetFreeObjectFromPool(eObjectType.EXPLOSION, out effect)) {
            effect.transform.position = hitPoint;
            effect.transform.rotation = Quaternion.identity;
            effect.GetComponent<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(explosionTime);

        // Play sound
        //Manager.audioManager.PlayOneShot(audioSource, SoundGlossary.ENEMY_KILLED_PS);
        //yield return new WaitForSeconds(2.0f);

        assetsPool.FreeObjectPool(eObjectType.EXPLOSION, effect);
    }

}
