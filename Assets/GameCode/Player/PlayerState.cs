using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _getJewel = 0;                  //  획득한 보석의 갯수

    [Header("Player Settings")]
    [SerializeField] private List<GameObject> _weaponList;
    private int _currentWeaponIndex = 0;                         // 현재 장착된 무기 인덱스
    [SerializeField] private List<GameObject> _arrowFriend;
    private int _currentArrowIndex = 0;                          // 현재 활성화할 화살 친구의 인덱스
    [SerializeField] private GameObject _boommerFriend;
    [SerializeField] private List<GameObject> _orbitalWeapon;       //  몸 주위를 회전하는 무기
    private int _currentOrbitalWeaponIndex = 0;

    private UIManager _uiManager;
    /// <summary>
    /// Property
    /// </summary>
    #region Property
    // Player HP Property
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    // Player Damage Property
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    // Player Speed Property
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    // Player getJewel Property
    public int GetJewel
    {
        get { return _getJewel; }
        set
        {
            _getJewel = value;
            // UI 업데이트 호출
            if (_uiManager != null)
            {
                _uiManager.UpdateJewelCount(_getJewel);  // 보석 수 업데이트
            }
        }
    }

    #endregion

    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {

    }

    // 무기 버튼 클릭시 무기 변경
    public void ChangeWeaopn()
    {
        // 현재 무기 비활성화
        _weaponList[_currentWeaponIndex].SetActive(false);

        // 인덱스 증가 (리스트 끝에 도달하면 다시 0으로)
        _currentWeaponIndex = (_currentWeaponIndex + 1) % _weaponList.Count;

        // 새로운 무기 활성화
        _weaponList[_currentWeaponIndex].SetActive(true);

        Debug.Log("Weapon Changed to: " + _weaponList[_currentWeaponIndex].name);
    }

    //  상점에서 Arrow Friend Call 버튼 클릭시 친구 소환
    public void CallArrowFriend()
    {
        if (_currentArrowIndex < _arrowFriend.Count)
        {
            // 현재 인덱스에 해당하는 ArrowFriend 활성화
            _arrowFriend[_currentArrowIndex].SetActive(true);
            Debug.Log("Arrow Friend Called: " + _arrowFriend[_currentArrowIndex].name);
            _currentArrowIndex++;
        }
        else
        {
            Debug.Log("*** No More ***");
        }
    }

    //  상점에서 Boomer Friend Call 버튼 클릭시 친구 소환
    public void CallBommerFriend()
    {
        _boommerFriend.SetActive(true);
    }

    //  몸 주위를 회전하는 무기 버튼
    public void OrbitalWeapon()
    {
        if(_currentOrbitalWeaponIndex < _orbitalWeapon.Count)
        {
        _orbitalWeapon[_currentOrbitalWeaponIndex].SetActive(true);
        Debug.Log("Orbital Weapon Activated: " + _orbitalWeapon[_currentOrbitalWeaponIndex].name);
        _currentOrbitalWeaponIndex++;
        }
        else
        {
            Debug.Log("*** No More ***");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Jewel")
            GetJewel++;
    }
}
