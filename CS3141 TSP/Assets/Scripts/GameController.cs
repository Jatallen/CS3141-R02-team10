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
        if (turn == "Black")
            AIMove();
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

    private void AIMove()
    {
        List<Vector2> allMoves = new List<Vector2>();
        List<GameObject> movePieces = new List<GameObject>();
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Black"))
        {
            allMoves.AddRange(piece.GetComponent<Piece>().ListMoves());
            while (movePieces.Count < allMoves.Count)
                movePieces.Add(piece);
        }
        int r = Random.Range(0, allMoves.Count);
        movePieces[r].transform.position = allMoves[r];
        turn = "White";
    }
}
