using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetsPool : MonoBehaviour {
    
    [SerializeField]
    private GameObject coinContainer = null;
    [SerializeField]
    private GameObject explosionContainer = null;

    private int[] poolArrayMaxValues;
    private Queue<GameObject>[] poolArray;

    [SerializeField]
    private bool loaded = false;

    public bool Loaded {
        get {
            return loaded;
        }
    }

    void Start() {
        poolArrayMaxValues = new int[(int)eObjectType.NUM_OBJECT_TYPE];
        poolArray = new Queue<GameObject>[(int)eObjectType.NUM_OBJECT_TYPE];
        StartCoroutine(LoadPrefabs());
    }

    private void populateQueue(eObjectType type, GameObject prefabObject) {
        for (int i = 0; i < poolArrayMaxValues[(int)type]; i++) {
            GameObject newObject = null;
            if (i > 0) {
                newObject = Instantiate(prefabObject);
                newObject.transform.parent = prefabObject.transform.parent;
            } else {
                newObject = prefabObject;
            }
            newObject.SetActive(false);
            poolArray[(int)type].Enqueue(newObject);
        }
    }

    private IEnumerator LoadPrefabs() {
        // Setup maxValues
        poolArrayMaxValues[(int)eObjectType.COIN_SX] = 64;
        poolArrayMaxValues[(int)eObjectType.COIN_DX] = 64;
        poolArrayMaxValues[(int)eObjectType.EXPLOSION] = 32;

        // Setup queues
        poolArray[(int)eObjectType.COIN_SX] = new Queue<GameObject>();
        poolArray[(int)eObjectType.COIN_DX] = new Queue<GameObject>();
        poolArray[(int)eObjectType.EXPLOSION] = new Queue<GameObject>();

        // Populating queues with pooled objects
        populateQueue(eObjectType.COIN_SX, AssetsFactory.Instantiate(eObjectType.COIN_SX, Vector3.zero, Quaternion.identity, coinContainer.transform));
        yield return null;
        populateQueue(eObjectType.COIN_DX, AssetsFactory.Instantiate(eObjectType.COIN_DX, Vector3.zero, Quaternion.identity, coinContainer.transform));
        yield return null;
        populateQueue(eObjectType.EXPLOSION, AssetsFactory.Instantiate(eObjectType.EXPLOSION, Vector3.zero, Quaternion.identity, explosionContainer.transform));
        yield return null;

        loaded = true;
    }

    public bool GetFreeObjectFromPool(eObjectType objectType, out GameObject pooledObject) {
        pooledObject = null;

        if (poolArray[(int)objectType].Count > 0) {
            pooledObject = poolArray[(int)objectType].Dequeue();
            if (pooledObject) {
                pooledObject.transform.position = Vector3.zero;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.SetActive(true);
            }
            return true;
        }

        return false;
    }

    public void FreeObjectPool(eObjectType objectType, GameObject pooledObject) {
        poolArray[(int)objectType].Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }

}
