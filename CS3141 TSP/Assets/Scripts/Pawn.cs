using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{

    public override List<Vector2> ListMoves()    //returns an array of the locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        int isWhite = gameObject.tag == "White" ? 1 : -1;

        //Pawn moves
        if (PieceAt(new Vector2(0, tile * isWhite) + position) == null)
        {
            moves.Add(new Vector2(0, tile * isWhite));    //one space

            if (!hasMoved)
            {
                moves.Add(new Vector2(0, tile * 2 * isWhite));    //two spaces
            }
        }

        if (PointCollidesWithTeam(new Vector2(tile, tile * isWhite) + position, OtherTeam(gameObject.tag)))
            moves.Add(new Vector2(tile, tile * isWhite));    //diag right
        if (PointCollidesWithTeam(new Vector2(tile * -1, tile * isWhite) + position, OtherTeam(gameObject.tag)))
            moves.Add(new Vector2(tile * -1, tile * isWhite));    //diag left


        for (int i = moves.Count - 1; i >= 0; i--)
        {
            moves[i] += position;
            if (!GameController.boardColl.OverlapPoint(moves[i]) ||
                PointCollidesWithTeam(moves[i], gameObject.tag))  //remove move if it's an illegal space
                moves.Remove(moves[i]);
        }


        return moves;
    }
    
}
