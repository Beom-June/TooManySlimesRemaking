using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Funtion
    /// </summary>
    [SerializeField] private bool _autoMoveBool = false;        //  true 클릭시 앞 방향으로 계속 이동
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

    [Header("Finish Settings")]
    [SerializeField] private GameObject _mainCamera;          //  메인 카메라
    [SerializeField] private GameObject _finishCamera;        //  finishLine 도달시 전환 카메라
    private bool _isGameFinished = false;                     // 게임 종료 상태를 추적하는 변수

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

        //  게임이 끝나면 더 이상 움직이지 않게 하기
        //if (!_isGameFinished)
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
        if (_autoMoveBool)
        {
            _horizentalAxis = Input.GetAxisRaw("Horizontal"); // 좌우 이동은 입력값 그대로 사용
            _verticalAxis = 1;    // z 방향으로 이동 (앞으로 이동)
        }
        else
        {
            _horizentalAxis = Input.GetAxisRaw("Horizontal");
            _verticalAxis = Input.GetAxisRaw("Vertical");
        }
    }

    #region Player 이동 관련
    // 플레이어 이동 함수
    public void PlayerMove()
    {
        if (_isGameFinished)
        {
            // 게임이 끝났을 때는 이동하지 않음
            _playerRigidbody.velocity = Vector3.zero;
            _animator.SetBool("isMove", false);
            return;
        }

        // 수동 입력 모드일 때
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            _isGameFinished = true;
            // 카메라 전환 코루틴 호출
            StartCoroutine(SwitchCamera());
            _animator.SetBool("isVictory", true);


            Debug.Log(" ***** Game Finish ***** ");
        }
    }

    private IEnumerator SwitchCamera()
    {
        // 초기 상태
        Vector3 startPosition = _mainCamera.transform.position;
        Quaternion startRotation = _mainCamera.transform.rotation;

        Vector3 endPosition = _finishCamera.transform.position;
        Quaternion endRotation = _finishCamera.transform.rotation;

        // 카메라 활성화
        _finishCamera.gameObject.SetActive(true);
        _mainCamera.gameObject.SetActive(false);

        float transitionTime = 2.0f; // 전환 시간 (초)
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;

            // 카메라 위치와 회전을 부드럽게 보간
            _mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            _mainCamera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 보간이 완료된 후 최종 상태로 설정
        _mainCamera.transform.position = endPosition;
        _mainCamera.transform.rotation = endRotation;
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
