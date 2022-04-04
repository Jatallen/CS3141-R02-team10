using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    [System.NonSerialized]
    public Collider2D thisCollider;
    [System.NonSerialized]
    public Vector2 position;
    [System.NonSerialized]
    public bool hasMoved;

    //highlight
    public GameObject highlight;
    public GameObject moveHighlight;
    public bool isHighlighted = false;
    public bool pieceSelectedHighlight = false;

    public GameObject Queen;
    public GameObject Rook;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    

    public abstract List<Vector2> ListMoves();  //returns an array of the locations the piece can move

    public GameObject PieceTaken;
    private static float changeTakeWhiteX = 4.4f;
    private static float changeTakeBlackX = 4.4f;
    private static float changeTakeWhiteY = 3.5f;
    private static float changeTakeBlackY = -3.5f;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        position = new Vector2(transform.position.x, transform.position.y);

        hasMoved = false;
        isHighlighted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameController.playerSelect == gameObject)  //if the piece is selected
        {
            if(!pieceSelectedHighlight){
                    var instance = Instantiate(highlight, transform.position, Quaternion.identity);
                    Destroy(instance, 1);

                    pieceSelectedHighlight = true;
                }

                if(isHighlighted == false){

                    foreach (Vector2 move in ListMoves()) {
                        //Debug.Log(" moves are " + move);
                        var moveInstance = Instantiate(moveHighlight, move, Quaternion.identity);
                        Destroy(moveInstance, 1);
                    }

                isHighlighted = true; 
                Invoke("setIsHighlightedtoFalse", 1.0f); //update is called constantly, destroys and creates at same rate so when a piece is grabbed is shows
            }

            transform.position = mousePos;
            
            if (Input.GetMouseButtonDown(0))    //left click
            {
                if (thisCollider.OverlapPoint(GameController.selectPos))    //if clicked on original spot
                {
                    pieceSelectedHighlight = false;
                    spriteRenderer.sortingOrder = 0;
                    transform.position = GameController.selectPos;
                    GameController.playerSelect = null;
                    //Debug.Log("Put Down: " + position + GameController.selectPos);
                }
                else
                {
                    foreach (Vector2 move in ListMoves())   //check if the clicked spot is a possible move
                    {
                        if (thisCollider.OverlapPoint(move))
                        {
                            GameObject pieceAtMove = PieceAt(move);
                            if (pieceAtMove != null)   //take pieces
                            {
                                pieceAtMove.GetComponent<Piece>().TakePiece(gameObject);
                                //Debug.Log("Piece Taken");
                            }

                            transform.position = move;
                            GameController.playerSelect = null;
                            GameController.turn = OtherTeam(GameController.turn);
                            
                            if ((GetComponent<Pawn>() != null))
                            {
                                // If pawn reaches back rank, destroy pawn and create a queen in its place
                                if (Mathf.Abs(transform.position.y) == 3.5)
                                {
                                    Destroy(gameObject);
                                    if (gameObject.tag == "White")
                                    {
                                        Instantiate(Queen, transform.position, transform.rotation);
                                    }
                                    else
                                    {
                                        Instantiate(Queen, transform.position, transform.rotation);
                                    }
                                }
                                if (Mathf.Abs(position.y - move.y) == 2)
                                    GetComponent<Pawn>().enPassant = true;
                                else
                                    GetComponent<Pawn>().enPassant = false;

                                if (position.x != move.x)
                                {
                                    GameObject pass = PieceAt(new Vector2(move.x, position.y));
                                    if (pass != null && pass.GetComponent<Pawn>() != null && pass.GetComponent<Pawn>().enPassant)
                                        pass.GetComponent<Piece>().TakePiece(gameObject);
                                }

                            }

                            NoPassant();


                            // If castle move by either King is made, move appropriate Rook
                            if ((GetComponent<King>() != null)){
                                Debug.Log("King position: " + position);
                                if (move.x == -1.5 && position.x == 0.5){
                                    Destroy(PieceAt(new Vector2((float)-3.5, (float)position.y)));
                                    Vector2 rookPos = new Vector2(move.x+1,move.y);
                                    Instantiate(Rook, rookPos, transform.rotation);
                                }
                                else if (move.x == 2.5 && position.x == 0.5){
                                    Destroy(PieceAt(new Vector2((float)3.5, (float)position.y)));
                                    Vector2 rookPos = new Vector2(move.x-1,move.y);
                                    Instantiate(Rook, rookPos, transform.rotation);
                                }
                            }
                            //Debug.Log("Successful Move: " + mousePos + move);
                            hasMoved = true;
                            isHighlighted = false;
                            pieceSelectedHighlight = false;
                            spriteRenderer.sortingOrder = 0;
                            audioSource.Play();
                            break;
                        }
                        //Debug.Log("Failed Move: " + mousePos + move);
                    }
                    if (ListMoves().Count == 0)
                        Debug.Log("No Moves");
                }
            }
            
        }
        else
        {
            position = new Vector2(transform.position.x, transform.position.y);

            if (Input.GetMouseButtonDown(0) && thisCollider.OverlapPoint(mousePos) && GameController.turn == gameObject.tag && GameController.playerSelect == null) //select this piece
            {
                GameController.playerSelect = gameObject;
                GameController.selectPos = position;
                spriteRenderer.sortingOrder = 1; //Makes sprite go to front
                

                //Debug.Log("Pick Up: " + GameController.selectPos);
            }
        }
        //Debug.Log(mousePos);
    }

    //return whether or not the point collides with a piece of the given team
    public bool PointCollidesWithTeam(Vector2 p, string team)
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag(team))
            if (piece.GetComponent<Collider2D>().OverlapPoint(p) && piece != gameObject)
                return true;
        return false;
    }

    
    public void TakePiece(GameObject piece) //handle this piece being taken by another piece
    {
        /*if (GameController.playerSelect != collision.gameObject &&
            GameController.board != collision.gameObject &&
            GameController.turn != gameObject.tag)*/
            if (GameController.turn.Equals("White")){
                GameObject captured = Instantiate(PieceTaken, new Vector3(changeTakeBlackX,changeTakeBlackY,0), Quaternion.identity);
                SpriteRenderer capturedSr = captured.GetComponent<SpriteRenderer>();
                capturedSr.sprite = GetComponent<SpriteRenderer>().sprite;
                changeTakeBlackX+=0.4f;
                if (changeTakeBlackX > 6.4f){
                    changeTakeBlackY+=0.5f;
                    changeTakeBlackX-=2.0f;
                }
            }
            else if (GameController.turn.Equals("Black")){
                GameObject captured = Instantiate(PieceTaken, new Vector3(changeTakeWhiteX,changeTakeWhiteY,0), Quaternion.identity);
                SpriteRenderer capturedSr = captured.GetComponent<SpriteRenderer>();
                capturedSr.sprite = GetComponent<SpriteRenderer>().sprite;
                changeTakeWhiteX+=0.4f;
                if (changeTakeWhiteX > 6.4f){
                    changeTakeWhiteY-=0.5f;
                    changeTakeWhiteX-=2.0f;
                }
            }

            Destroy(gameObject); 
    }
    
    public GameObject PieceAt(Vector2 pos)
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("White"))
            if (piece.GetComponent<Collider2D>().OverlapPoint(pos) && piece != gameObject)
                return piece;
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Black"))
            if (piece.GetComponent<Collider2D>().OverlapPoint(pos) && piece != gameObject)
                return piece;
        return null;
    }

    public static string OtherTeam(string team)
    {
        if (team == "White")
            return "Black";
        return "White";
    }

    void setIsHighlightedtoFalse(){
        isHighlighted = false;
    }

    public void NoPassant()
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("White"))
            if (piece.GetComponent<Pawn>() != null && piece != gameObject)
                piece.GetComponent<Pawn>().enPassant = false;
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Black"))
            if (piece.GetComponent<Pawn>() != null && piece != gameObject)
                piece.GetComponent<Pawn>().enPassant = false;
    }


    public List<Vector2> TestMoves(List<Vector2> moves)
    {
        for (int i = moves.Count - 1; i >= 0; i--)
        {
            moves[i] += position;
            if (!GameController.boardColl.OverlapPoint(moves[i]) ||
                PointCollidesWithTeam(moves[i], tag)) //remove move if it's an illegal space
                moves.Remove(moves[i]);
            else if (true && tag == GameController.turn)
            {
                Vector3 temp = transform.position;
                Vector2 moveTemp = moves[i];
                if (PointCollidesWithTeam(moves[i], OtherTeam(tag)))
                    PieceAt(moves[i]).tag = tag;
                transform.position = moves[i];
                if (GameController.CheckChecked(tag))
                    moves.Remove(moves[i]);
                transform.position = temp;
                if (PointCollidesWithTeam(moveTemp, tag))
                    PieceAt(moveTemp).tag = OtherTeam(tag);
            }
        }

        return moves;
    }
}
