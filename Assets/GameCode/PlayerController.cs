using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Funtion
    /// </summary>
    private float _horizentalAxis;
    private float _verticalAxis;

    /// <summary>
    /// Component
    /// </summary>
    [SerializeField] private GameObject _hitBox;
    private Vector3 _moveVec;
    private Rigidbody _playerRigidbody;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private PlayerState _playerState;

    /// <summary>
    /// Property
    /// </summary>
    public Animator PlayerAnim
    {
        get { return _animator; }
        set { _animator = value; }
    }
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerState = GetComponent<PlayerState>();
    }
    void Start()
    {
        // GameManager ���� ��ȭ �̺�Ʈ ����
        GameManager._gameManager._onGameStateChange += OnGameStateChange;
    }
    void Update()
    {
            PlayerInput();
            PlayerMove();
    }
    void OnDestroy()
    {
        // GameManager�� ���� ���� �̺�Ʈ ���� ����
        GameManager._gameManager._onGameStateChange -= OnGameStateChange;
        Debug.Log(" *** GameManager ���� ���� *** ");
    }


    // Ű �Է� �Լ�
    void PlayerInput()
    {
        _horizentalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");
    }

    private void PlayerAttack()
    {

    }

    #region Player �̵� ����
    // �÷��̾� �̵� �Լ�
    public void PlayerMove()
    {
        _moveVec = new Vector3(_horizentalAxis, 0, _verticalAxis).normalized;

        // ��ǥ �ӵ� ����
        float _speed = _playerState.Speed;
        Vector3 _targetVelocity = _moveVec * _speed;
        _targetVelocity.y = _playerRigidbody.velocity.y; // y�� �ӵ��� ���� ������ ���� ��ȭ�� ����

        // ������ٵ��� �ӵ��� ���� ����
        _playerRigidbody.velocity = _targetVelocity;

        _animator.SetBool("isMove", _moveVec != Vector3.zero);
    }

    // �÷��̾� ���� �Լ�
    void PlayerTurn()
    {
        // �̵� ���� ī�޶� ����
        transform.LookAt(transform.position + _moveVec);
    }

    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _navMeshAgent.enabled = true;
        }
    }

    // ���� ���� ������ ó���ϴ� �ݹ�
    void OnGameStateChange(GameState newGameState)
    {
        Debug.Log("PlayerController: GameState�� " + newGameState + "�� ����Ǿ����ϴ�.");

        if (newGameState == GameState.GameOver)
        {
            // ���� ���� ���� ó��, ��: �÷��̾� ��Ʈ�� ��Ȱ��ȭ
            _playerState.Speed = 0;
            Debug.Log("PlayerController: ���� ���� - �÷��̾� �̵� ����");
        }
        else if (newGameState == GameState.Playing)
        {
            // �÷��̾� ��Ʈ�� �ٽ� Ȱ��ȭ
            _playerState.Speed = 10f;
            Debug.Log("PlayerController: ���� ���� - �÷��̾� �̵� ����");
        }
    }
}
