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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Monster"))
        {
            Debug.Log("*** _hitBox Checking *** ");

            if (_playerController != null)
            {
                _playerController.PlayerAnim.SetBool("isAttack", true);
                //_playerController.SetAttacking(true);
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
                //_playerController.SetAttacking(false);
            }
        }
    }

    // ���� �ִϸ��̼��� �����ϴ� �޼���
    public void StopAttackAnimation()
    {
        if (_playerController != null)
        {
            _playerController.PlayerAnim.SetBool("isAttack", false);
        }
    }
}
