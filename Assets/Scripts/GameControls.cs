using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    public GameObject SpawnObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newObject = Instantiate(SpawnObject, (Vector2)worldPos, Quaternion.identity);
            newObject.SetActive(true);
        }   
    }
}