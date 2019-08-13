using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager instance = null;

    public BoardManager boardManager;
    public GameObject test;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Awake()
    {
        // Game Manager Singleton Check
        if (instance != null && instance != this) {
            Destroy(instance);
        }

        instance = this;

        boardManager = GetComponent<BoardManager>();
        boardManager.SetupScene();
    }


}
