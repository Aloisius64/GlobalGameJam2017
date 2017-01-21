using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour {

    [SerializeField]
    private GameObject manager = null;
    private AssetsPool assetsPool = null;
    [SerializeField]
    private float explosionTime = 4.0f;

    void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        assetsPool = manager.GetComponent<AssetsPool>();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            Debug.Log("Boom!");

            GameObject explosionSystem = Instantiate(Resources.Load("Prefabs/Explosion")) as GameObject;

            foreach (ContactPoint2D missileHit in coll.contacts) {
                Vector2 hitPoint = missileHit.point;

                //Instantiate(explosionSystem, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);

                StartCoroutine(ExplosionParticlesEffect(hitPoint));
                break;
            }
        }
    }

    private IEnumerator ExplosionParticlesEffect(Vector2 hitPoint) {
        GameObject effect = null;
        if (assetsPool.GetFreeObjectFromPool(eObjectType.EXPLOSION, out effect)) {
            effect.transform.position = hitPoint;
            gameObject.transform.rotation = Quaternion.identity;
            effect.GetComponent<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(explosionTime);

        // Play sound
        //Manager.audioManager.PlayOneShot(audioSource, SoundGlossary.ENEMY_KILLED_PS);
        //yield return new WaitForSeconds(2.0f);

        assetsPool.FreeObjectPool(eObjectType.EXPLOSION, effect);
    }

}
