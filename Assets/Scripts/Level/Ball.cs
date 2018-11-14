using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Editor exposed members
    [SerializeField] private float _minVelocity = 5;
    #endregion

    #region Events
    public event Action<EndZone.EndZoneType> EnteredEndZone;
    #endregion

    #region Public members
    public Vector3 direction;
    public float velocity;
    #endregion

    #region Private members
    private enum Direction
    {
        Left,
        Right=2,
        Up,
        Down=5
    }
    
    #endregion

    /// <summary>
    /// Gives the ball a completely random velocity (50%/50% left/right + 50%/50% up/down) with the minimum velocity
    /// </summary>
    public void GiveRandomVelocity()
    {
        // TODO: Give our rigidbody a random velocity, 50%/50% left/right + 50%/50% up/down - must be at least at _minVelocity
        velocity = Random.Range(_minVelocity, 10f);
        int upOrDown = Random.Range((int)Direction.Up, (int)Direction.Down);
        if (upOrDown == (int)Direction.Up)
        {
            direction = Vector3.up;
        }
        else
        {
            direction = Vector3.down;
        }
        int leftOrRight = Random.Range((int)Direction.Left, (int)Direction.Right);
        if (leftOrRight == (int)Direction.Left)
        {
            direction += Vector3.left;
        }
        else
        {
            direction += Vector3.right;
        }
        GetComponent<Rigidbody>().velocity = direction*velocity;
    }

    /// <summary>
    /// Resets the ball (position and velocity)
    /// </summary>
    public void Reset()
    {
        // TODO: Reset our ball's position and velocity
        GetComponent<Rigidbody>().transform.position = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make sure if the ball lost velocity that we're never below the minimum
        if (GetComponent<Rigidbody>().velocity.magnitude < _minVelocity)
        {
            float ratio = _minVelocity / GetComponent<Rigidbody>().velocity.magnitude;
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * ratio;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // TODO: Handle trigger collisions for endzones
        // TODO: Make sure we collided with an endzone
        // TODO: Raise the EnteredEndZone event if we did
        if (collider.tag.Equals("End Zone"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        
            if(collider.name.Equals("Left End Zone"))
            {
                EnteredEndZone.Invoke(EndZone.EndZoneType.Left);
            }
            else if (collider.name.Equals("Right End Zone"))
            {
                EnteredEndZone.Invoke(EndZone.EndZoneType.Right);

            }
        }
    }
}