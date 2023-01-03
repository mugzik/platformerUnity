using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class StaticMovementComoponent : MonoBehaviour
    {

        enum MovementType
        {
            Horizontal,
            Vertical,
            //Mixed
        };

        [SerializeField] private MovementType _movementType;
        [SerializeField] private float _speed;
        [SerializeField] private float _length;
        [SerializeField] private bool _isCycled;
        [SerializeField] private bool _isMovingEnabled;
        //May be list
        [SerializeField] private string _movableTag;

        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private bool _isReversed;

        public void Awake()
        {
            _isReversed = false;
            _startPoint = transform.position;
            _endPoint = getEndPoint(_startPoint);
        }

        public void Update()
        {
            DebugRay();

            if (_isMovingEnabled)
            {
                Move();
            }
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _movableTag)
            {
                collision.gameObject.transform.parent = transform;
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == _movableTag)
            {
                collision.gameObject.transform.parent = null;
            }
        }

        public void EnableMovement()
        {
            _isMovingEnabled = true;
        }

        public void DisableMovement()
        {
            _isMovingEnabled = false;
        }

        private void DebugRay()
        {
            Vector3 direction;
            if (_movementType == MovementType.Vertical)
            {
                direction = transform.TransformDirection(Vector3.up);
            }
            else
            {
                //MovementType.Horizontal as default
                direction = transform.TransformDirection(Vector3.right);
            }

            Debug.DrawRay(_startPoint, direction * _length * Mathf.Abs(_speed)/_speed, Color.green);
        }

        private void Move()
        {
            var oldPosition = transform.position;

            var toPosition = _isReversed ? _startPoint : _endPoint;


            if (!oldPosition.Equals(toPosition))
            {
                transform.position = Vector3.MoveTowards(oldPosition, toPosition, Mathf.Abs(_speed) * Time.deltaTime);
            }
            else if (_isCycled)
            {
                _isReversed = !_isReversed;
            }
            else
            {
                DisableMovement();
            }

        }

        private Vector3 getEndPoint(Vector3 startPoint)
        {
            // Return Vector3 position depended on startPoint, _movementType and _length
            var result = new Vector3(startPoint.x, startPoint.y, startPoint.z);

            if (_movementType == MovementType.Horizontal)
            {
                result.x += _length * Mathf.Abs(_speed) / _speed;
            }
            else if (_movementType == MovementType.Vertical)
            {
                result.y += _length * Mathf.Abs(_speed) / _speed;
            }
            
            return result;
        }

    }
}