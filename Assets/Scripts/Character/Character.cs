using System.Collections;
using UnityEngine;

public abstract partial class Character : MonoBehaviour
{
    public SpriteRenderer SPRITERENDERER;
    public Rigidbody RIGIDBODY;
    public CapsuleCollider CAPSULECOLLIDER;

    protected virtual void Awake()
    {
        SPRITERENDERER = GetComponent<SpriteRenderer>();
        ANIMATOR = GetComponent<Animator>();
        RIGIDBODY = GetComponent<Rigidbody>();
        CAPSULECOLLIDER = GetComponent<CapsuleCollider>();
    }

    protected virtual void Start() { }

    protected abstract IEnumerator UpdateState();
    protected abstract void Idle();
    protected abstract void Move();
    protected abstract void Attack();
    protected abstract void OnCollisionEnter(Collision collision);
    protected abstract void OnCollisionExit(Collision collision);
}