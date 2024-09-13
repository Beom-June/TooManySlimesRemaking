using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;  // ���͸� �����ϴ� �θ� ������Ʈ
    [SerializeField] private List<GameObject> _monsterList = new List<GameObject>();

    void Start()
    {
        if (_parentObject != null)
        {
            AddAllChildrenToList();
        }
    }

    void Update()
    {
        SubscribeToMonsterEvents();
    }
    // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ�� ����Ʈ�� �߰��ϴ� �޼���
    private void AddAllChildrenToList()
    {
        _monsterList.Clear();  // ����Ʈ �ʱ�ȭ

        // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ�� �˻�
        foreach (Transform _child in _parentObject.transform)
        {
            // �ڽ� ������Ʈ�� ����Ʈ�� �߰�
            _monsterList.Add(_child.gameObject);
        }

        Debug.Log("Total " + _monsterList.Count + "Monster Checking");
    }
    // ���� ������ �̺�Ʈ�� �����ϴ� �޼���
    private void SubscribeToMonsterEvents()
    {
        foreach (GameObject monster in _monsterList)
        {
            MonsterState monsterState = monster.GetComponent<MonsterState>();
            if (monsterState != null)
            {
                monsterState._onMonsterDeath += HandleMonsterDeath;
            }
        }
    }

    // ���� ü���� 0�� �Ǿ��� �� ȣ��Ǵ� �޼���
    private void HandleMonsterDeath(GameObject deadMonster)
    {
        //Debug.Log(deadMonster.name + " has been defeated.");

        // ����Ʈ���� ���� ����
        if (_monsterList.Contains(deadMonster))
        {
            _monsterList.Remove(deadMonster);
            //Debug.Log(deadMonster.name + " removed from list.");
        }
        else
        {
            //Debug.Log(deadMonster.name + " not found in list.");
        }
    }
}
