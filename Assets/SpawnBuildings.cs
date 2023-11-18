using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBuildings : MonoBehaviour
{
    public Transform transform;
    public float offscreen = -35.0f;
    public GameObject buildingPrefab;
    private GameObject[] buildings = new GameObject[5];

    void Start()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = Instantiate(buildingPrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-10, 10), 0), Quaternion.identity);
        }
    }

    void Update()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if(buildings[i].transform.position.x < offscreen) {
                Destroy(buildings[i]);
                buildings[i] = Instantiate(buildingPrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-10, 10), 0), Quaternion.identity);
            }
        }
    }
}
