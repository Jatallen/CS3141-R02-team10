using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2> ListMoves()    //returns an array of the relative locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        //Rook moves
        moves.Add(new Vector2(tile, 0));    //right
        moves.Add(new Vector2(0, tile));    //top
        moves.Add(new Vector2(tile * -1, 0));    //left
        moves.Add(new Vector2(0, tile * -1));   //bottom

        for (int i = 0; i < moves.Count; i++)
        {
            moves[i] += position;
            if (!GameController.boardColl.OverlapPoint(moves[i]) ||
                PointCollidesWithTeam(moves[i]))  //remove move if it's an illegal space
                moves.Remove(moves[i]);
        }

        return moves;
    }
    
}
