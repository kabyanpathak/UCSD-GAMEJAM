using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject SpringObject;
    public GameObject RampObject;
    public GameObject BombObject;
    
    public Image[] itemIcons = new Image[5];
    public Image[] itemSlotBackgrounds = new Image[5];

    public Sprite itemSlotBackgroundSprite;
    public Sprite itemSlotBackgroundSpriteSelected;
    public Sprite emptySlotSprite;

    public int ItemSpawnRate;

    private List<ItemType> items = new List<ItemType>();
    private int selectedItem;

    private Dictionary<ItemType, GameObject> itemDictionary = new Dictionary<ItemType, GameObject>();

    void Start()
    {
        itemDictionary.Add(ItemType.Spring, SpringObject);
        itemDictionary.Add(ItemType.Ramp, RampObject);
        itemDictionary.Add(ItemType.Bomb, BombObject);

        StartCoroutine(AddItem());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedItem = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedItem = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedItem = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            selectedItem = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            selectedItem = 4;
        
        if (Input.GetMouseButtonDown(0) && items.Count >= selectedItem + 1)
        {
            GameObject selectedObject = itemDictionary[items[selectedItem]];
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject newObject = Instantiate(selectedObject, (Vector2)worldPos, Quaternion.identity);
            newObject.SetActive(true);

            items.RemoveAt(selectedItem);
            selectedItem = 0;
        } 

        for (int i = 0; i < 5; i++)
        {
            if (i < items.Count)
            {
                itemIcons[i].sprite = itemDictionary[items[i]].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                itemIcons[i].sprite = emptySlotSprite;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == selectedItem)
            {
                itemSlotBackgrounds[i].sprite = itemSlotBackgroundSpriteSelected;
            }
            else
            {
                itemSlotBackgrounds[i].sprite = itemSlotBackgroundSprite;
            }
        }
    }

    IEnumerator AddItem()
    {
        while (true)
        {
            System.Random random = new System.Random();
            items.Add((ItemType)random.Next(0, 3));

            yield return new WaitForSeconds(ItemSpawnRate);
        }
    }
}
