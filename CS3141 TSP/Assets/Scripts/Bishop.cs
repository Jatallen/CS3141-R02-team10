using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void SetPriority()
    {
        priority = 3;
    }

    public override List<Vector2> ListMoves()    //returns an array of the relative locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        //Bishop moves
        int j = 1;
        while (position.x + j * tile < 4 && position.y + j * tile < 4 && !PointCollidesWithTeam(new Vector2(tile * j, tile * j) + position, gameObject.tag))
        {
            moves.Add(new Vector2(tile * j, tile * j));    //top right
            if (PointCollidesWithTeam(new Vector2(tile * j, tile * j) + position, OtherTeam(gameObject.tag)))
                break;
            j++;
        }
        j = 1;
        while (position.x + j * tile < 4 && position.y - j * tile > -4 && !PointCollidesWithTeam(new Vector2(tile * j, tile * j * -1) + position, gameObject.tag))
        {
            moves.Add(new Vector2(tile * j, tile * j * -1));    //bottom right
            if (PointCollidesWithTeam(new Vector2(tile * j, tile * j * -1) + position, OtherTeam(gameObject.tag)))
                break;
            j++;
        }
        j = 1;
        while (position.x - j * tile > -4 && position.y - j * tile > -4 && !PointCollidesWithTeam(new Vector2(tile * j * -1, tile * j * -1) + position, gameObject.tag))
        {
            moves.Add(new Vector2(tile * j * -1, tile * j * -1));    //bottom right
            if (PointCollidesWithTeam(new Vector2(tile * j * -1, tile * j * -1) + position, OtherTeam(gameObject.tag)))
                break;
            j++;
        }
        j = 1;
        while (position.x - j * tile > -4 && position.y + j * tile < 4 && !PointCollidesWithTeam(new Vector2(tile * j * -1, tile * j) + position, gameObject.tag))
        {
            moves.Add(new Vector2(tile * j * -1, tile * j));    //top right
            if (PointCollidesWithTeam(new Vector2(tile * j * -1, tile * j) + position, OtherTeam(gameObject.tag)))
                break;
            j++;
        }

        return TestMoves(moves);
    }
}
