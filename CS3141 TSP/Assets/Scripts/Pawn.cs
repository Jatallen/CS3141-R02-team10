using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{

    [System.NonSerialized]
    public bool enPassant;

    public override void SetPriority()
    {
        priority = 1;
    }

    public override List<Vector2> ListMoves()    //returns an array of the locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        int isWhite = gameObject.tag == "White" ? 1 : -1;

        //Pawn moves
        if (PieceAt(new Vector2(0, tile * isWhite) + position) == null)
        {
            moves.Add(new Vector2(0, tile * isWhite));    //one space

            if (!hasMoved && PieceAt(new Vector2(0, tile * isWhite * 2) + position) == null)
            {
                moves.Add(new Vector2(0, tile * 2 * isWhite));    //two spaces
            }
        }

        if (PointCollidesWithTeam(new Vector2(tile, tile * isWhite) + position, OtherTeam(gameObject.tag)))
            moves.Add(new Vector2(tile, tile * isWhite));    //diag right
        if (PointCollidesWithTeam(new Vector2(tile * -1, tile * isWhite) + position, OtherTeam(gameObject.tag)))
            moves.Add(new Vector2(tile * -1, tile * isWhite));    //diag left

        GameObject right = PieceAt(new Vector2(tile, 0) + position);
        if (right != null && right.GetComponent<Pawn>() != null && right.GetComponent<Pawn>().enPassant)
            moves.Add(new Vector2(tile, tile * isWhite));    //diag right
        GameObject left = PieceAt(new Vector2(tile * -1, 0) + position);
        if (left != null && left.GetComponent<Pawn>() != null && left.GetComponent<Pawn>().enPassant)
            moves.Add(new Vector2(tile * -1, tile * isWhite));    //diag left

        return TestMoves(moves);
    }
    
}
