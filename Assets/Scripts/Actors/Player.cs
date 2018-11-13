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
    }
}