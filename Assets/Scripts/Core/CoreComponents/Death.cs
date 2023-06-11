using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private CollectedItem collectedItem;
    [SerializeField] private Sprite imageGold;
    [SerializeField] private Sprite imageXP;

    private void Start()
    {
        base.Awake();

        core.Stats.OnHealthZero += Die;
    }

    public void Die()
    {
        if (!transform.parent.parent.CompareTag("Player"))
        {
            collectedItem.SetCollectItemUI(imageGold, "Gold", core.Stats.getGold.ToString());
            collectedItem.SetCollectItemUI(imageXP, "XP", core.Stats.getXP.ToString());
            PlayerStats.Instance.IncreaseXP(core.Stats.getXP);
            PlayerStats.Instance.Gold.Increase(core.Stats.getGold);
            core.transform.gameObject.SetActive(false);
        }

        foreach (var particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }

    }

    private void OnDisable()
    {
        core.Stats.OnHealthZero -= Die;
    }
}
