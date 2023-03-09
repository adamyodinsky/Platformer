using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] Line[] lines;
    Collider2D flagCollider;
    private readonly object _lock = new object();
    
    void start()
    {
        flagCollider = GetComponent<Collider2D>();
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player" && other is CapsuleCollider2D)
        {
            lock(_lock) {
                Dialog dialog = FindObjectOfType<Dialog>();
                dialog.dialogLines = lines;
                dialog.startDialog();
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
