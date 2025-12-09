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

    private Vector2 facingDirection = Vector2.down;

    [Header("Camera Clamp")]
    public float bottomExtra = 0.5f;

    void Start()
    {
        current = gun_down;
        SetActiveChild(current);
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMoving = moveInput.magnitude > 0.1f;

        facingDirection = GetMouseDirection();

        GameObject next = DetermineNextPrefab(isMoving, facingDirection);

        if (next != current)
        {
            SetActiveChild(next);
            current = next;
        }

        transform.Translate(moveInput.normalized * moveSpeed * Time.deltaTime);

        ClampToCamera();
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = (worldMousePos - transform.position).normalized;
        return direction;
    }

    GameObject DetermineNextPrefab(bool isMoving, Vector2 lookDir)
    {
        bool isVertical = Mathf.Abs(lookDir.y) > Mathf.Abs(lookDir.x);

        if (isMoving)
        {
            if (isVertical)
                return lookDir.y > 0 ? gun_walk_up : gun_walk_down;
            else
                return lookDir.x > 0 ? gun_walk_left : gun_walk_right;
        }
        else
        {
            if (isVertical)
                return lookDir.y > 0 ? gun_up : gun_down;
            else
                return lookDir.x > 0 ? gun_right : gun_left;
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
        float offset = 0.5f;

        pos.x = Mathf.Clamp(pos.x, -width + offset, width - offset);
        pos.y = Mathf.Clamp(pos.y, -height + offset + bottomExtra, height - offset);

        transform.position = pos;
    }
}
