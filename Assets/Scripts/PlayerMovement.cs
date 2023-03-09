using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
  Vector2 MovementInput;
  Rigidbody2D rb;
  CapsuleCollider2D BodyCollider;
  Animator animator;

  public BoxCollider2D FeetCollider;
  public Arrow arrowPrefab;
  public Transform firePosition;


  [SerializeField] AudioClip jumpSFX;
  [SerializeField] AudioClip deathSFX;
  [SerializeField] AudioClip fireSFX;
  [SerializeField] AudioClip LandSFX;
  [SerializeField] AudioClip CoinSFX;
  [SerializeField] AudioClip RollFX;

  [SerializeField] AudioSource AudioSourceBG;
  [SerializeField] AudioSource AudioSourceMain;

  float gravityAtStart;
  bool isAlive = true;

  float runSpeed;
  float climbSpeed;
  float jumpSpeed;

  int pickupPoints = 100;
  float defaultRunSpeed = 7f;
  float defaultClimbSpeed = 5f;
  float defaultJumpSpeed = 12f;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    BodyCollider = GetComponent<CapsuleCollider2D>();
    gravityAtStart = rb.gravityScale;
    animator = GetComponent<Animator>();

    runSpeed = defaultRunSpeed;
    climbSpeed = defaultClimbSpeed;
    jumpSpeed = defaultJumpSpeed;
  }

  void FixedUpdate()
  {
    if (!isAlive) { return; }
    ClimbLadder();
    Run();
    FlipSprite();
  }

  void Update()
  {

  }


  void Run()
  {
    rb.velocity = new Vector2(MovementInput.x * runSpeed, rb.velocity.y);
    animator.SetBool("isRunning", IsRunning());
  }

  void FlipSprite()
  {
    if (IsRunning())
    {
      transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }
  }

  bool IsRunning()
  {
    return (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon);
  }

  void OnMove(InputValue value)
  {
    if (!isAlive) { return; }

    MovementInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!isAlive) { return; }

    if (value.isPressed)
    {
      if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
      {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        AudioSourceMain.PlayOneShot(jumpSFX, 0.5f);
      }
    }
  }

  void OnRoll(InputValue value)
  {
    if (!isAlive) { return; }

    if (value.isPressed)
    {
      if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
      {
        animator.SetTrigger("Roll");
        AudioSourceMain.PlayOneShot(RollFX, 0.3f);
      }
    }
  }

  void ClimbLadder()
  {
    if (BodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    {
      rb.gravityScale = 0f;
      rb.velocity = new Vector2(rb.velocity.y, MovementInput.y * climbSpeed);
      animator.SetBool("isClimbing", Mathf.Abs(rb.velocity.y) > Mathf.Epsilon);
    }
    else
    {
      rb.gravityScale = gravityAtStart;
      animator.SetBool("isClimbing", false);
    }
  }

  void OnFire(InputValue value)
  {
    if (!isAlive) { return; }
    if (value.isPressed && !animator.GetBool("isClimbing"))
    {
      animator.SetTrigger("Arrow");
      AudioSourceMain.PlayOneShot(fireSFX, 0.4f);
      Instantiate(arrowPrefab, firePosition.position, transform.rotation);
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy" && isAlive) {
        isAlive = false;
        AudioSourceBG.pitch = 0.7f;
        animator.SetTrigger("Die");
        AudioSourceMain.PlayOneShot(deathSFX, 0.5f);
    }

    if (other.gameObject.tag == "Ground") {
        AudioSourceMain.PlayOneShot(LandSFX, 0.5f);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.gameObject.tag == "Coin") {
        Destroy(other.gameObject);
        FindObjectOfType<GameSession>().AddScore(pickupPoints);
        AudioSourceMain.PlayOneShot(CoinSFX, 0.4f);
    }
  }

  public void Die() {
    FindObjectOfType<GameSession>().ProcessPlayerDeath();
  }

  public void RollSpeedIncrease() {
    runSpeed = defaultRunSpeed * 1.5f;
    jumpSpeed = defaultJumpSpeed * 1.15f;
  }

  public void RollSpeedDecrease() {
    runSpeed = defaultRunSpeed;
    jumpSpeed = defaultJumpSpeed;
  }
}
