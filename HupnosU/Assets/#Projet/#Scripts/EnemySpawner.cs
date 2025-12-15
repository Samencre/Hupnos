using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject nightmarePrefab; 
    public Tilemap groundTilemap; 
    public Transform player; 
    public int maxEnemies = 64;
    public float spawnInterval = 2f;
    public CandleManager candleManager;
    public int currentEnemies = 0;
    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        GenerateSpawnPositions();
        InvokeRepeating(nameof(TrySpawnEnemy), 1f, spawnInterval);
    }

    void GenerateSpawnPositions()
    {
        spawnPositions.Clear();
        BoundsInt bounds = groundTilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (!groundTilemap.HasTile(pos)) continue;
            Vector3 worldPos = groundTilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0);
            spawnPositions.Add(worldPos);
        }
    }

    void TrySpawnEnemy()
    {
        if (currentEnemies >= maxEnemies || spawnPositions.Count == 0 || nightmarePrefab == null) return;
        int lightedCandles = candleManager.GetLitCount();
        int allowedEnemies = Mathf.Clamp(1 + lightedCandles, 8, maxEnemies);
        if (currentEnemies >= allowedEnemies) return;
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GameObject enemy = Instantiate(nightmarePrefab, spawnPos, Quaternion.identity);
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null && player != null)
            ai.target = player;
        currentEnemies++;
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
            health.OnDeath += () => currentEnemies--;
    }
}


