using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _getJewel = 0;                  //  ȹ���� ������ ����

    [Header("Player Settings")]
    [SerializeField] private List<GameObject> _weaponList;
    private int _currentWeaponIndex = 0;                         // ���� ������ ���� �ε���
    [SerializeField] private List<GameObject> _arrowFriend;
    private int _currentArrowIndex = 0;                          // ���� Ȱ��ȭ�� ȭ�� ģ���� �ε���
    [SerializeField] private GameObject _boommerFriend;
    [SerializeField] private List<GameObject> _orbitalWeapon;       //  �� ������ ȸ���ϴ� ����
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
            // UI ������Ʈ ȣ��
            if (_uiManager != null)
            {
                _uiManager.UpdateJewelCount(_getJewel);  // ���� �� ������Ʈ
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

    // ���� ��ư Ŭ���� ���� ����
    public void ChangeWeaopn()
    {
        // ���� ���� ��Ȱ��ȭ
        _weaponList[_currentWeaponIndex].SetActive(false);

        // �ε��� ���� (����Ʈ ���� �����ϸ� �ٽ� 0����)
        _currentWeaponIndex = (_currentWeaponIndex + 1) % _weaponList.Count;

        // ���ο� ���� Ȱ��ȭ
        _weaponList[_currentWeaponIndex].SetActive(true);

        Debug.Log("Weapon Changed to: " + _weaponList[_currentWeaponIndex].name);
    }

    //  �������� Arrow Friend Call ��ư Ŭ���� ģ�� ��ȯ
    public void CallArrowFriend()
    {
        if (_currentArrowIndex < _arrowFriend.Count)
        {
            // ���� �ε����� �ش��ϴ� ArrowFriend Ȱ��ȭ
            _arrowFriend[_currentArrowIndex].SetActive(true);
            Debug.Log("Arrow Friend Called: " + _arrowFriend[_currentArrowIndex].name);
            _currentArrowIndex++;
        }
        else
        {
            Debug.Log("*** No More ***");
        }
    }

    //  �������� Boomer Friend Call ��ư Ŭ���� ģ�� ��ȯ
    public void CallBommerFriend()
    {
        _boommerFriend.SetActive(true);
    }

    //  �� ������ ȸ���ϴ� ���� ��ư
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
