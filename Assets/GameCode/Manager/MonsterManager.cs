using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;  // 몬스터를 포함하는 부모 오브젝트
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
    // 부모 오브젝트의 모든 자식 오브젝트를 리스트에 추가하는 메서드
    private void AddAllChildrenToList()
    {
        _monsterList.Clear();  // 리스트 초기화

        // 부모 오브젝트의 모든 자식 오브젝트를 검색
        foreach (Transform _child in _parentObject.transform)
        {
            // 자식 오브젝트를 리스트에 추가
            _monsterList.Add(_child.gameObject);
        }

        Debug.Log("Total " + _monsterList.Count + "Monster Checking");
    }
    // 몬스터 상태의 이벤트를 구독하는 메서드
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

    // 몬스터 체력이 0이 되었을 때 호출되는 메서드
    private void HandleMonsterDeath(GameObject deadMonster)
    {
        //Debug.Log(deadMonster.name + " has been defeated.");

        // 리스트에서 몬스터 제거
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
