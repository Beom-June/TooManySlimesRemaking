using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject _basicUI;        //  basic UI
    [SerializeField] private GameObject _shopUI;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            Debug.Log(" ***** Shop Open ***** ");
            _shopUI.SetActive(true);
            _basicUI.SetActive(false);

            // 게임 일시정지
            Time.timeScale = 0;
        }
    }
}
