using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Animator transition;

    public PortalManager portalManager;
    public int manaCount = 0;
    public int manaLayer;
    public int enemyLayer;
    private int collectedManaCount = 0;
    public GameObject mana;
    [HideInInspector] public int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        CountAllChildren();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
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
            if(childObjects[i].layer == manaLayer || childObjects[i].layer == enemyLayer) {
                manaCount++;
            }
        }
    }

    public void CreateMana(Vector3 pos) {
        Instantiate(mana, pos, quaternion.identity, transform);
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    private IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(index);
    }
}