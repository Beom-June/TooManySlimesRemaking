using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _basicUI;            //  basic UI
    [SerializeField] private GameObject _shopUI;             //  Shop UI
    [SerializeField] private List<Text> _jewelCountText;    //  획득 보석 카운트 Text

    //  Shop Out Button 누르면 게임 계속 시작
    public void OutnBtn()
    {
        _basicUI.SetActive(true);
        _shopUI.SetActive(false);
        Time.timeScale = 1;
    }

    // 보석 수를 업데이트하는 메서드
    public void UpdateJewelCount(int jewelCount)
    {
        foreach (Text _jewelText in _jewelCountText)
        {
            _jewelText.text = jewelCount.ToString();  // 모든 텍스트에 보석 수 적용
        }
    }
}
