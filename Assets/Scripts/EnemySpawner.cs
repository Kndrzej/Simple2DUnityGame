using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int count = 1000;
    public float spawnPadding = 0.5f;
    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned on EnemySpawner.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomPointOnScreen(spawnPadding);
            GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity);
            e.transform.localScale = Vector3.one * 0.2f;
            enemies.Add(e);
            var ctrl = e.GetComponent<EnemyController>();
            if (ctrl != null) ctrl.speed = Random.Range(1.0f, 3.0f);
        }
    }

    Vector2 RandomPointOnScreen(float padding)
    {
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float x = Random.Range(min.x + padding, max.x - padding);
        float y = Random.Range(min.y + padding, max.y - padding);
        return new Vector2(x, y);
    }
}