using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _basicUI;            //  basic UI
    [SerializeField] private GameObject _shopUI;             //  Shop UI
    [SerializeField] private List<Text> _jewelCountText;    //  ȹ�� ���� ī��Ʈ Text

    //  Shop Out Button ������ ���� ��� ����
    public void OutnBtn()
    {
        _basicUI.SetActive(true);
        _shopUI.SetActive(false);
        Time.timeScale = 1;
    }
}
