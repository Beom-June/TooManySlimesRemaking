using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum weaponType
    {
        Dagger,
        Hammer,
        Sword
    }
    [SerializeField] private float _weaponDamage = 10.0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("***** Weapon Hit *****");

            MonsterState monsterState = collider.GetComponent<MonsterState>();
            if (monsterState != null)
            {
                // 몬스터의 체력 감소
                monsterState.TakeDamage(_weaponDamage);
            }
        }
    }
}
