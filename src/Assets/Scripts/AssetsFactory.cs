using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eObjectType {
    COIN = 0,
    EXPLOSION,
    NUM_OBJECT_TYPE
}

public class AssetsFactory {

    private static AssetsFactory instance = null;
    private string[] prefabPaths;
    private GameObject[] prefabs;

    private AssetsFactory() {
        prefabPaths = new string[(int)eObjectType.NUM_OBJECT_TYPE];
        prefabs = new GameObject[(int)eObjectType.NUM_OBJECT_TYPE];

        for (int i = 0; i < (int)eObjectType.NUM_OBJECT_TYPE; i++) {
            prefabs[i] = null;
        }

        prefabPaths[(int)eObjectType.COIN] = "Prefabs/Coin";
        prefabPaths[(int)eObjectType.EXPLOSION] = "Prefabs/Explosion";
    }

    public static AssetsFactory Instance {
        get {
            if (instance == null) {
                instance = new AssetsFactory();
            }
            return instance;
        }
    }

    public static GameObject InstantiatePrefab(string name, string prefabPath, Vector3 position, Quaternion rotation, Vector3 scaling, Transform parent = null) {
        GameObject prefab = Resources.Load(prefabPath) as GameObject;
        return InstantiatePrefab(name, prefab, position, rotation, scaling, parent);
    }

    public static GameObject InstantiatePrefab(string name, GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scaling, Transform parent = null) {
        GameObject gameObject = GameObject.Instantiate(prefab, position, rotation) as GameObject;
        gameObject.name = name;
        if (parent) {
            gameObject.transform.SetParent(parent);
        }
        return gameObject;
    }

    public static GameObject Instantiate(eObjectType type, Vector3 position, Quaternion rotation, Transform parent = null) {
        if (!Instance.prefabs[(int)type]) {
            Instance.prefabs[(int)type] = Resources.Load(Instance.prefabPaths[(int)type]) as GameObject;
        }

        return InstantiatePrefab(type.ToString(), Instance.prefabs[(int)type], position, rotation, Vector3.one, parent);
    }

}
