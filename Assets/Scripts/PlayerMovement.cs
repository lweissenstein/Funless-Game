using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed = 5f;

    [Header("Idle Prefabs")]
    public GameObject gun_down;
    public GameObject gun_left;
    public GameObject gun_right;
    public GameObject gun_up;

    [Header("Walk Prefabs")]
    public GameObject gun_walk_down;
    public GameObject gun_walk_right;
    public GameObject gun_walk_left;
    public GameObject gun_walk_up;

    private GameObject current;
    private Vector2 lastDirection = Vector2.down; // letzte Bewegungsrichtung merken

    void Start()
    {
        // Start mit Idle Down
        current = gun_down;
        SetActiveChild(current);
    }

    void Update()
    {
        // --- Input ---
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMoving = move.magnitude > 0.1f;

        // Letzte Richtung nur bei Bewegung speichern
        if (isMoving)
            lastDirection = move.normalized;

        // Bestimme nächsten aktiven Prefab
        GameObject next = DetermineNextPrefab(isMoving, move, lastDirection);

        // Wechsel nur, wenn Prefab anders
        if (next != current)
        {
            SetActiveChild(next);
            current = next;
        }

        // Bewegung
        transform.Translate(move.normalized * moveSpeed * Time.deltaTime);

        ClampToCamera();
    }

    GameObject DetermineNextPrefab(bool isMoving, Vector2 move, Vector2 lastDir)
    {
        if (isMoving)
        {
            // Vertikal bewegen
            if (Mathf.Abs(move.y) > Mathf.Abs(move.x))
                return move.y > 0 ? gun_walk_up : gun_walk_down;
            else // Sidewalk
                return move.x > 0 ? gun_walk_left : gun_walk_right;
        }
        else
        {
            // Idle: letzte Richtung nutzen
            if (Mathf.Abs(lastDir.y) > Mathf.Abs(lastDir.x))
                return lastDir.y > 0 ? gun_up : gun_down;
            else
                return lastDir.x > 0 ? gun_right : gun_left;
        }
    }

    void SetActiveChild(GameObject active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(child.gameObject == active);
        }
    }

    void ClampToCamera()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 pos = transform.position;
        float offset = 0.5f; // optional Abstand zum Rand

        pos.x = Mathf.Clamp(pos.x, -width + offset, width - offset);
        pos.y = Mathf.Clamp(pos.y, -height + offset, height - offset);

        transform.position = pos;
    }
}
