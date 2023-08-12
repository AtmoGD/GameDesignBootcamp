using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Character : MonoBehaviour
{
    [Header("Character Settings")]
    public float maxHealth = 100f;
    public float speed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public float jumpCooldown = 0.3f;
    public float gravityDelta = 0.1f;
    public float gravity = 20f;
    public float rotationSpeed = 5f;
    public float interactRange = 2f;

    [Header("References")]
    private CharacterController controller;
    private Animator animator;

    [Header("Live Data (Don't Modify)")]
    [SerializeField] private float health = 100f;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private int jumps = 0;
    [SerializeField] private float lastJump = 0f;

    private bool IsDead { get { return health <= 0; } }
    private bool CanJump { get { return (Time.time - lastJump > jumpCooldown) && jumps < maxJumps; } }
    public int Coins { get; private set; } = 0;
    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Revive();
    }

    private void Update()
    {
        if (IsDead) return;

        if (controller.isGrounded && moveDirection.y < 0)
            jumps = 0;

        moveDirection.y = Mathf.Clamp(moveDirection.y - (gravityDelta * Time.deltaTime), -gravity, float.MaxValue);

        controller.Move(moveDirection);

        Rotate(new Vector3(moveDirection.x, 0, moveDirection.z));

        animator.SetBool("IsRunning", new Vector2(moveDirection.x, moveDirection.z).magnitude > speed * 0.1f);
    }

    public void Move(Vector2 direction)
    {
        if (IsDead) return;

        moveDirection = new Vector3(direction.x, 0, direction.y) * speed;
    }

    public void Jump()
    {
        if (CanJump && !IsDead)
        {
            moveDirection.y = jumpForce;
            jumps++;
            animator.SetTrigger("Jump");
            lastJump = Time.time;
        }
    }

    public void Rotate(Vector3 direction)
    {
        if (IsDead) return;

        if (direction.normalized != Vector3.zero)
            transform.forward = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        health -= damage;

        if (health <= 0) Die();
    }

    private void Die()
    {
        if (IsDead) return;

        controller.Move(Vector3.zero);
        animator.SetTrigger("Die");
    }

    private void Revive()
    {
        health = maxHealth;
        animator.SetTrigger("Revive");
    }

    public void AddCoin(int amount)
    {
        Coins += amount;
    }

    public void OnInteractPressed(InputAction.CallbackContext _callbackContext)
    {
        if (_callbackContext.started)
            Interact();
    }

    private void Interact()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in interactables)
        {
            Interactable interactable = collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact(this);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
