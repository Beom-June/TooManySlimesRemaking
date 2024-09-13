using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsScript : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator; // �÷��̾��� �ִϸ�����
    private Animator _Animator; 
    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerAnimator != null && _Animator != null)
        {
            // �÷��̾� �ִϸ����Ϳ��� �Ķ���� ��������
            bool isVictory = _playerAnimator.GetBool("isVictory");
            bool isMove = _playerAnimator.GetBool("isMove");

            _Animator.SetBool("isVictory", isVictory);
            _Animator.SetBool("isMove", isMove);
        }
    }

    // �÷��̾� �ִϸ����͸� �����ϴ� �޼���
    public void SetPlayerAnimator(Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
    }
}
