Update()
{
    int multiplier = 1;
    int step = 10;
    double reduction = energy / step;
    do
    {
        current_energy = (energy * multiplier);
        multiplier = -multiplier;
        energy -= reduction;
    } while (energy > 0);

}