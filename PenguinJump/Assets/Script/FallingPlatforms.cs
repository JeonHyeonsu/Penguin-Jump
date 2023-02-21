using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private float fallDelay = 1f;
    private float respawnDelay = 1.5f;
    private Vector2 initialPosition;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("???? : " + collision.contacts[0].normal.y);

            if(collision.contacts[0].normal.y <= -0.9f && collision.contacts[0].normal.y >= -1f) //?? ??? ???? ?? 1?? ??? ??
            {
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnDelay);
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = initialPosition;
    }
}
