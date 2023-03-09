using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  float moveSpeed = 1f;
  Rigidbody2D rb;
  BoxCollider2D bodyCollider;
  CapsuleCollider2D edgeCollider;
  Collider2D coll;
  Animator animator;
  SoundManager SoundManager;

  [SerializeField] AudioClip deathSFX;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<Collider2D>();
    bodyCollider = GetComponent<BoxCollider2D>();
    edgeCollider = GetComponent<CapsuleCollider2D>();
    animator = GetComponent<Animator>();
    AudioSource SFXSource = GetComponent<AudioSource>();
    SoundManager = FindObjectOfType<SoundManager>();
  }

  // Update is called once per frame
  void Update()
  {

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
      Die();
    }
  }

  void FlipSprite()
  {
    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
  }

  public void Die()
  {
    moveSpeed = 0f;
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
