using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer[] renderers;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Read input every frame (responsive)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        // Flip visuals only
        if (moveInput.x > 0)
            Flip(true);
        else if (moveInput.x < 0)
            Flip(false);
    }

    void FixedUpdate()
    {
        // Apply physics at fixed timestep
        rb.linearVelocity = moveInput * speed;
    }

    void Flip(bool flip)
    {
        foreach (var r in renderers)
            r.flipX = flip;
    }
}