using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesTracker : MonoBehaviour
{
    GamesTracker instance;

    public static Dictionary<string, bool> itemCompletionStatus = new Dictionary<string, bool>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
   public static bool GetItemCompletionStatus(string itemID)
    {
        if (itemCompletionStatus.ContainsKey(itemID))
        {
            return itemCompletionStatus[itemID];
        }
        return false;

    }

    public static void SetItemCompletionStatus(string itemID, bool isComplete)
    {
        if (itemCompletionStatus.ContainsKey(itemID))
        {
            itemCompletionStatus[itemID] = isComplete;
        }
        else
        {
            itemCompletionStatus.Add(itemID, isComplete);
        }
    }
}
