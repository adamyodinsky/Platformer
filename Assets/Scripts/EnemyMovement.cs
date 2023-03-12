using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 1f;
  [SerializeField] int killingPoints = 100;
  [SerializeField] int lifePoints = 2;

  Rigidbody2D rb;
  Collider2D coll;
  Animator animator;
  SoundManager SoundManager;
  GameSession gameSession;

  [SerializeField] BoxCollider2D bodyCollider;
  [SerializeField] CapsuleCollider2D edgeCollider;
  [SerializeField] AudioClip deathSFX;
  [SerializeField] AudioClip hitSFX;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<Collider2D>();
    animator = GetComponent<Animator>();
    SoundManager = FindObjectOfType<SoundManager>();
    gameSession = FindObjectOfType<GameSession>();
  }

  void FixedUpdate()
  {
    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
    {
      FlipSprite();
      moveSpeed *= -1;
    }

    if (other.gameObject.tag == "Arrow")
    {
        lifePoints--;
        SoundManager.PlaySound(hitSFX, 0.4f);
        gameSession.AddScore(killingPoints);
        if (lifePoints <= 0)
        {
          Die();
        }
    }
  }

  void FlipSprite()
  {
    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
  }

  public void Die()
  {
    moveSpeed = 0f;
    gameSession.AddScore(killingPoints);
    SoundManager.PlaySound(deathSFX, 0.5f);
    animator.SetTrigger("Die");
    transform.localScale = new Vector2(1, -1);
    rb.velocity = new Vector2(0, 5);
    coll.isTrigger = true;
  }

  public void SelfDestruct()
  {
    Destroy(gameObject);
  }

  void OnTriggerExit2D(Collider2D other)
  {
    FlipSprite();
    moveSpeed *= -1;
  }
}
