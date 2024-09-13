using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _posAttack;           //  boss 몬스터 공격 생성 위치
    [SerializeField] private GameObject _attackPrefab;          //  생성하여 발사하는 오브젝트

    [SerializeField] private float _attackForce = 10f;              //  발사하는 힘
    [SerializeField] private float _attackInterval = 2f;            //  공격 간격 시간
    private float _timeSinceLastAttack = 0f;                        //  마지막 공격 이후 경과 시간

    [SerializeField ]private MonsterPlayerChecking _monsterPlayerChecking;

    void Start()
    {
    }
    void Update()
    {
        // 플레이어가 감지되었는지 확인
        if (_monsterPlayerChecking != null && _monsterPlayerChecking.CheckingPlayer)
        {
            Debug.Log("##");
            // 시간이 흐르면 공격을 시작
            _timeSinceLastAttack += Time.deltaTime;

            if (_timeSinceLastAttack >= _attackInterval)
            {
                Attack();
                _timeSinceLastAttack = 0f;
            }
        }
    }
    private void Attack()
    {
        if (_posAttack.Count == 0 || _attackPrefab == null)
        {
            Debug.LogWarning("No attack positions or prefab assigned.");
            return;
        }

        // _posAttack 리스트에서 랜덤한 위치를 선택
        int randomIndex = Random.Range(0, _posAttack.Count);
        GameObject selectedPosition = _posAttack[randomIndex];

        // 선택된 위치에 _attackPrefab을 생성
        GameObject attackInstance = Instantiate(_attackPrefab, selectedPosition.transform.position, selectedPosition.transform.rotation);

        // -z 방향으로 발사
        Rigidbody rb = attackInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // -z 방향으로 힘을 가함
            rb.AddForce(Vector3.back * _attackForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("The attack prefab does not have a Rigidbody.");
        }
    }
}
