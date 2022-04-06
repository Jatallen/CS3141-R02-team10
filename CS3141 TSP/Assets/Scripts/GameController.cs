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

    public float waitTime;
    private float waited;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        tile = tileSize;
        board = gameObject;
        turn = "White";

        boardColl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        waited = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (turn == "Black")
            waited += Time.deltaTime;
        if (waited >= waitTime)
        {
            AIMove();
            waited = 0;
        }
    }

    public static void GameOver(string lost)
    {
        turn = "Grey";
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag(lost))
        {
            SpriteRenderer pieceSR = piece.GetComponent<SpriteRenderer>();
            Color color = Color.gray;
            //color.a = .667f;
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

        if (allMoves.Count == 0)
            GameOver("Black");

        List<Vector2> allMovesClone = new List<Vector2>(allMoves);
        List<GameObject> movePiecesClone = new List<GameObject>(movePieces);
        for (int i = allMoves.Count - 1; i >= 0; i--)
        {
            GameObject takingPiece = movePieces[i].GetComponent<Piece>().PieceAt(allMoves[i]);
            if (takingPiece == null || takingPiece.GetComponent<Piece>().priority < movePieces[i].GetComponent<Piece>().priority)
            {
                allMoves.RemoveAt(i);
                movePieces.RemoveAt(i);
            }
        }
        if (allMoves.Count == 0 && movePieces.Count == 0)
        {
            allMoves = new List<Vector2>(allMovesClone);
            movePieces = new List<GameObject>(movePiecesClone);
            //Debug.Log("No taking moves");
        }
        for (int i = allMoves.Count - 1; i >= 0; i--)
        {
            GameObject takingPiece = movePieces[i].GetComponent<Piece>().PieceAt(allMoves[i]);
            if (takingPiece != null && takingPiece.GetComponent<Piece>().priority < movePieces[i].GetComponent<Piece>().priority - 3)
            {
                allMoves.RemoveAt(i);
                movePieces.RemoveAt(i);
            }
        }
        if (allMoves.Count == 0 && movePieces.Count == 0)
        {
            allMoves = allMovesClone;
            movePieces = movePiecesClone;
            //Debug.Log("No taking moves");
        }


        int r = Random.Range(0, allMoves.Count);
        Piece movePiece = movePieces[r].GetComponent<Piece>();

        GameObject pieceAtMove = movePiece.PieceAt(allMoves[r]);
        if (pieceAtMove != null)   //take pieces
        {
            pieceAtMove.GetComponent<Piece>().TakePiece(gameObject);
        }

        Pawn pawn = movePieces[r].GetComponent<Pawn>();
        if (pawn != null)
        {
            // If pawn reaches back rank, destroy pawn and create a queen in its place
            if (Mathf.Abs(movePieces[r].transform.position.y) == 3.5)
            {
                Destroy(movePieces[r]);
                Instantiate(pawn.Queen, movePieces[r].transform.position, Quaternion.identity);
            }

            if (Mathf.Abs(pawn.position.y - allMoves[r].y) == 2)
                pawn.enPassant = true;
            else
                pawn.enPassant = false;

            if (pawn.position.x != allMoves[r].x)
            {
                GameObject pass = movePiece.PieceAt(new Vector2(allMoves[r].x, pawn.position.y));
                if (pass != null && pass.GetComponent<Pawn>() != null && pass.GetComponent<Pawn>().enPassant)
                    pass.GetComponent<Piece>().TakePiece(gameObject);
            }
        }
        movePiece.NoPassant();

        // If castle move by either King is made, move appropriate Rook
        if ((GetComponent<King>() != null))
        {
            Debug.Log("King position: " + movePiece.position);
            if (allMoves[r].x == -1.5 && movePiece.position.x == 0.5)
            {
                Destroy(movePiece.PieceAt(new Vector2((float)-3.5, (float)movePiece.position.y)));
                Vector2 rookPos = new Vector2(allMoves[r].x + 1, allMoves[r].y);
                Instantiate(movePiece.Rook, rookPos, transform.rotation);
            }
            else if (allMoves[r].x == 2.5 && movePiece.position.x == 0.5)
            {
                Destroy(movePiece.PieceAt(new Vector2((float)3.5, (float)movePiece.position.y)));
                Vector2 rookPos = new Vector2(allMoves[r].x - 1, allMoves[r].y);
                Instantiate(movePiece.Rook, rookPos, transform.rotation);
            }
        }
        //Debug.Log("Successful Move: " + mousePos + move);
        movePiece.hasMoved = true;
        audioSource.Play();

        movePieces[r].transform.position = allMoves[r];
        turn = "White";
    }

    public static bool CheckChecked(string team)
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag(Piece.OtherTeam(team)))
            foreach(Vector2 move in piece.GetComponent<Piece>().ListMoves())
                if(piece.GetComponent<Piece>().PieceAt(move) != null && piece.GetComponent<Piece>().PieceAt(move).GetComponent<King>() != null)
                    return true;

        return false;
    }
}
