using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimer;
    [Header("Spawn Area")]
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float safeZoneMargin = 2f;
    private GameObject[] enemiesAlive;

    private void Update()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemiesAlive.Length);

        spawnTimer -= Time.deltaTime;

        if (enemiesAlive.Length < 3 && spawnTimer <= 0)
        {
            SpawnEnemy(ChooseEnemy());
        }
    }

    private GameObject ChooseEnemy()
    {
        int randomValue = Random.Range(1, 10);
        if (randomValue % 2 == 0)
        {
            return skeletonPrefab;
        }
        else
        {
            return demonPrefab;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Vector2 spawnPosition;
        do
        {
            // Generate a random position within the spawn area
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector2(randomX, randomY);
        } while (IsInPlayerView(spawnPosition));

        // Instantiate the enemy
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        spawnTimer = spawnInterval;

    }
    private bool IsInPlayerView(Vector2 position)
    {
        // Get the bounds of the player's camera
        Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Check if the position is within the camera's visible area (with margin)
        if (position.x > screenMin.x - safeZoneMargin && position.x < screenMax.x + safeZoneMargin &&
            position.y > screenMin.y - safeZoneMargin && position.y < screenMax.y + safeZoneMargin)
        {
            return true; // Position is inside the player's view
        }

        return false; // Position is safe to spawn
    }

    private void OnDrawGizmos()
    {
        // Visualize the spawn area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(spawnAreaMin.x, spawnAreaMin.y), new Vector2(spawnAreaMax.x, spawnAreaMin.y));
        Gizmos.DrawLine(new Vector2(spawnAreaMax.x, spawnAreaMin.y), new Vector2(spawnAreaMax.x, spawnAreaMax.y));
        Gizmos.DrawLine(new Vector2(spawnAreaMax.x, spawnAreaMax.y), new Vector2(spawnAreaMin.x, spawnAreaMax.y));
        Gizmos.DrawLine(new Vector2(spawnAreaMin.x, spawnAreaMax.y), new Vector2(spawnAreaMin.x, spawnAreaMin.y));
    }
}
