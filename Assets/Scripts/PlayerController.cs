using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    Rigidbody2D rb;
    Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D, Left/Right
        float v = Input.GetAxisRaw("Vertical");   // W/S, Up/Down
        moveInput = new Vector2(h, v).normalized;

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
            moveInput = dir;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        ConstrainToScreen();
    }

    void ConstrainToScreen()
    {
        Vector3 pos = transform.position;
        Vector3 min = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 max = Camera.main.ViewportToWorldPoint(Vector3.one);
        float halfWidth = 0.2f * 0.5f;
        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + halfWidth, max.y - halfWidth);
        transform.position = pos;
    }
}