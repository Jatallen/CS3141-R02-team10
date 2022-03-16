using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public static float tileSize;

    public static GameObject board;
    public static Collider2D boardColl;

    // Start is called before the first frame update
    void Start()
    {
        board = gameObject;
        boardColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
