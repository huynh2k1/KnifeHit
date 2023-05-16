using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector2 throwForce;
    bool isActive = true;

    private Rigidbody2D _rb2D;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        ThrowKnife();
    }
    private void ThrowKnife()
    {
        if(Input.GetMouseButtonDown(0) && isActive)
        {
            _rb2D.AddForce(throwForce, ForceMode2D.Impulse);
            _rb2D.gravityScale = 1;
            UIController.instance.DecrementDisplayedKnifeCount();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
             return;
        isActive = false;
        if(collision.gameObject.CompareTag("Log")) // Nếu dao chạm vào bia
        {
            GetComponent<ParticleSystem>().Play();

            _rb2D.velocity = new Vector2(0, 0); // Set 
            _rb2D.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);

            _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, -0.4f);
            _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, 1.2f);

            GameController.instance.OnSuccessfullKnifeHit();
        }
        else if(collision.gameObject.CompareTag("Knife"))
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, -5);
            GameController.instance.StartGameOverSequence(false); //nếu game kết thúc thì show button restart
        }
    }
}
