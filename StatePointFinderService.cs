namespace state_server
{
    public interface IStatePointFinderService
    {
        bool IsPointInPolygon(PointF[] polygon, PointF point);
    }

    public class StatePointFinderService : IStatePointFinderService
    {
        public bool IsPointInPolygon(PointF[] polygon, PointF point)
        {

            //lat is Y
            //long is X
            bool isInside = false;

            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                //The != is to deal with the vertex-edge case. 
                //Edge cases (no pun intended) like this are more common in simplified data sets.
                //The reference implenention of this insists on X/Y instead of lat-long, I didn't see 
                //the harm in passing it through this way
                if ((
                    (polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    //flips between true (odd number of crosses) or false (even number of crosses)
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}