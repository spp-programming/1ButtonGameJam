using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PortalManager portalManager;
    public int manaCount = 0;
    private int collectedManaCount = 0;

    private int currentLevel = 1;

    void Start()
    {
        CountAllChildren();
    }

    public void CollectEvent() { 
        collectedManaCount++;
        if(collectedManaCount >= manaCount) {
            portalManager.Open();
        }
    }

    public void LevelCompleteEvent() {
        currentLevel++;
        Debug.Log("Level complete, from level manager");
    }

    void CountAllChildren()
    {
        GameObject[] childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
            if(childObjects[i].layer == 6) {
                manaCount++;
            }
        }
    }
}