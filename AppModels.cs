
public class StateDTO
{
    public string state { get; set; }

    public double[][] border { get; set; }
}

//the reference algo really likes these PointF objects.
public class PointF
{
    public double X { get; set; }

    public double Y { get; set; }
}

public class InputObject
{
    public string latitude { get; set;}
    public string longitude {get; set;}
}