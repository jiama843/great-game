using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TreasureLevel : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] coins;

    public Text goalText;

    private string[] requiredItems;
    private GameObject[] collectedItems;
    private List<GameObject> itemList = new List<GameObject>();
    private int requiredCoins;
    private int collectedCoins;

    // Start is called before the first frame update
    void Start()
    {
        //set goal for the win
        SetTargetItems();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTargetItems()
    {
        
        //0-3 items
        requiredItems = items.OrderBy(x => Random.value).Take(Random.Range(0, items.Length)).Select(item => item.name).ToArray();
        //0-4 coins
        requiredCoins = Random.Range(0, coins.Length);

        //set text
        goalText.text = "Required items:\n" + string.Join(", ", requiredItems) + "\nRequired coins: " + requiredCoins;

    }

    public void GameCheck()
    {
        //check if items match
        //count coins
        collectedCoins = 0;
        foreach (GameObject c in coins)
        {
            TrasureStock treasure = c.GetComponent<TrasureStock>();

            if (treasure.isInChest)
            {
                collectedCoins++;
            }
        }
        //
        collectedItems = null;
        foreach (GameObject i in items)
        {
            TrasureStock treasure = i.GetComponent<TrasureStock>();

            if (treasure.isInChest)
            {
                itemList.Add(i);
            }
        }

        collectedItems = itemList.ToArray();
        bool itemsMatch = requiredItems.All(item => collectedItems.Any(collectedItem => collectedItem.name == item));

        // Check if collected coins are sufficient
        bool coinsMatch = collectedCoins >= requiredCoins;

        if (itemsMatch && coinsMatch)
        {
            Debug.Log("Congratulations! You win!");

            int keys = PlayerPrefs.GetInt("KEYS", 0);
            PlayerPrefs.SetInt("KEYS", keys--);
            
        }
        else
        {
            Debug.Log("Goal is yet to be reached");
        }
    }
}
