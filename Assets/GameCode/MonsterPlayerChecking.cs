using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerChecking : MonoBehaviour
{
    [SerializeField] private bool _checkingPlayer = false;      //  �÷��̾� üŷ�� bool ��

    public bool CheckingPlayer
    {
        get { return _checkingPlayer; }
        set { _checkingPlayer = value; }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log(" ***** Player Checking ***** ");
            _checkingPlayer = true;
        }
    }
}
