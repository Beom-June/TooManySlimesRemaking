using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterState : MonoBehaviour
{
    public enum MonsterType
    {
        basic,
        boss
    }
    [Header("Monster Setting")]
    [SerializeField] private MonsterType _monsterType;
    [SerializeField] private float _basicHp = 50.0f;   // 기본 몬스터 HP
    [SerializeField] private float _bossHp = 200.0f;   // 보스 몬스터 HP
    [SerializeField] private float _basicDropPercent = 50.0f; // 기본 몬스터 보석 드롭 확률
    [SerializeField] private float _bossDropPercent = 25.0f;  // 보스 몬스터 보석 드롭 확률
    [SerializeField] private Slider _hpBar;             //  해당 몬스터 hpBar
    private float _initialHp;                           // 몬스터의 초기 체력
    private float _monsterHp;

    [Header("Monster Item Setting")]
    [SerializeField] private GameObject _dropJewel;
    [SerializeField] private float _dropPercent = 50.0f;

    // 몬스터 사망 이벤트 정의
    public delegate void MonsterDeathHandler(GameObject deadMonster);
    public event MonsterDeathHandler _onMonsterDeath;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        // 몬스터 타입에 따라 HP와 드롭 확률 설정
        switch (_monsterType)
        {
            case MonsterType.basic:
                _monsterHp = _basicHp;
                _dropPercent = _basicDropPercent;
                break;

            case MonsterType.boss:
                _monsterHp = _bossHp;
                _dropPercent = _bossDropPercent;
                break;
        }

        // HP 바 초기화
        if (_hpBar != null)
        {
            _hpBar.maxValue = _monsterHp;
            _hpBar.value = _monsterHp;
        }
    }

    void Update()
    {

    }
    // 체력을 감소시키는 메서드
    public void TakeDamage(float damage)
    {
        _monsterHp -= damage;

        // Slider Update
        if (_hpBar != null)
        {
            _hpBar.value = _monsterHp; // 현재 체력에 맞춰 슬라이더의 값을 업데이트
        }

        if (_monsterHp <= 0)
        {
            _monsterHp = 0;
            Debug.Log("*****" + gameObject.name + " Monster HP is 0 *****");

            // 추가적인 처리를 여기에 추가
            // 몬스터 죽음 처리, 애니메이션 변경 등
            _onMonsterDeath?.Invoke(gameObject);

            //  보석 생성 및 해당 몬스터 삭제 전에 플레이어의 공격 애니메이션 종료 처리
            PlayerHitBox _playerHitBox = FindObjectOfType<PlayerHitBox>();
            if (_playerHitBox != null)
            {
                _playerHitBox.StopAttackAnimation();
            }

            //  보석 생성 및 해당 몬스터 삭제 (Death Animation 부재)
            Vector3 _dropPos = transform.position - transform.forward * 4f;
            Instantiate(_dropJewel, _dropPos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
