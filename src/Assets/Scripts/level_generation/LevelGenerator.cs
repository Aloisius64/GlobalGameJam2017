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
            List<Vector3> points = getLevel();
            foreach (var item in points) {
                GameObject point = null;
                if (assetsPool.GetFreeObjectFromPool(eObjectType.COIN, out point)) {
                    point.transform.position = item;
                    point.transform.rotation = Quaternion.identity;
                }
            }

            enabled = false;
        }
    }

    public List<Vector3> getLevel() {
        HashSet<Vector3> half_sx = new HashSet<Vector3>();
        HashSet<Vector3> half_dx = new HashSet<Vector3>();

        for (int i = 0; i < how_many; i++) {
            half_sx.Add(new Vector3(UnityEngine.Random.Range(0.0f, width), UnityEngine.Random.Range(-height, height), 0.0f));
        }

        // Simmetric
        foreach (var item in half_sx) {
            half_dx.Add(new Vector3(-item.x, item.y, 0.0f));
        }

        List<Vector3> result = new List<Vector3>();
        result.AddRange(half_sx);
        result.AddRange(half_dx);

        return result;
    }
}