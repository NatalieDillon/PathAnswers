namespace PathExercises.Classes
{
    public class Vertex : IEquatable<Vertex>
    {
        // Properties
        public string Name { get; set; }

        public bool Visited { get; set; }

        public double Distance { get; set; }

        public Vertex? PreviousVertex { get; set; }

        // Constructor
        public Vertex(string name)
        {
            Name = name;
            Visited = false;
            Distance = double.PositiveInfinity;
        }


        // Public methods
        public void Reset()
        {
            Visited = false;
            Distance = double.PositiveInfinity;
            PreviousVertex = null;
        }

        public bool Equals(Vertex? other)
        {
            if (other != null)
            {
                return Name.Equals(other.Name);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vertex otherVertex)
            {
                return Equals(otherVertex);
            }
            return false;
        }

        public static bool operator ==(Vertex? lhs, Vertex? rhs)
        {
            if (lhs is null)
            {
                return rhs is null;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vertex? lhs, Vertex? rhs)
        {
            return !(lhs == rhs);
        }
    }
}