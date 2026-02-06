using UnityEngine;

public class chickenInteractable : MonoBehaviour
{
    public float moveSpeed = 3f;
    private float totalTimer = 30f; // Stops moving after 30 seconds
    private float changeDirTimer = 2f;
    private Vector3 currentDirection;

    void Update()
    {
        if (totalTimer > 5)
        {
            totalTimer -= Time.deltaTime;
            changeDirTimer -= Time.deltaTime;

            // Pick a new random direction every 2 seconds
            if (changeDirTimer <= 6)
            {
                currentDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
                changeDirTimer = 2f; 
            }

            // Move the chicken automatically
            transform.position += currentDirection * moveSpeed * Time.deltaTime;
        }
    }
}