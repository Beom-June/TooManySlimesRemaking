using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private PlayerController _playerController;
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Monster"))
        {
            Debug.Log("*** _hitBox Checking *** ");

            if (_playerController != null)
            {
                _playerController.PlayerAnim.SetBool("isAttack", true);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Monster"))
        {
            Debug.Log("*** _hitBox Checking End *** ");
            if (_playerController != null)
            {
                _playerController.PlayerAnim.SetBool("isAttack", false);
            }
        }
    }

    // 공격 애니메이션을 중지하는 메서드
    public void StopAttackAnimation()
    {
        if (_playerController != null)
        {
            _playerController.PlayerAnim.SetBool("isAttack", false);
        }
    }
}
