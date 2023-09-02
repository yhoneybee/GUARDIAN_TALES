using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class GuardianData : EntityData
{
    [NonSerialized]
    public Guardian Guardian;

    public Weapon PlayerWeapon;

    public Vector3 Pos => Guardian.transform.position;

    [field: SerializeField, Tooltip("크리티컬 확률")]
    public int CriticalChance { get; set; }

    public int CriticalDamage => AttackDamage.Value * 2;

    public event Action<int> OnCriticalAttack;

    public override int Damage
    {
        get
        {
            if (Random.Range(0, 100) <= CriticalChance)
            {
                OnCriticalAttack(CriticalDamage);
                return CriticalDamage;
            }

            return AttackDamage.Value;
        }
    }


    public override void Init()
    {
        base.Init();
        
        Guardian = FindObjectOfType<Guardian>();
    }

    public void WeaponChange(Weapon weapon)
    {
        PlayerWeapon = weapon;
        
        weapon.Init(this);
    }
}