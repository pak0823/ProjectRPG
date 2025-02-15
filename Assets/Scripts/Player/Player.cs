using System.Collections;
using System.Collections.Generic;
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
    private float monsterAttackTime; //몬스터의 마지막 공격 시간
    private float attackWindowTime = 0.3f; //패링 가능한 시간

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        //StartCoroutine(UpdateState());
    }

    //protected override IEnumerator UpdateState()
    //{
    //    //while (true)
    //    //{
    //    //    InputManager();

    //    //    switch (currentState)
    //    //    {
    //    //        case EPlayerState.IDLE:
    //    //            Idle();
    //    //            break;
    //    //        case EPlayerState.ATTACK:
    //    //            Attack();
    //    //            break;
    //    //    }
    //    //    yield return null;
    //    //}

    //    yield return null;
    //}

    private void Update()
    {
        InputManager();

        if (currentState == EPlayerState.IDLE)
            Idle();
    }

    void FixedUpdate()
    {
        if (currentState == EPlayerState.MOVE)
        {
            Move();
        }
        if (runEndTime <= Time.time - 3f)
            IncreaseStamina();
    }

    public void ChangeState(EPlayerState _currentstate)
    {
        currentState = _currentstate;
    }

    private void InputManager()
    {
        //키 입력 확인
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))   //이동
        {
            if (currentState != EPlayerState.ATTACK && currentState != EPlayerState.DEFEND) // 공격상태 또는 방어 상태가 아닐 때만 이동 가능
                ChangeState(EPlayerState.MOVE);
        }
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 클릭 시 공격
        {
            if (CanAttack() && currentState != EPlayerState.HIT)//쿨타임 체크
            {
                ChangeState(EPlayerState.ATTACK);
                Attack();
            }
            else
            {
                Debug.Log($"아직 {Time.time - (lastAttackTime + attackCoolDown)}의 쿨타임이 남았습니다!");
                return;
            }

        }
        if (Input.GetMouseButton(1)) //마우스 오른쪽 클릭 시 방어
        {
            if (currentState != EPlayerState.HIT && currentState != EPlayerState.DEFENDHIT)
            {
                ChangeState(EPlayerState.DEFEND);
                Defend();
            }
        }
        else if (Input.GetMouseButtonUp(1)) // 마우스 오른쪽 버튼에서 손을 뗐을 때
        {
            ChangeState(EPlayerState.IDLE); // Idle 상태로 변경
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Shared.skillCoolDown.isCooldown[0])
            {
                ChangeState(EPlayerState.ATTACK);
                SetAnimationState("animationState", 10);
                UseSkill(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (!Shared.skillCoolDown.isCooldown[1])
            {
                ChangeState(EPlayerState.ATTACK);
                SetAnimationState("animationState", 11);
                UseSkill(1);
            }
        }
    }

    // 적의 공격 감지 시 호출
    public void OnAttackDetected()
    {
        monsterAttackTime = Time.time; // 공격이 감지되면 시간 기록
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

    //public void OnDestroy()
    //{
    //    //이 오브젝트가 제거될 시 실행되는 함수임.
    //    Destroy(gameObject);
    //}


    //public override void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            ChangeState(EPlayerState.IDLE);
        }

        //Debug.Log(collision.gameObject.name);
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
