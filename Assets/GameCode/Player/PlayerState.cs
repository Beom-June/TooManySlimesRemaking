using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _getJewel = 0;         //  ȹ���� ������ ����

    /// <summary>
    /// Property
    /// </summary>
    #region Property
    // �÷��̾��� HP ������Ƽ
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    // �÷��̾��� ������ ������Ƽ
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    // �÷��̾��� �ӵ� ������Ƽ
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Jewel")
            _getJewel++;
    }
}
