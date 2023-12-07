
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    #region Movement
    private float moveDirection;
    private Vector2 currentVelocity;
    private Vector2 workspaceVelocity;
    private float moveSpeed = 2f;
    #endregion

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<float>();
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
    }

    private void FixedUpdate()
    {
        ChangeVelocityX(moveDirection);
    }

    public void ChangeVelocityX(float X)
    {
        FlipX((int)X);
        workspaceVelocity.Set(X * moveSpeed, currentVelocity.y);
        rb.velocity = workspaceVelocity;
        currentVelocity = workspaceVelocity;
    }

    private void FlipX(int X)
    {
        if (X > 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (X < 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.Lose();
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.Win();
        }
    }
}
