using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorParameterType
{
    Float,
    Integer,
    Bool,
    Trigger
}

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    private PlayerData playerData;

    [SerializeField]
    private string attackTriggerName = "Attack";

    [SerializeField]
    private string attackPatternCountName = "AttackPatternCount";

    [Space]
    
    public CooldownController SkillCoolDown;

    /// <summary>
    /// 공격 애니메이션 끝났을 때 콜백
    /// </summary>
    public event Action OnEndAttackAnimation = () => { };

    private Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        // Test
        playerData = GameManager.Instance.PlayerData;
        playerData.PlayerWeapon = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    /// <summary>
    /// 플레이어 스크립트에서 공격할 때 사용할 함수
    /// </summary>
    /// <param name="value">공격 패턴 카운트</param>
    public void SetAttackAnimator(int value)
    {
        SetAnimatorParameter(AnimatorParameterType.Trigger, attackTriggerName);
        SetAnimatorParameter(AnimatorParameterType.Integer, attackPatternCountName, value);
    }

    /// <summary>
    /// 플레이어에서 호출할 Animator Parameter 설정할 함수
    /// </summary>
    public void SetAnimatorParameter(AnimatorParameterType parameterType, string parameterName,
        object value = null)
    {
        if (parameterType != AnimatorParameterType.Trigger && value == null)
            throw new NullReferenceException($"{nameof(SetAnimatorParameter)} Method : Parameter value is Null");

        switch (parameterType)
        {
            case AnimatorParameterType.Float:
                animator.SetFloat(parameterName, (float)value);
                break;
            case AnimatorParameterType.Integer:
                animator.SetInteger(parameterName, (int)value);
                break;
            case AnimatorParameterType.Bool:
                animator.SetBool(parameterName, (bool)value);
                break;
            case AnimatorParameterType.Trigger:
                animator.SetTrigger(parameterName);
                break;
        }
    }

    public abstract void Skill();

    public void TryUseSkill()
    {
        if (SkillCoolDown.IsCooldownFinished())
            Skill();
    }

    public void FlipX(bool isLeft)
    {
        transform.rotation = Quaternion.Euler(0, isLeft ? 180 : 0, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // 자식개체에 있는 WeaponChildColliderHandler 스크립트에서 호출
    public void OnChildTriggerEnter2D(Collider2D other)
    {
        print("무기 공격");
    }

    #region Animator Event에서 쓸 함수들

    public void ResetTrigger()
    {
        animator.ResetTrigger(attackTriggerName);
    }

    public void EndAttackAnimation()
    {
        OnEndAttackAnimation();
        ResetTrigger();
    }

    #endregion
}