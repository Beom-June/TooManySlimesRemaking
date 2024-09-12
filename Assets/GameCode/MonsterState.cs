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
    [SerializeField] private float _basicHp = 50.0f;   // �⺻ ���� HP
    [SerializeField] private float _bossHp = 200.0f;   // ���� ���� HP
    [SerializeField] private float _basicDropPercent = 50.0f; // �⺻ ���� ���� ��� Ȯ��
    [SerializeField] private float _bossDropPercent = 25.0f;  // ���� ���� ���� ��� Ȯ��
    [SerializeField] private Slider _hpBar;             //  �ش� ���� hpBar
    private float _initialHp;                           // ������ �ʱ� ü��
    private float _monsterHp;

    [Header("Monster Item Setting")]
    [SerializeField] private GameObject _dropJewel;
    [SerializeField] private float _dropPercent = 50.0f;

    // ���� ��� �̺�Ʈ ����
    public delegate void MonsterDeathHandler(GameObject deadMonster);
    public event MonsterDeathHandler _onMonsterDeath;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        // ���� Ÿ�Կ� ���� HP�� ��� Ȯ�� ����
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

        // HP �� �ʱ�ȭ
        if (_hpBar != null)
        {
            _hpBar.maxValue = _monsterHp;
            _hpBar.value = _monsterHp;
        }
    }

    void Update()
    {

    }
    // ü���� ���ҽ�Ű�� �޼���
    public void TakeDamage(float damage)
    {
        _monsterHp -= damage;

        // Slider Update
        if (_hpBar != null)
        {
            _hpBar.value = _monsterHp; // ���� ü�¿� ���� �����̴��� ���� ������Ʈ
        }

        if (_monsterHp <= 0)
        {
            _monsterHp = 0;
            Debug.Log("*****" + gameObject.name + " Monster HP is 0 *****");

            // �߰����� ó���� ���⿡ �߰�
            // ���� ���� ó��, �ִϸ��̼� ���� ��
            _onMonsterDeath?.Invoke(gameObject);

            //  ���� ���� �� �ش� ���� ���� ���� �÷��̾��� ���� �ִϸ��̼� ���� ó��
            PlayerHitBox _playerHitBox = FindObjectOfType<PlayerHitBox>();
            if (_playerHitBox != null)
            {
                _playerHitBox.StopAttackAnimation();
            }

            //  ���� ���� �� �ش� ���� ���� (Death Animation ����)
            Vector3 _dropPos = transform.position - transform.forward * 4f;
            Instantiate(_dropJewel, _dropPos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
