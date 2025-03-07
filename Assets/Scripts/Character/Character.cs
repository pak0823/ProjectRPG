using System.Collections;
using UnityEngine;

public abstract partial class Character : MonoBehaviour
{
    public SpriteRenderer SPRITERENDERER;
    public Rigidbody RIGIDBODY;
    public CapsuleCollider CAPSULECOLLIDER;
    public BoxCollider BoxCOLLIDER;

    protected virtual void Awake()
    {
        SPRITERENDERER = GetComponent<SpriteRenderer>();
        ANIMATOR = GetComponent<Animator>();
        RIGIDBODY = GetComponent<Rigidbody>();
        CAPSULECOLLIDER = GetComponent<CapsuleCollider>();
        BoxCOLLIDER = GetComponent<BoxCollider>();
    }

    protected virtual void Start() { }

    //protected abstract IEnumerator UpdateState();
    public abstract void Idle();
    public abstract void Move();
    public abstract void Attack();
    public abstract void TakeDamage(float _damage);
    protected abstract IEnumerator DestroyObject(float _destroytime);
    public abstract void OnCollisionEnter(Collision collision);
    public abstract void OnCollisionExit(Collision collision);
}