using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Player identifier enum
    /// </summary>
    public enum PlayerType
    {
        Left,
        Right
    }

    #region Editor exposed members
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private float _movementSpeed = 5;
    #endregion

    #region Private members
    private Transform _transform;
    private float _halfHeight;
    #endregion

    private void Start()
    {
        // Store highly used variables in advance for performance
        _transform = transform;
        _halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        // TODO: Get movement input (Make sure left/right player)
        // TODO: Move player
        // TODO: Make sure player doesn't leave screen bounds (ScreenUtil.ScreenPhysicalBounds will help you out)
        if (_playerType == PlayerType.Left)
        {
            if (Input.GetKey("w"))
            {
                if (_transform.position.y + _halfHeight <= ScreenUtil.ScreenPhysicalBounds.yMax)
                {
                    _transform.position += Vector3.up * _movementSpeed * Time.deltaTime;
                }
            }
            if (Input.GetKey("s"))
            {
                if(_transform.position.y - _halfHeight >= ScreenUtil.ScreenPhysicalBounds.yMin)
                {
                    _transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
                }
            }
        }
        if (_playerType == PlayerType.Right)
        {
            if (Input.GetKey("up"))
            {
                if (_transform.position.y + _halfHeight <= ScreenUtil.ScreenPhysicalBounds.yMax)
                {
                    _transform.position += Vector3.up * _movementSpeed * Time.deltaTime;
                }
            }
            if (Input.GetKey("down"))
            {
                if (_transform.position.y - _halfHeight >= ScreenUtil.ScreenPhysicalBounds.yMin)
                {
                    _transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
                }
            }
        }
    }
}