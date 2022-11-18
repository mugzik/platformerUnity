using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void BattleRoar()
    {
        Debug.Log("ROOOOARRR!!!");
    }

    private void Update()
    {
        if( _direction != new Vector2(0,0) )
        {
            var d_x = _direction.x * _speed * Time.deltaTime;
            var d_y = _direction.y * _speed * Time.deltaTime;
            var newXPos = transform.position.x + d_x;
            var newYPos = transform.position.y + d_y;

            transform.position = new Vector3( newXPos, newYPos, transform.position.z );
        }
    }
}
