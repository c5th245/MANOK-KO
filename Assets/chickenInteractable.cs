using UnityEngine;

public class chickenInteractable : MonoBehaviour
{
    // --- STEP 1: Define your variables here ---
    public float moveSpeed = 3f;
    private float totalTimer = 30f; 
    private float changedDirTimer = 2f;
    private Vector3 currentDirection;

    // --- STEP 2: The logic stays inside Update ---
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

            // 3. Rotate (Flip) the character based on direction
if (currentDirection.x > 0)
{
    // Facing Right - Use your X and Y scale from the Inspector
    transform.localScale = new Vector3(19.782f, 11.843f, 1);
}
else if (currentDirection.x < 0)
{
    // Facing Left (flipped) - Make only the X value negative
    transform.localScale = new Vector3(-19.782f, 11.843f, 1);
}
            }
        }
    }
