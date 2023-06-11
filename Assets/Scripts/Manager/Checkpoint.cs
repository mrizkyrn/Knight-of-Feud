using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform player;
    private Vector3 lastCheckpointPosition;
    private float respawnDelay = 3f; // Delay in seconds before respawn

    private Player playerRef;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRef = player.GetComponent<Player>();

        lastCheckpointPosition = player.position;
        Debug.Log(lastCheckpointPosition);

        PlayerStats.Instance.Health.OnCurrentValueZero += RespawnAtLastCheckpoint;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.Health.OnCurrentValueZero -= RespawnAtLastCheckpoint;
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    public void RespawnAtLastCheckpoint()
    {
        StartCoroutine(RespawnDelayCoroutine());
    }

    private IEnumerator RespawnDelayCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        Debug.Log("Respawned");
        playerRef.IsAlive = true;
        playerRef.InputHandler.OnEnable();
        player.position = lastCheckpointPosition;
        PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetCheckpoint(other.transform.position);
            Debug.Log("Checkpoint set!");
        }
    }
}
