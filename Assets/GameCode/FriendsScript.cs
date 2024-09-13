using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsScript : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator; // 플레이어의 애니메이터
    private Animator _Animator; 
    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerAnimator != null && _Animator != null)
        {
            // 플레이어 애니메이터에서 파라미터 가져오기
            bool isVictory = _playerAnimator.GetBool("isVictory");
            bool isMove = _playerAnimator.GetBool("isMove");

            _Animator.SetBool("isVictory", isVictory);
            _Animator.SetBool("isMove", isMove);
        }
    }

    // 플레이어 애니메이터를 설정하는 메서드
    public void SetPlayerAnimator(Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
    }
}
