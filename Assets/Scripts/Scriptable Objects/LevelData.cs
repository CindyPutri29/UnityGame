using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Object/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("SAVE SETTING")]
    public string saveKey;
    public bool defaultUnlocked;

    [Header("ENEMY SETTING")]
    public int totalEnemyToSpawn;
    public float enemySpawnRadius;
    public GameObject[] enemies;
} 
