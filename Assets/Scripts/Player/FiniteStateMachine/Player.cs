using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SoundEffectEntry
{
    public string name;
    public AudioClip audioClip;
}

public class Player : MonoBehaviour, IDataPersistence
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerShieldState ShieldState { get; private set; }
    #endregion

    #region Components
    [SerializeField] private PlayerData playerData;

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField] public Transform attackPoint;
    [SerializeField] public TMP_Text fpsText;
    #endregion

    #region Musics
    public SoundEffectEntry[] soundEffects;

    private AudioSource audioSource;
    #endregion

    #region Other Variables
    public bool IsAlive { get; set; }
    public bool IsShielding { get; set; }

    private int lastFrameIndex;
    private float[] frameDeltatimeArray;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        FallState = new PlayerFallState(this, StateMachine, playerData, "fall");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "jump");
        SlideState = new PlayerSlideState(this, StateMachine, playerData, "slide");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        ShieldState = new PlayerShieldState(this, StateMachine, playerData, "shield");

        IsAlive = true;
        frameDeltatimeArray = new float[50];
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);

        PlayerStats.Instance.Health.OnCurrentValueZero += Death;
        // Core.Stats.OnHealthZero += Death;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio source to not play on awake
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        // SHOW FPS
        frameDeltatimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltatimeArray.Length;

        fpsText.text = "fps: " + Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void ComboAttack1Transition()
    {
        if (AttackState.attackCount >= 1)
        {
            PlaySoundEffect("Attack2", 0.3f);
            Anim.SetTrigger("attack2");
        }
    }

    private void ComboAttack2Transition()
    {
        if (AttackState.attackCount >= 2)
        {
            PlaySoundEffect("Attack3", 0.3f);
            Anim.SetTrigger("attack3");
        }
    }

    private void Attack1SoundEffect()
    {
        PlaySoundEffect("Attack1", 0.3f);
    }
    
    private void MovementStartTrigger()
    {
        // Core.Movement.SetVelocityX(playerData.movementAttack[AttackState.attackCount] * Core.Movement.FacingDirection);
    }

    private void MovementStopTrigger()
    {
        // Core.Movement.SetVelocityZero();
    }

    public void RemoveGameObject()
    {
        transform.gameObject.SetActive(false);
    }

    private void Death()
    {
        IsAlive = false;
        InputHandler.OnDisable();
    }

    private void Respawned()
    {

    }

    private void AttackTrigger()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoint.position, playerData.attackRange, playerData.enemyLayerMask);

		foreach (Collider2D collider in detectedObjects) 
        {
			IDamageable damageable = collider.GetComponent<IDamageable>();

			if (damageable != null)
            {
                Debug.Log(PlayerStats.Instance.Damage.CurrentValue);
				damageable.Damage(PlayerStats.Instance.Damage.CurrentValue);
			}

			IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

			if (knockbackable != null)
			{
				knockbackable.Knockback(playerData.knockbackAngle, playerData.knockbackStrength, Core.Movement.FacingDirection);
			}
		}
    }

    private void OnDisable()
    {
        // Core.Stats.OnHealthZero -= Death;
        PlayerStats.Instance.Health.OnCurrentValueZero -= Death;
    }

    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltatimeArray)
        {
            total += deltaTime;
        }

        return frameDeltatimeArray.Length / total;
    }
    
    public void OnDrawGizmos()
    {  
        Gizmos.DrawWireSphere(attackPoint.position, playerData.attackRange);
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }
    
    public void PlaySoundEffect(string name, float volume = 1.0f)
    {
        foreach (var entry in soundEffects)
        {
            if (entry.name == name)
            {
                AudioClip audioClip = entry.audioClip;
                if (audioClip != null)
                {
                    audioSource.PlayOneShot(audioClip, volume);
                }
                return;
            }
        }
    }
}
