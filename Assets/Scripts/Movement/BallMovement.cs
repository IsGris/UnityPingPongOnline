using UnityEngine;
using Unity.Netcode;
using System;

public class BallMovement : NetworkBehaviour
{
    public Vector2 Direction { get; protected set; }
    public float Speed;
    // Maximum deviation of the ball when a hit occurs at the paddle's edge
    public const float MaxDeviation = 0.8f;
    public bool IsMoving { get; private set; } = false;

    /// <summary>
    /// Generates random direction for ball
    /// </summary>
    private void GenerateRandomDirection()
    {
        Direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.3f, 0.3f)).normalized;
    }

    public void StartMovement()
    {
        if (!IsSpawned || IsMoving || !HasAuthority) return;

        IsMoving = true;

        GenerateRandomDirection();
        NetworkManager.NetworkTickSystem.Tick += UpdateBallPositionTick;
    }

    public void StopMovement()
    {
        if (!IsSpawned || !IsMoving || !HasAuthority) return;

        IsMoving = false;

        NetworkManager.NetworkTickSystem.Tick -= UpdateBallPositionTick;
    }

    private void UpdateBallPositionTick()
    {
        gameObject.transform.position +=
            (Convert.ToSingle(GameConstants.TimeBetweenTicks + NetworkManager.Singleton.LocalTime.TickOffset) * // How much time passed since last tick
            Speed * Direction).Vector3();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("WORK");

        if (collider.CompareTag("Player"))
        {
            // How close the ball is to the edge of the paddle (0 at center, 1 at edge)
            float edgeHitPercent = Mathf.Abs(transform.position.y - collider.bounds.center.y) / (collider.bounds.size.y / 2);

            // Check if the paddle is below the ball (hit down half of the paddle)
            bool hitDownEdge = collider.transform.position.y > transform.position.y;
            // Apply deviation depending on the hit location on the paddle
            Direction = new Vector2(Direction.x * -1,
                Mathf.Lerp(0, MaxDeviation, edgeHitPercent) * (hitDownEdge ? -1 : 1)).normalized;
        }
        else if (collider.CompareTag("Wall"))
            Direction = new Vector2(Direction.x, Direction.y * -1).normalized;
    }

    public override void OnNetworkDespawn()
    {
        if (HasAuthority)
            NetworkManager.NetworkTickSystem.Tick -= UpdateBallPositionTick;
        base.OnNetworkSpawn();
    }
}
