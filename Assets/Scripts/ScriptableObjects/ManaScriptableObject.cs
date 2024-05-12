using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaResource", menuName = "ScriptableObjects/ManaScriptableObject", order = 1)]
public class ManaScriptableObject : ScriptableObject
{
    [SerializeField] int initialMana = 10;
    public int maxMana = 10;
    public int manaAmount = 10;

    // Apparenlty to "save" values across scenes, we need this. Source:
    // https://discussions.unity.com/t/persistence-of-scriptableobjects-between-scenes/237648/3 
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;

        // Reset SO on each play. Source:
        // https://discussions.unity.com/t/how-are-you-resetting-your-scriptable-objects-between-play-tests/227242/2
        maxMana = initialMana;
        manaAmount = initialMana;
    }


    public void DecreaseMana(int amount)
    {
        manaAmount -= amount;
    }
}
