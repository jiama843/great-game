using System;
using System.Collections;


using UnityEngine;



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

    SwitchScene switchScene;

    void Start()
    {
        switchScene = GetComponent<SwitchScene>();
        manaController = manaManager.GetComponent<ManaController>();

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        GameObject prefabMonster = monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Length)];


        // Store the instance of the prefab! monsterPrefabs[#] is a reference to a prefab!
        monsterInstance = Instantiate(prefabMonster, new Vector3(0, 0, 0), Quaternion.identity);

        // Make monster GO an child of canvas, so it's rendered according to canvas' space (and in player's view)
        monsterInstance.transform.SetParent(canvas.transform, false);

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

            if (monsterController.GetIsDead())
            {
                StartCoroutine(GoBackToDungeon());
            }
        }
        else
        {
            monsterController.Heal();
            string droppedItemName = monsterController.GetDroppedItemName();

            if (!String.IsNullOrEmpty(droppedItemName))
            {
                int currentAmountInInventory = PlayerPrefs.GetInt(droppedItemName, 0);
                PlayerPrefs.SetInt(droppedItemName, currentAmountInInventory + 1);
                // Leaving in case we need to debug
                // Debug.Log(" current" + currentAmountInInventory);
                // Debug.Log("Update" + PlayerPrefs.GetInt(droppedItemName, 0));
            }

            StartCoroutine(GoBackToDungeon());
        }
    }

    IEnumerator GoBackToDungeon()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("going back");
        switchScene.LoadNextScene();
    }
}
