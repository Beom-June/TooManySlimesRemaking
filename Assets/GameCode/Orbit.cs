using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private Transform _target;         //      공전 목표
    [SerializeField] private float _orbitSpeed;         //      공전 속도
    [SerializeField] private Vector3 _offSet;           //      target과의 거리

    void Start()
    {
        _offSet = transform.position - _target.position;
    }

    void Update()
    {
        transform.position = _target.position + _offSet;
        //  공전 설정
        transform.RotateAround(_target.position, Vector3.up, _orbitSpeed * Time.deltaTime);
        _offSet = transform.position - _target.position;
    }

    public void OrbitSpeedUp()
    {
        _orbitSpeed += 100.0f;
    }
}
