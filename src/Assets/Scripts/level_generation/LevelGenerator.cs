using System;
using System.Collections.Generic;
using UnityEngine;


class LevelGenerator : MonoBehaviour {
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int how_many = 10;
    [SerializeField]
    private GameObject manager = null;
    private AssetsPool assetsPool = null;

    void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager");
        assetsPool = manager.GetComponent<AssetsPool>();
    }

    void Update() {
        if (assetsPool.Loaded) {
            HashSet<Vector3> half_sx = new HashSet<Vector3>();
            HashSet<Vector3> half_dx = new HashSet<Vector3>();

            getLevel(ref half_sx, ref half_dx);

            foreach (var item in half_sx) {
                GameObject point = null;
                if (assetsPool.GetFreeObjectFromPool(eObjectType.COIN_SX, out point)) {
                    point.transform.position = item;
                    point.transform.rotation = Quaternion.identity;
                }
            }

            foreach (var item in half_dx) {
                GameObject point = null;
                if (assetsPool.GetFreeObjectFromPool(eObjectType.COIN_DX, out point)) {
                    point.transform.position = item;
                    point.transform.rotation = Quaternion.identity;
                }
            }

            enabled = false;
        }
    }

    public void getLevel(ref HashSet<Vector3> half_sx, ref HashSet<Vector3> half_dx) {

        for (int i = 0; i < how_many; i++) {
            half_dx.Add(new Vector3(UnityEngine.Random.Range(0.1f, width), UnityEngine.Random.Range(-height, height), 0.0f));
        }

        // Simmetric
        foreach (var item in half_dx) {
            half_sx.Add(new Vector3(-item.x, item.y, 0.0f));
        }

    }
}