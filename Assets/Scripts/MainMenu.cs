using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject LevelSelectionScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (TitleScreen.activeInHierarchy)
            {
                if (LevelManager.Instance.UnlockedLevels > 1)
                {
                    TitleScreen.SetActive(false);
                    LevelSelectionScreen.SetActive(true);
                }
                else if (LevelManager.Instance.UnlockedLevels == 1)
                {
                    LevelManager.Instance.LoadNextLevel(1);
                }
            }

        }
    }
}
