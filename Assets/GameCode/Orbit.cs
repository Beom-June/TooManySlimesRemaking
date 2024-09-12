using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private Transform _target;         //      ���� ��ǥ
    [SerializeField] private float _orbitSpeed;         //      ���� �ӵ�
    [SerializeField] private Vector3 _offSet;           //      target���� �Ÿ�

    void Start()
    {
        _offSet = transform.position - _target.position;
    }

    void Update()
    {
        transform.position = _target.position + _offSet;
        //  ���� ����
        transform.RotateAround(_target.position, Vector3.up, _orbitSpeed * Time.deltaTime);
        _offSet = transform.position - _target.position;
    }

    public void OrbitSpeedUp()
    {
        _orbitSpeed += 100.0f;
    }
}
