using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
  float arrowSpeed = 9f;
  float xSpeed;
  
  Rigidbody2D rb;
  PlayerMovement playerMovement;
  

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    playerMovement = FindObjectOfType<PlayerMovement>();
    xSpeed = playerMovement.transform.localScale.x * arrowSpeed;
    transform.localScale = new Vector2(-playerMovement.transform.localScale.x, 1);
  }

  void FixedUpdate()
  {
    shootArrow();
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(gameObject);
  }

  void shootArrow()
  { 
    rb.velocity = new Vector2(xSpeed, 0f);
  }
}
