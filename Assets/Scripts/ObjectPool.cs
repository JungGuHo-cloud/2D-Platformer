using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject bulletPrefab;
    public int bulletLimit = 30;

    List<GameObject> bullets;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        bullets = new List<GameObject>();
        for (int i = 0; i < bulletLimit; i++)
        {
            GameObject gameObject = Instantiate(bulletPrefab, transform);
            gameObject.SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject go in bullets)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }

        GameObject obj = Instantiate(bulletPrefab, transform);
        bullets.Add(obj);

        return obj;
    }
}
