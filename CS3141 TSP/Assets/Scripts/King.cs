using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{ 

        // Update is called once per frame
    /*void Update()
    {
        position = new Vector2(transform.position.x, transform.position.y);

        if (Input.GetKeyDown(KeyCode.W))
            transform.position = position + GetComponent<King>().ListMoves()[2];
        if (Input.GetKeyDown(KeyCode.A))
            transform.position = position + GetComponent<King>().ListMoves()[4];
        if (Input.GetKeyDown(KeyCode.S))
            transform.position = position + GetComponent<King>().ListMoves()[6];
        if (Input.GetKeyDown(KeyCode.D))
            transform.position = position + GetComponent<King>().ListMoves()[0];
    }*/

    public override List<Vector2> ListMoves()    //returns an array of the relative locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        
        //King moves
        moves.Add(new Vector2(tile, 0));    //right
        moves.Add(new Vector2(tile, tile)); //top right
        moves.Add(new Vector2(0, tile));    //top
        moves.Add(new Vector2(tile * -1, tile)); //top left
        moves.Add(new Vector2(tile * -1, 0));    //left
        moves.Add(new Vector2(tile * -1, tile * -1));   //bottom left
        moves.Add(new Vector2(0, tile * -1));   //bottom
        moves.Add(new Vector2(tile, tile * -1));   //bottom right

        if ((PieceAt(new Vector2(position.x - 4, position.y)).GetComponent<Rook>().hasMoved == false) && PieceAt(new Vector2(position.x - 3, position.y)) == null && PieceAt(new Vector2(position.x - 2, position.y)) == null && PieceAt(new Vector2(position.x - 1, position.y)) == null){
            moves.Add(new Vector2(tile * -2, 0));
        }

        for (int i = moves.Count - 1; i >= 0; i--)
        {
            moves[i] += position;
            if (!GameController.boardColl.OverlapPoint(moves[i]) ||
                PointCollidesWithTeam(moves[i], gameObject.tag))  //remove move if it's an illegal space
                moves.Remove(moves[i]);
        }

        return moves;
    }
    public new void TakePiece(GameObject piece)
    {
        //if (GameController.playerSelect != collision.gameObject && GameController.turn != gameObject.tag)
            GameController.GameOver(gameObject.tag);
    }
}
