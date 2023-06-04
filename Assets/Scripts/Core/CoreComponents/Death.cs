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
        if (!transform.parent.parent.CompareTag("Player"))
        {
            PlayerStats.Instance.IncreaseXP(core.Stats.getXP);
            PlayerStats.Instance.Gold.Increase(core.Stats.getGold);
        }

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
