using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float tileSize;
    public static float tile;

    public static GameObject board;
    public static Collider2D boardColl;
    public static string turn;

    public static GameObject playerSelect;  //piece the player is holding
    public static Vector2 selectPos;    //original position of selected piece

    // Start is called before the first frame update
    void Start()
    {
        tile = tileSize;
        board = gameObject;
        turn = "White";

        boardColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GameOver(string lost)
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag(lost))
        {
            SpriteRenderer pieceSR = piece.GetComponent<SpriteRenderer>();
            Color color = Color.gray;
            color.a = .667f;
            pieceSR.color = color;
        }
    }
}
