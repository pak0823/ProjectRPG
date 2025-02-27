using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Character
{
    public EPlayerState currentState;
    public bool isGrounded;
    Monster monster;

    public float attackCoolDown = 1.0f; //공격 쿨타임
    public float lastAttackTime = 0.0f; // 마지막 공격 시간
    public float invincibilityTime = 1.0f; //피격 후 무적시간
    private float lastHitTime = 0f; // 마지막 피격 시간

    

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        StartCoroutine(UpdateState());
    }

    private IEnumerator UpdateState()
    {
        while (true)
        {
            InputManager(); // 입력 처리

            switch (currentState)
            {
                case EPlayerState.IDLE:
                    Idle();
                    break;
                case EPlayerState.ATTACK:
                    Attack();
                    break;
                case EPlayerState.DEFEND:
                    Defend();
                    break;
                //case EPlayerState.HIT:
                //    // 피격 처리 로직 추가 가능
                //    break;
                case EPlayerState.DIE:
                    Die();
                    break;
            }

            // 스태미너 증가 처리
            if (decreaseEndTime <= Time.time - 3f)
                IncreaseStamina();

            yield return null; // 다음 프레임까지 대기
        }
    }

    void FixedUpdate()
    {
        if (currentState == EPlayerState.MOVE)
        {
            Move();
        }
    }

    public void ChangeState(EPlayerState _currentstate)
    {
        currentState = _currentstate;
        //Debug.Log("changeState:" + currentState);
    }

    private readonly EPlayerState[] blockedStates =
    {   //특정 상태를 체크하는 배열
        EPlayerState.ATTACK,
        EPlayerState.DEFEND,
        EPlayerState.SKILL
    };

    private void InputManager()
    {
        // 마우스 포인터가 보일 경우 입력을 차단
        if (Cursor.visible)
        {
            return; // 입력 처리 중단
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (!blockedStates.Contains(currentState))
            {
                ChangeState(EPlayerState.MOVE);
            }
        }

        if (Input.GetMouseButtonDown(0) && CanAttack())
        {
            ChangeState(EPlayerState.ATTACK);
        }

        if (Input.GetMouseButton(1))
        {
            if(!blockedStates.Contains(currentState))
                ChangeState(EPlayerState.DEFEND);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (currentState == EPlayerState.DEFEND)
                ChangeState(EPlayerState.IDLE);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // 스킬 사용 처리
        HandleSkillInput();
    }

    

    private bool CanAttack()
    {
        // 쿨타임이 지난 경우에만 true 반환
        return Time.time >= lastAttackTime + attackCoolDown;
    }
    private bool CanHit()
    {
        // 무적 상태가 아닐 경우 true 반환
        return Time.time >= lastHitTime + invincibilityTime;
    }

    protected override IEnumerator DestroyObject(float _destroytime)
    {
        yield return new WaitForSeconds(_destroytime);
        Destroy(gameObject);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
        }
    }
}
