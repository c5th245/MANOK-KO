using UnityEngine;

public class chickenInteractable : MonoBehaviour
{
    public float moveSpeed = 3f;
    private float totalTimer = 30f;
    private float changedDirTimer = 2f;
    private Vector3 currentDirection;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (totalTimer > 5)
        {
            totalTimer -= Time.deltaTime;
            changedDirTimer -= Time.deltaTime;

            if (changedDirTimer <= 0)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                currentDirection = new Vector3(randomDir.x, randomDir.y, 0);
                changedDirTimer = 2f;
            }

            transform.position += currentDirection * moveSpeed * Time.deltaTime;

            if (currentDirection.x > 0)
            {
                spriteRenderer.flipX = false; 
            }
            else if (currentDirection.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}