using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    // 1 second = 50 timer ticks
    public int Timer;
    public int ExplosionPower;
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
                
                float magnitude = (float)Math.Clamp(ExplosionPower - 0.1 * Math.Pow(Math.Sqrt(direction.x * direction.x + direction.y * direction.y), 2.0), 0.0, ExplosionPower);

                direction.Normalize();

                Rigidbody2D objectRB = obj.GetComponent<Rigidbody2D>();
                if (objectRB != null)
                {
                    objectRB.velocity += direction * magnitude;
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