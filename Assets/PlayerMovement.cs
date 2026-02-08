using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Vector2 lastDirection = Vector2.down;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isRunning = false;

    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveLeft = false;
    private bool moveRight = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (upButton != null)
        {
            EventTrigger upTrigger = upButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(upTrigger, EventTriggerType.PointerDown, () => moveUp = true);
            AddEventTrigger(upTrigger, EventTriggerType.PointerUp, () => moveUp = false);
        }

        if (downButton != null)
        {
            EventTrigger downTrigger = downButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(downTrigger, EventTriggerType.PointerDown, () => moveDown = true);
            AddEventTrigger(downTrigger, EventTriggerType.PointerUp, () => moveDown = false);
        }

        if (leftButton != null)
        {
            EventTrigger leftTrigger = leftButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(leftTrigger, EventTriggerType.PointerDown, () => moveLeft = true);
            AddEventTrigger(leftTrigger, EventTriggerType.PointerUp, () => moveLeft = false);
        }

        if (rightButton != null)
        {
            EventTrigger rightTrigger = rightButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(rightTrigger, EventTriggerType.PointerDown, () => moveRight = true);
            AddEventTrigger(rightTrigger, EventTriggerType.PointerUp, () => moveRight = false);
        }
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                vertical = 1f;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                vertical = -1f;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;
        }

        if (moveLeft) horizontal = -1f;
        if (moveRight) horizontal = 1f;
        if (moveUp) vertical = 1f;
        if (moveDown) vertical = -1f;

        moveDirection = new Vector2(horizontal, vertical).normalized;

        // Check if character is running
        isRunning = moveDirection != Vector2.zero;

        // Store the last direction when moving
        if (isRunning)
        {
            lastDirection = moveDirection;
        }

        if (animator != null)
        {
            // Update animator with running state
            animator.SetBool("isRunning", isRunning);

            if (isRunning)
            {
                // When running, set movement direction
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
            }
            else
            {
                // When idle, keep last direction for proper idle pose
                animator.SetFloat("Horizontal", lastDirection.x);
                animator.SetFloat("Vertical", lastDirection.y);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType triggerType, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => callback());
        trigger.triggers.Add(entry);
    }
}