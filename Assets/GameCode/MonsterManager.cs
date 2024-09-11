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
}
