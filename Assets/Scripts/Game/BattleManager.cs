using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefabs;
    GameObject selectedMonster;
    MonsterController monsterController;

    void Start()
    {
        GameObject prefabMonster = monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Length)];

        // Store the instance of the prefab! monsterPrefabs[#] is a reference to a prefab!
        selectedMonster = Instantiate(prefabMonster, new Vector3(0, 0, 0), Quaternion.identity);

        monsterController = selectedMonster.GetComponent<MonsterController>();

    }

    public void HandlePlayerClick(string attackType)
    {
        Debug.Log("Monster weakness" + monsterController.GetAffinitiy() + " Given: " + attackType);

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
