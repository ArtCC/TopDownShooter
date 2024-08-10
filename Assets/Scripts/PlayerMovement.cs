using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;
    private bool isFacingRight = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (GameManager.Instance.IsPlayerDead) {
            input = Vector2.zero;
            return;
        }

        ProcessInputs();
        Flip();

        animator.SetFloat("Speed", input.magnitude);
    }

    private void FixedUpdate() {
        rb.velocity = input * speed;
    }

    private void ProcessInputs() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        input = new Vector2(xInput, yInput).normalized;
    }

    private void Flip() {
        if ((isFacingRight && input.x < 0f) || (!isFacingRight && input.x > 0f)) {
            Vector3 scale = transform.localScale;

            scale.x *= -1f;

            transform.localScale = scale;
            
            isFacingRight = !isFacingRight;
        }
    }
}