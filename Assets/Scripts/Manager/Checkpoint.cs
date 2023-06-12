using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    private Transform player;
    private Vector3 lastCheckpointPosition;
    private Player playerRef;
    private Animator anim;
    private AudioSource teleportSFX;

    private float respawnDelay = 3f;
    private bool isActive;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRef = player.GetComponent<Player>();
        anim = GetComponent<Animator>();
        teleportSFX = GetComponent<AudioSource>();

        lastCheckpointPosition = player.position;
        Debug.Log(lastCheckpointPosition);

        PlayerStats.Instance.Health.OnCurrentValueZero += RespawnAtLastCheckpoint;

        UI.SetActive(false);
        isActive = false;
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
            if (!isActive)
            {
                UI.SetActive(true);
            }
            else
            {
                SetCheckpoint(other.transform.position);
                PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (UI.activeSelf)
            {
                UI.SetActive(false);
            }
        }
    }

    public void OnActivePortal()
    {
        teleportSFX.Play();
        anim.SetTrigger("active");
        isActive = true;
        UI.SetActive(false);
    }
}
