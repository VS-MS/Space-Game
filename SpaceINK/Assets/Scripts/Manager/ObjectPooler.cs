using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    //Количество объектов в пуле
    public int amountToPool;
    //Тип объекта для пула
    public GameObject objectToPool;
    //Разрешение на увеличение пула при его переполнении
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    //Делаем экземпляр статичным, для упращения доступа из вне
    public static ObjectPooler SharedInstance;

    //Список, в котором будем хранить все объекты, которые должны храниться в пуле
    public List<GameObject> pooledObjects;
    //Список, в котором будем хранить все типы объектов и их параметры
    public List<ObjectPoolItem> itemsToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            //Каждый тип вставляем в пул pooledObjects в количестве amountToPool 
            //Повторяем цикл для всех элементов списка itemsToPool
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //Перебераем пул и ищем первый объект с нужным тагом и не активный на сцене.
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            //Если не найден в пуле ни один объект с нужным тагом, добавляем новый элемент.
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
