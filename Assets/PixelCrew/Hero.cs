using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerCheck _groundCheck;

    // Private methods
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var isJumping = _direction.y > 0;

        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        if (isJumping)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        // GroundCheck method
        // Return true if object 'hero' have 'bottom' collision with layers from _groundCheck._groundLayer
        // overwise return false. See LayerCheck class

        return _groundCheck.IsTouchingLayer();
    }

    // Public methods
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void BattleRoar()
    {
        Debug.Log("ROOOOARRR!!!");
    }

}
