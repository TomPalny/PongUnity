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

    /// <summary>
    /// Gives the ball a completely random velocity (50%/50% left/right + 50%/50% up/down) with the minimum velocity
    /// </summary>
    public void GiveRandomVelocity()
    {
        // TODO: Give our rigidbody a random velocity, 50%/50% left/right + 50%/50% up/down - must be at least at _minVelocity
    }

    /// <summary>
    /// Resets the ball (position and velocity)
    /// </summary>
    public void Reset()
    {
        // TODO: Reset our ball's position and velocity
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

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}