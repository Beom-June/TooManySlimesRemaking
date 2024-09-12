using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _getJewel = 0;         //  획득한 보석의 갯수

    /// <summary>
    /// Property
    /// </summary>
    #region Property
    // 플레이어의 HP 프로퍼티
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    // 플레이어의 데미지 프로퍼티
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    // 플레이어의 속도 프로퍼티
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
