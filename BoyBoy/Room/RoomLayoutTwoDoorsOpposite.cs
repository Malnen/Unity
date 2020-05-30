using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutTwoDoorsOpposite : RoomLayout
{
    private const int layoutCount = 6;
    public RoomLayoutTwoDoorsOpposite(GameObject parent) : base(parent)
    {

    }
    protected override void chooseLayout()
    {
        base.chooseLayout();
        int random = Random.Range(0, layoutCount);
        layout.name += random;
        switch (random)
        {
            case 0:
                #region layout0
                #endregion
                break;
            case 1:
                #region layout1
                fields[0, 0].item = new Rock(fields[0, 0].field); fields[0, 1].item = new Rock(fields[0, 1].field); fields[0, 2].item = new Rock(fields[0, 2].field); fields[0, 3].item = new Rock(fields[0, 3].field); fields[0, 7].item = new Rock(fields[0, 7].field); fields[0, 8].item = new Rock(fields[0, 8].field); fields[0, 9].item = new Rock(fields[0, 9].field); fields[0, 10].item = new Rock(fields[0, 10].field); fields[1, 0].item = new Rock(fields[1, 0].field); fields[1, 1].item = new Rock(fields[1, 1].field); fields[1, 2].item = new Rock(fields[1, 2].field); fields[1, 3].item = new Rock(fields[1, 3].field); fields[1, 7].item = new Rock(fields[1, 7].field); fields[1, 8].item = new Rock(fields[1, 8].field); fields[1, 9].item = new Rock(fields[1, 9].field); fields[1, 10].item = new Rock(fields[1, 10].field); fields[9, 0].item = new Rock(fields[9, 0].field); fields[9, 1].item = new Rock(fields[9, 1].field); fields[9, 2].item = new Rock(fields[9, 2].field); fields[9, 3].item = new Rock(fields[9, 3].field); fields[9, 7].item = new Rock(fields[9, 7].field); fields[9, 8].item = new Rock(fields[9, 8].field); fields[9, 9].item = new Rock(fields[9, 9].field); fields[9, 10].item = new Rock(fields[9, 10].field); fields[10, 0].item = new Rock(fields[10, 0].field); fields[10, 1].item = new Rock(fields[10, 1].field); fields[10, 2].item = new Rock(fields[10, 2].field); fields[10, 3].item = new Rock(fields[10, 3].field); fields[10, 7].item = new Rock(fields[10, 7].field); fields[10, 8].item = new Rock(fields[10, 8].field); fields[10, 9].item = new Rock(fields[10, 9].field); fields[10, 10].item = new Rock(fields[10, 10].field);

                #endregion
                break;
            case 2:
                #region layout2
                fields[0, 0].item = new Rock(fields[0, 0].field); fields[0, 1].item = new Rock(fields[0, 1].field); fields[0, 2].item = new Rock(fields[0, 2].field); fields[0, 3].item = new Rock(fields[0, 3].field); fields[0, 4].item = new Rock(fields[0, 4].field); fields[0, 6].item = new Rock(fields[0, 6].field); fields[0, 7].item = new Rock(fields[0, 7].field); fields[0, 8].item = new Rock(fields[0, 8].field); fields[0, 9].item = new Rock(fields[0, 9].field); fields[0, 10].item = new Rock(fields[0, 10].field); fields[1, 0].item = new Rock(fields[1, 0].field); fields[1, 1].item = new Rock(fields[1, 1].field); fields[1, 2].item = new Rock(fields[1, 2].field); fields[1, 3].item = new Rock(fields[1, 3].field); fields[1, 7].item = new Rock(fields[1, 7].field); fields[1, 8].item = new Rock(fields[1, 8].field); fields[1, 9].item = new Rock(fields[1, 9].field); fields[1, 10].item = new Rock(fields[1, 10].field); fields[2, 0].item = new Rock(fields[2, 0].field); fields[2, 1].item = new Rock(fields[2, 1].field); fields[2, 2].item = new Rock(fields[2, 2].field); fields[2, 8].item = new Rock(fields[2, 8].field); fields[2, 9].item = new Rock(fields[2, 9].field); fields[2, 10].item = new Rock(fields[2, 10].field); fields[3, 0].item = new Rock(fields[3, 0].field); fields[3, 1].item = new Rock(fields[3, 1].field); fields[3, 9].item = new Rock(fields[3, 9].field); fields[3, 10].item = new Rock(fields[3, 10].field); fields[4, 0].item = new Rock(fields[4, 0].field); fields[4, 10].item = new Rock(fields[4, 10].field); fields[6, 0].item = new Rock(fields[6, 0].field); fields[6, 10].item = new Rock(fields[6, 10].field); fields[7, 0].item = new Rock(fields[7, 0].field); fields[7, 1].item = new Rock(fields[7, 1].field); fields[7, 9].item = new Rock(fields[7, 9].field); fields[7, 10].item = new Rock(fields[7, 10].field); fields[8, 0].item = new Rock(fields[8, 0].field); fields[8, 1].item = new Rock(fields[8, 1].field); fields[8, 2].item = new Rock(fields[8, 2].field); fields[8, 8].item = new Rock(fields[8, 8].field); fields[8, 9].item = new Rock(fields[8, 9].field); fields[8, 10].item = new Rock(fields[8, 10].field); fields[9, 0].item = new Rock(fields[9, 0].field); fields[9, 1].item = new Rock(fields[9, 1].field); fields[9, 2].item = new Rock(fields[9, 2].field); fields[9, 3].item = new Rock(fields[9, 3].field); fields[9, 7].item = new Rock(fields[9, 7].field); fields[9, 8].item = new Rock(fields[9, 8].field); fields[9, 9].item = new Rock(fields[9, 9].field); fields[9, 10].item = new Rock(fields[9, 10].field); fields[10, 0].item = new Rock(fields[10, 0].field); fields[10, 1].item = new Rock(fields[10, 1].field); fields[10, 2].item = new Rock(fields[10, 2].field); fields[10, 3].item = new Rock(fields[10, 3].field); fields[10, 4].item = new Rock(fields[10, 4].field); fields[10, 6].item = new Rock(fields[10, 6].field); fields[10, 7].item = new Rock(fields[10, 7].field); fields[10, 8].item = new Rock(fields[10, 8].field); fields[10, 9].item = new Rock(fields[10, 9].field); fields[10, 10].item = new Rock(fields[10, 10].field);

                #endregion
                break;
            case 3:
                #region layout3
                fields[4, 4].item = new Rock(fields[4, 4].field); fields[4, 5].item = new Rock(fields[4, 5].field); fields[4, 6].item = new Rock(fields[4, 6].field); fields[5, 4].item = new Rock(fields[5, 4].field); fields[5, 6].item = new Rock(fields[5, 6].field); fields[6, 4].item = new Rock(fields[6, 4].field); fields[6, 5].item = new Rock(fields[6, 5].field); fields[6, 6].item = new Rock(fields[6, 6].field);
                #endregion
                break;
            case 4:
                #region layout4
                fields[1, 1].item = new Rock(fields[1, 1].field); fields[1, 2].item = new Rock(fields[1, 2].field); fields[1, 3].item = new Rock(fields[1, 3].field); fields[1, 7].item = new Rock(fields[1, 7].field); fields[1, 8].item = new Rock(fields[1, 8].field); fields[1, 9].item = new Rock(fields[1, 9].field); fields[2, 1].item = new Rock(fields[2, 1].field); fields[2, 2].item = new Rock(fields[2, 2].field); fields[2, 8].item = new Rock(fields[2, 8].field); fields[2, 9].item = new Rock(fields[2, 9].field); fields[3, 1].item = new Rock(fields[3, 1].field); fields[3, 9].item = new Rock(fields[3, 9].field); fields[7, 1].item = new Rock(fields[7, 1].field); fields[7, 9].item = new Rock(fields[7, 9].field); fields[8, 1].item = new Rock(fields[8, 1].field); fields[8, 2].item = new Rock(fields[8, 2].field); fields[8, 8].item = new Rock(fields[8, 8].field); fields[8, 9].item = new Rock(fields[8, 9].field); fields[9, 1].item = new Rock(fields[9, 1].field); fields[9, 2].item = new Rock(fields[9, 2].field); fields[9, 3].item = new Rock(fields[9, 3].field); fields[9, 7].item = new Rock(fields[9, 7].field); fields[9, 8].item = new Rock(fields[9, 8].field); fields[9, 9].item = new Rock(fields[9, 9].field);

                #endregion
                break;
            case 5:
                #region layout5
                fields[0, 2].item = new Rock(fields[0, 2].field); fields[0, 3].item = new Rock(fields[0, 3].field); fields[0, 7].item = new Rock(fields[0, 7].field); fields[0, 8].item = new Rock(fields[0, 8].field); fields[1, 1].item = new Rock(fields[1, 1].field); fields[1, 2].item = new Rock(fields[1, 2].field); fields[1, 3].item = new Rock(fields[1, 3].field); fields[1, 7].item = new Rock(fields[1, 7].field); fields[1, 8].item = new Rock(fields[1, 8].field); fields[1, 9].item = new Rock(fields[1, 9].field); fields[2, 0].item = new Rock(fields[2, 0].field); fields[2, 1].item = new Rock(fields[2, 1].field); fields[2, 2].item = new Rock(fields[2, 2].field); fields[2, 8].item = new Rock(fields[2, 8].field); fields[2, 9].item = new Rock(fields[2, 9].field); fields[2, 10].item = new Rock(fields[2, 10].field); fields[3, 0].item = new Rock(fields[3, 0].field); fields[3, 1].item = new Rock(fields[3, 1].field); fields[3, 2].item = new Rock(fields[3, 2].field); fields[3, 8].item = new Rock(fields[3, 8].field); fields[3, 9].item = new Rock(fields[3, 9].field); fields[3, 10].item = new Rock(fields[3, 10].field); fields[7, 0].item = new Rock(fields[7, 0].field); fields[7, 1].item = new Rock(fields[7, 1].field); fields[7, 2].item = new Rock(fields[7, 2].field); fields[7, 8].item = new Rock(fields[7, 8].field); fields[7, 9].item = new Rock(fields[7, 9].field); fields[7, 10].item = new Rock(fields[7, 10].field); fields[8, 0].item = new Rock(fields[8, 0].field); fields[8, 1].item = new Rock(fields[8, 1].field); fields[8, 2].item = new Rock(fields[8, 2].field); fields[8, 8].item = new Rock(fields[8, 8].field); fields[8, 9].item = new Rock(fields[8, 9].field); fields[8, 10].item = new Rock(fields[8, 10].field); fields[9, 1].item = new Rock(fields[9, 1].field); fields[9, 2].item = new Rock(fields[9, 2].field); fields[9, 3].item = new Rock(fields[9, 3].field); fields[9, 7].item = new Rock(fields[9, 7].field); fields[9, 8].item = new Rock(fields[9, 8].field); fields[9, 9].item = new Rock(fields[9, 9].field); fields[10, 2].item = new Rock(fields[10, 2].field); fields[10, 3].item = new Rock(fields[10, 3].field); fields[10, 7].item = new Rock(fields[10, 7].field); fields[10, 8].item = new Rock(fields[10, 8].field);

                #endregion
                break;
        }
    }
}
