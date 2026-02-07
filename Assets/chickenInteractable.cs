using UnityEngine;

public class chickenInteractable : MonoBehaviour
{
    public float moveSpeed = 3f;
    private float totalTimer = 30f;
    private float changedDirTimer = 2f;
    private Vector3 currentDirection;

    void Update()
    {
        // Only move and flip if the timer is above 5
        if (totalTimer > 5)
        {
            totalTimer -= Time.deltaTime;
            changedDirTimer -= Time.deltaTime;

            // Handle direction changes
            if (changedDirTimer <= 0)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                currentDirection = new Vector3(randomDir.x, randomDir.y, 0);
                changedDirTimer = 2f;
            }

            // Apply movement
            transform.position += currentDirection * moveSpeed * Time.deltaTime;

            // Handle sprite flipping based on movement direction
            if (currentDirection.x > 0)
            {
                transform.localScale = new Vector3(19.782f, 11.843f, 1);
            }
            else if (currentDirection.x < 0)
            {
                transform.localScale = new Vector3(-19.782f, 11.843f, 1);
            }
        }
    }
}