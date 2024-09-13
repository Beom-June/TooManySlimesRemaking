using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _weaponDamage;
    void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("***** OrbitWeapon Hit *****");

            MonsterState monsterState = collider.GetComponent<MonsterState>();
            if (monsterState != null)
            {
                // 몬스터의 체력 감소
                monsterState.TakeDamage(_weaponDamage);

            }
        }
    }
}
