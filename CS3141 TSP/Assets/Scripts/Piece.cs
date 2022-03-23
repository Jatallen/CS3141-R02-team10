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

    private SpriteRenderer spriteRenderer ;
    

    public abstract List<Vector2> ListMoves();  //returns an array of the locations the piece can move

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                if(isHighlighted == false){

                    if(!pieceSelectedHighlight){
                    var instance = Instantiate(highlight, transform.position, Quaternion.identity);
                    Destroy(instance, 1);
                    }

                    pieceSelectedHighlight = true;

                    foreach (Vector2 move in ListMoves()){

                    Debug.Log(" moves are " + move);
                    
                    var moveInstance = Instantiate(moveHighlight, move, Quaternion.identity);
                    Destroy(moveInstance, 1);
   
                }
                isHighlighted = true; 
                Invoke("setIsHighlightedtoFalse", 1.0f);
            }

            

            transform.position = mousePos;
            
            if (Input.GetMouseButtonDown(0))    //left click
            {
                if (thisCollider.OverlapPoint(GameController.selectPos))    //if clicked on original spot
                {
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
                            if (pieceAtMove != null)   //take pieces - WIP
                            {
                                pieceAtMove.GetComponent<Piece>().TakePiece(gameObject);
                                Debug.Log("Piece Taken");
                            }

                            transform.position = move;
                            GameController.playerSelect = null;
                            GameController.turn = OtherTeam(GameController.turn);
                            
                            // If pawn reaches back rank, destroy pawn and create a queen in its place
                            if ((GetComponent<Pawn>() != null) && Mathf.Abs(transform.position.y) == 3.5){
                                Destroy(gameObject);
                                if (gameObject.tag == "White"){
                                    Instantiate(Queen, transform.position, transform.rotation);
                                } else {
                                    Instantiate(Queen, transform.position, transform.rotation);
                                }
                            }

                            Debug.Log("Successful Move: " + mousePos + move);
                            hasMoved = true;
                            isHighlighted = false;
                            pieceSelectedHighlight = false;
                            spriteRenderer.sortingOrder = 0;
                            break;
                        }
                        Debug.Log("Failed Move: " + mousePos + move);
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

    public string OtherTeam(string team)
    {
        if (team == "White")
            return "Black";
        return "White";
    }

    void setIsHighlightedtoFalse(){
        isHighlighted = false;
    }

}
