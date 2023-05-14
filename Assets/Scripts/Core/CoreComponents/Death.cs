using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;

    private void Start()
    {
        base.Awake();

        core.Stats.OnHealthZero += Die;
    }

    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }

        core.transform.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        core.Stats.OnHealthZero -= Die;
    }
}
