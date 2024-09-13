using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _posAttack;           //  boss ���� ���� ���� ��ġ
    [SerializeField] private GameObject _attackPrefab;          //  �����Ͽ� �߻��ϴ� ������Ʈ

    [SerializeField] private float _attackForce = 10f;              //  �߻��ϴ� ��
    [SerializeField] private float _attackInterval = 2f;            //  ���� ���� �ð�
    private float _timeSinceLastAttack = 0f;                        //  ������ ���� ���� ��� �ð�

    [SerializeField ]private MonsterPlayerChecking _monsterPlayerChecking;

    void Start()
    {
    }
    void Update()
    {
        // �÷��̾ �����Ǿ����� Ȯ��
        if (_monsterPlayerChecking != null && _monsterPlayerChecking.CheckingPlayer)
        {
            Debug.Log("##");
            // �ð��� �帣�� ������ ����
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

        // _posAttack ����Ʈ���� ������ ��ġ�� ����
        int randomIndex = Random.Range(0, _posAttack.Count);
        GameObject selectedPosition = _posAttack[randomIndex];

        // ���õ� ��ġ�� _attackPrefab�� ����
        GameObject attackInstance = Instantiate(_attackPrefab, selectedPosition.transform.position, selectedPosition.transform.rotation);

        // -z �������� �߻�
        Rigidbody rb = attackInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // -z �������� ���� ����
            rb.AddForce(Vector3.back * _attackForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("The attack prefab does not have a Rigidbody.");
        }
    }
}
