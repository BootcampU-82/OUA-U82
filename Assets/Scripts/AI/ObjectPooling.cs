using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Pool[] pools;

    [System.Serializable]
    public class Pool
    {
        public Queue<GameObject> objectInPool;
        public int poolSize;
        public GameObject objectPrefab;
    }

    public static ObjectPooling Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].objectInPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab,spawnPoint.transform);

                obj.SetActive(false);

                pools[i].objectInPool.Enqueue(obj);
            }
        }
    }

    public GameObject GetInPool(int type,Vector3 pos)
    {
        if (type> pools.Length)
        {
            return null;
        }
        GameObject obj = pools[type].objectInPool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        pools[type].objectInPool.Enqueue(obj);
        return obj;
    }

    public void AddPool(GameObject obj,int type)
    {
        obj.SetActive(false);
        obj.transform.position = spawnPoint.transform.position;
        pools[type].objectInPool.Enqueue(obj);
    }
}

