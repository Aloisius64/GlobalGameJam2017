using System;
using System.Collections.Generic;

class LevelGenerator
{
    public int width { get; set; }
    public int height { get; set; }
    public int how_many { get; set; }

    public HashSet<Point> getLevel()
    {
        HashSet<Point> level = new HashSet<Point>();

        Random random = new Random();

        for (int i = 0; i < how_many; i++)
        {
            level.Add(new Point(random.Next(width / 2), random.Next(height / 2)));
        }

        // Simmetric
        foreach (var item in level)
        {
            level.Add(new Point(-item.x, item.y));
        }

        // Translation
        foreach (var item in level)
        {
            item.x = item.x + width / 2;
        }

        return level;
    }
}