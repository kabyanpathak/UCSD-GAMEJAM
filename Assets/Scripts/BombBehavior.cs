using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    // 1 second = 50 timer ticks
    public int Timer;
    public float Power;
    public float Range;
    public Sprite ExplosionSprite;
    
    private GameObject explosionObject;

    void FixedUpdate()
    {
        if (Timer-- == 0)
        {
            explosionObject = new GameObject("Explosion");
            explosionObject.transform.position = gameObject.transform.position;

            SpriteRenderer explosionSpriteRenderer = explosionObject.AddComponent<SpriteRenderer>();
            explosionSpriteRenderer.sprite = ExplosionSprite;
            explosionSpriteRenderer.sortingOrder = 1;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyObjects());
            
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objects)
            {
                Vector2 direction = new Vector2(obj.transform.position.x - gameObject.transform.position.x,
                                                obj.transform.position.y - gameObject.transform.position.y);
                
                float distance = (float)Math.Sqrt(direction.x * direction.x + direction.y * direction.y);

                direction.Normalize();

                Rigidbody2D objectRB = obj.GetComponent<Rigidbody2D>();
                if (objectRB != null && distance < Range)
                {
                    objectRB.velocity += direction * Power;
                }
            }
        }
    }

    IEnumerator DestroyObjects()
    {
        yield return new WaitForSeconds(1);

        Destroy(explosionObject);
        Destroy(gameObject);
    }
}