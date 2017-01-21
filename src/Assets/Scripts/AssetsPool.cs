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
        poolArrayMaxValues[(int)eObjectType.COIN] = 10;
        poolArrayMaxValues[(int)eObjectType.EXPLOSION] = 10;

        // Setup queues
        poolArray[(int)eObjectType.COIN] = new Queue<GameObject>();
        poolArray[(int)eObjectType.EXPLOSION] = new Queue<GameObject>();

        // Populating queues with pooled objects
        populateQueue(eObjectType.COIN, AssetsFactory.Instantiate(eObjectType.COIN, Vector3.zero, Quaternion.identity, coinContainer.transform));
        yield return null;
        populateQueue(eObjectType.EXPLOSION, AssetsFactory.Instantiate(eObjectType.EXPLOSION, Vector3.zero, Quaternion.identity, explosionContainer.transform));
        yield return null;
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
