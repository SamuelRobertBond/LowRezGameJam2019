using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    public ControllerManager owner;
    public LayerMask blockingLayer;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected abstract void Act();
}
