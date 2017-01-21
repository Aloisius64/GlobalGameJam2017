using System;
using System.Collections.Generic;

public class AI
{

    public int direction { get; set; }

    public void run()
    { // This should be executed every N seconds

        Player me = getMyPlayer();
        List<Item> items = getItems();

        direction = getDirection(me, items);

    }

    private int getDirection(Player me, List<Item> items)
    {

        double min_distance = Double.MinValue;
        Item min_item = null;
        foreach (var item in items)
        {
            double distance = getDistance(me.x, me.y, item.x, item.y);

            if (distance < min_distance)
            {
                min_distance = distance;
                min_item = item;
            }

        }

        if (min_item != null)
        {
            double angle = getAngle(me.x, me.y, min_item.x, min_item.y);

            return (int)(angle / (Math.PI / 4));
        }

        return 0;
    }

    private double getAngle(int x1, int y1, int x2, int y2)
    {

        double angle = Math.Atan2(y2 - y1, x2 - x1);
        if (angle < 0) angle += 2 * Math.PI;

        return angle;

    }

    private double getDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
}