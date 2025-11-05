using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public bool stopped = false;
    Transform player;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (stopped || player == null) return;

        Vector2 dirAway = ((Vector2)transform.position - (Vector2)player.position).normalized;
        if (dirAway == Vector2.zero)
        {
            dirAway = Random.insideUnitCircle.normalized;
        }
        Vector3 newPos = transform.position + (Vector3)(dirAway * speed * Time.deltaTime);
        Vector3 worldMin = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 worldMax = Camera.main.ViewportToWorldPoint(Vector3.one);
        float margin = 0.1f;
        newPos.x = Mathf.Clamp(newPos.x, worldMin.x + margin, worldMax.x - margin);
        newPos.y = Mathf.Clamp(newPos.y, worldMin.y + margin, worldMax.y - margin);

        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            stopped = true;
        }
    }
}