using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public enum monsterType
    {
        basic,
        boss
    }
    [Header("Monster Setting")]
    [SerializeField] private float _monsterHp = 50.0f;

    [Header("Monster Item Setting")]
    [SerializeField] private GameObject _item;
    [SerializeField] private float _dropPercent = 50.0f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    // 체력을 감소시키는 메서드
    public void TakeDamage(float damage)
    {
        _monsterHp -= damage;

        if (_monsterHp <= 0)
        {
            _monsterHp = 0;
            Debug.Log("*****" + gameObject.name + " Monster HP is 0 *****");

            // 추가적인 처리를 여기에 추가
            // 몬스터 죽음 처리, 애니메이션 변경 등
        }
    }
}
