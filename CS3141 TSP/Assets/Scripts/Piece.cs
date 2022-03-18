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

    public abstract List<Vector2> ListMoves();  //returns an array of the locations the piece can move

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();

        hasMoved = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameController.playerSelect == gameObject)  //if the piece is selected
        {
            transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))    //left click
            {
                if (thisCollider.OverlapPoint(GameController.selectPos))    //if clicked on original spot
                {
                    transform.position = GameController.selectPos;
                    GameController.playerSelect = null;
                    Debug.Log("Put Down: " + position + GameController.selectPos);
                }
                else
                {
                    foreach (Vector2 move in ListMoves())   //check if the clicked spot is a possible move
                    {
                        if (thisCollider.OverlapPoint(move))
                        {
                            transform.position = move;
                            GameController.playerSelect = null;
                            GameController.turn = GameController.turn == "White" ? "Black" : "White";
                            List<Collider2D> result = new List<Collider2D>();
                            if (thisCollider.OverlapCollider(new ContactFilter2D(), result) >= 1)   //take pieces - WIP
                            {
                                foreach (Collider2D col in result)
                                    if (col.gameObject != GameController.board)
                                        col.GetComponent<Piece>().TakePiece(gameObject);
                                Debug.Log("Piece Taken");
                            }
                            Debug.Log("Successful Move: " + position + move);
                            hasMoved = true;
                            break;
                        }
                        Debug.Log("Failed Move: " + position + move);
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

                Debug.Log("Pick Up: " + GameController.selectPos);
            }
        }

        //Debug.Log(mousePos);

    }

    //return whether or not the point collides with a piece of the given team
    public bool PointCollidesWithTeam(Vector2 p)
    {
        foreach (GameObject piece in GameObject.FindGameObjectsWithTag(gameObject.tag))
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
}
