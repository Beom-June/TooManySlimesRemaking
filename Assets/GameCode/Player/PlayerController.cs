using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Funtion
    /// </summary>
    [SerializeField] private bool _autoMoveBool = false;        //  true Ŭ���� �� �������� ��� �̵�
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
    [SerializeField] private GameObject _mainCamera;          //  ���� ī�޶�
    [SerializeField] private GameObject _finishCamera;        //  finishLine ���޽� ��ȯ ī�޶�
    private bool _isGameFinished = false;                     // ���� ���� ���¸� �����ϴ� ����

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

        //  ������ ������ �� �̻� �������� �ʰ� �ϱ�
        //if (!_isGameFinished)
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
        if (_autoMoveBool)
        {
            _horizentalAxis = Input.GetAxisRaw("Horizontal"); // �¿� �̵��� �Է°� �״�� ���
            _verticalAxis = 1;    // z �������� �̵� (������ �̵�)
        }
        else
        {
            _horizentalAxis = Input.GetAxisRaw("Horizontal");
            _verticalAxis = Input.GetAxisRaw("Vertical");
        }
    }

    #region Player �̵� ����
    // �÷��̾� �̵� �Լ�
    public void PlayerMove()
    {
        if (_isGameFinished)
        {
            // ������ ������ ���� �̵����� ����
            _playerRigidbody.velocity = Vector3.zero;
            _animator.SetBool("isMove", false);
            return;
        }

        // ���� �Է� ����� ��
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            _isGameFinished = true;
            // ī�޶� ��ȯ �ڷ�ƾ ȣ��
            StartCoroutine(SwitchCamera());
            _animator.SetBool("isVictory", true);


            Debug.Log(" ***** Game Finish ***** ");
        }
    }

    private IEnumerator SwitchCamera()
    {
        // �ʱ� ����
        Vector3 startPosition = _mainCamera.transform.position;
        Quaternion startRotation = _mainCamera.transform.rotation;

        Vector3 endPosition = _finishCamera.transform.position;
        Quaternion endRotation = _finishCamera.transform.rotation;

        // ī�޶� Ȱ��ȭ
        _finishCamera.gameObject.SetActive(true);
        _mainCamera.gameObject.SetActive(false);

        float transitionTime = 2.0f; // ��ȯ �ð� (��)
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;

            // ī�޶� ��ġ�� ȸ���� �ε巴�� ����
            _mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            _mainCamera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ �Ϸ�� �� ���� ���·� ����
        _mainCamera.transform.position = endPosition;
        _mainCamera.transform.rotation = endRotation;
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
