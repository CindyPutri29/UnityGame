using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("Enemy Spawner")]
    public float enemySpawnRadius;
    public int totalEnemyToSpawn;
    public Transform enemyParent;
    public GameObject[] enemies;

    [Header("Spawn Pickup")]
    public float pickupSpawnRadius;
    public int totalPickupToSpawn;
    public Transform pickupParent;
    public GameObject[] pickups;

    [Header("Gameplay")]
    public TMP_Text tittleText;
    public Button restartButton;
    public Button nextButton;
    public GameObject gameOverPanel;
    public HealthManager playerHealthManager;

    [Header("DEBUG OUTPUT")]
    public List<HealthManager> enemiesHealthManager = new List<HealthManager>();
    public int totalEnemyAlive;
    public static GameplayManager instance;
    public string nextLevelKey;
    public GameObject playerManagerPrefab;

    private void Awake()
    {
        instance = this;
        playerManagerPrefab = Resources.Load<GameObject>("Prefabs/Player Manager");
    }

    private void Start()
    {
        SpawnEnemies();
        SpawnPickups();
        ListenButton();
    }

    private void Update()
    {
        CheckEnemyAlive();
        CheckGameOver();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < totalEnemyToSpawn; i++)
        {
            GameObject enemyClone = Instantiate(enemies[Random.Range(0, enemies.Length)], enemyParent);
            float randomX = Random.Range(-enemySpawnRadius, enemySpawnRadius);
            float randomZ = Random.Range(-enemySpawnRadius, enemySpawnRadius);
            Vector3 randomPoint = new Vector3(randomX, 0, randomZ);
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, enemySpawnRadius, NavMesh.AllAreas);
            enemyClone.transform.position = hit.position;
            enemiesHealthManager.Add(enemyClone.GetComponent<HealthManager>());
            totalEnemyAlive += 1;
        }
    }

    void SpawnPickups()
    {
        for (int i = 0; i < totalPickupToSpawn; i++)
        {
            GameObject pickupClone = Instantiate(pickups[Random.Range(0, pickups.Length)], pickupParent);
            float randomX = Random.Range(-pickupSpawnRadius, pickupSpawnRadius);
            float randomZ = Random.Range(-pickupSpawnRadius, pickupSpawnRadius);
            Vector3 randomPoint = new Vector3(randomX, 0, randomZ);
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, pickupSpawnRadius, NavMesh.AllAreas);
            pickupClone.transform.position = hit.position;
        }
    }

    void CheckGameOver()
    {
        if (playerHealthManager.CURRENTHEALTH <= 0)
        {
            gameOverPanel.SetActive(true);
            nextButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            tittleText.text = "YOU ARE DEFEATED";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (totalEnemyAlive <= 0)
        {
            gameOverPanel.SetActive(true);
            nextButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            tittleText.text = "YOU ARE WON";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void CheckEnemyAlive()
    {
        foreach (var enemyHealth in enemiesHealthManager)
        {
            if (enemyHealth.currentHealth <= 0)
            {
                totalEnemyAlive -= 1;
                enemiesHealthManager.Remove(enemyHealth);
            }
        }
    }

    void RestartLevel()
    {
        GameplayManager newGameplayManager = Instantiate(playerManagerPrefab).GetComponent<GameplayManager>();
        newGameplayManager.totalEnemyToSpawn = totalEnemyToSpawn;
        newGameplayManager.enemies = enemies;
        newGameplayManager.nextLevelKey = nextLevelKey;
        Destroy(gameObject);
    }

    void NextLevel()
    {
        if (nextLevelKey != "")
        {
            PlayerPrefs.SetInt(nextLevelKey, 1);
        }
        SceneManager.LoadScene("Main Menu");
    }

    void ListenButton()
    {
        restartButton.onClick.AddListener(RestartLevel);
        nextButton.onClick.AddListener(NextLevel);
    }

}
