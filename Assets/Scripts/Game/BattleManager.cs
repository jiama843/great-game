using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

// The Battle "game state" is handled here:
// - Player does action on button press > Game state does monster checks and triggers anims/sprite changes
// - Updates mana if magic is cast 
public class BattleManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] Canvas canvas;


    [Header("Magik")]
    [SerializeField] GameObject manaManager;
    [SerializeField] int atackCost = 1;
    ManaController manaController;

    GameObject monsterInstance;
    MonsterController monsterController;

    void Start()
    {
        manaController = manaManager.GetComponent<ManaController>();

        GameObject prefabMonster = monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Length)];
        // Store the instance of the prefab! monsterPrefabs[#] is a reference to a prefab!
        monsterInstance = Instantiate(prefabMonster, new Vector3(100, 100, 0), Quaternion.identity);

        monsterInstance.transform.SetParent(canvas.transform, false);
        monsterInstance.transform.localScale = new Vector3(1, 1, 1);

        monsterController = monsterInstance.GetComponent<MonsterController>();

    }

    public void HandlePlayerClick(string attackType)
    {
        if (attackType != "FEED")
        {
            manaController.DecreaseMana(atackCost);
        }

        if (attackType != monsterController.GetAffinitiy())
        {
            monsterController.TakeDamage();
        }
        else
        {
            monsterController.Heal();
        }
    }
}
