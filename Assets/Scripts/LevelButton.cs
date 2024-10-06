using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string levelSceneName;
    public GameObject playerManager;
    public LevelData levelData;
    Button selfButton;
    bool unlocked;

    private void Start()
    {
        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(LoadLevel);
        LoadLevelData();
    }

    void LoadLevel()
    {
        StartCoroutine(IELoadSceneAsync());
        IEnumerator IELoadSceneAsync()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(levelSceneName, LoadSceneMode.Additive);
            while (!async.isDone)
            {
                yield return null;
            }
            var loadedLevelScene = SceneManager.GetSceneByName(levelSceneName);
            Transform loadedLevelTransform = loadedLevelScene.GetRootGameObjects()[0].transform;
            GameObject playerManagerClone = Instantiate(playerManager, loadedLevelTransform);
            playerManagerClone.transform.SetParent(null);
            GameplayManager.instance.totalEnemyToSpawn = levelData.totalEnemyToSpawn;
            GameplayManager.instance.enemySpawnRadius = levelData.enemySpawnRadius;
            GameplayManager.instance.enemies = levelData.enemies;
            SceneManager.UnloadSceneAsync("Main Menu");
            GameplayManager.instance.nextLevelKey = GetNextLevelKey();
        }
    }

    void LoadLevelData()
    {
        if (PlayerPrefs.HasKey(levelData.saveKey))
        {
            unlocked = IntToBool(PlayerPrefs.GetInt(levelData.saveKey));
        }
        else
        {
            unlocked = levelData.defaultUnlocked;
        }
        selfButton.interactable = unlocked;
    }


    bool IntToBool(int value)
    {
        if (value > 0)
        {
            return true;
        }
        else if (value <= 0)
        {
            return false;
        }
        return false;
    }
    string GetNextLevelKey()
    {
        LevelButton nextLevelButton = transform.parent.GetChild(transform.GetSiblingIndex() + 1)
            .GetComponent<LevelButton>();
        if (nextLevelButton != null)
        {
            return nextLevelButton.levelData.saveKey;
        }
        else
        {
            return "";
        }
    }
}
