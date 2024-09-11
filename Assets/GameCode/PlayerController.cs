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
        // GameManager 상태 변화 이벤트 구독
        GameManager._gameManager._onGameStateChange += OnGameStateChange;
    }
    void Update()
    {
            PlayerInput();
            PlayerMove();
    }
    void OnDestroy()
    {
        // GameManager의 상태 변경 이벤트 구독 해제
        GameManager._gameManager._onGameStateChange -= OnGameStateChange;
        Debug.Log(" *** GameManager 구독 해제 *** ");
    }


    // 키 입력 함수
    void PlayerInput()
    {
        _horizentalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");
    }

    private void PlayerAttack()
    {

    }

    #region Player 이동 관련
    // 플레이어 이동 함수
    public void PlayerMove()
    {
        _moveVec = new Vector3(_horizentalAxis, 0, _verticalAxis).normalized;

        // 목표 속도 설정
        float _speed = _playerState.Speed;
        Vector3 _targetVelocity = _moveVec * _speed;
        _targetVelocity.y = _playerRigidbody.velocity.y; // y축 속도는 점프 등으로 인한 변화를 유지

        // 리지드바디의 속도를 직접 설정
        _playerRigidbody.velocity = _targetVelocity;

        _animator.SetBool("isMove", _moveVec != Vector3.zero);
    }

    // 플레이어 시점 함수
    void PlayerTurn()
    {
        // 이동 방향 카메라 시점
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

    // 게임 상태 변경을 처리하는 콜백
    void OnGameStateChange(GameState newGameState)
    {
        Debug.Log("PlayerController: GameState가 " + newGameState + "로 변경되었습니다.");

        if (newGameState == GameState.GameOver)
        {
            // 게임 오버 상태 처리, 예: 플레이어 컨트롤 비활성화
            _playerState.Speed = 0;
            Debug.Log("PlayerController: 게임 오버 - 플레이어 이동 멈춤");
        }
        else if (newGameState == GameState.Playing)
        {
            // 플레이어 컨트롤 다시 활성화
            _playerState.Speed = 10f;
            Debug.Log("PlayerController: 게임 시작 - 플레이어 이동 가능");
        }
    }
}
