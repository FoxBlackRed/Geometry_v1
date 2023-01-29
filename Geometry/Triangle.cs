using System;

namespace Geometry
{
    public class Triangle : ITriangle
    {
        private double eps = Constants.CalculationAccuracy;

        public Triangle(double edgeA, double edgeB, double edgeC)
        {
            if (edgeA < eps)
                throw new ArgumentException("Side specified incorrectly.", nameof(edgeA));

            if (edgeB < eps)
                throw new ArgumentException("Side specified incorrectly.", nameof(edgeB));

            if (edgeC < eps)
                throw new ArgumentException("Side specified incorrectly.", nameof(edgeC));

            var maxEdge = Math.Max(edgeA, Math.Max(edgeB, edgeC));
            var perimeter = edgeA + edgeB + edgeC;
            if ((perimeter - maxEdge) - maxEdge < Constants.CalculationAccuracy)
                throw new ArgumentException("The longest side of the triangle must be less than the sum of the other sides.");

            EdgeA = edgeA;
            EdgeB = edgeB;
            EdgeC = edgeC;

            _isRightTriangle = new Lazy<bool>(GetIsRightTriangle);
        }

        public double EdgeA { get; }
        public double EdgeB { get; }
        public double EdgeC { get; }

        private readonly Lazy<bool> _isRightTriangle;
        public bool IsRightTriangle => _isRightTriangle.Value;

        private bool GetIsRightTriangle()
        {
            double maxEdge = EdgeA, b = EdgeB, c = EdgeC;
            if (b - maxEdge > Constants.CalculationAccuracy)
                Utils.Swap(ref maxEdge, ref b);

            if (c - maxEdge > Constants.CalculationAccuracy)
                Utils.Swap(ref maxEdge, ref c);

            return Math.Abs(Math.Pow(maxEdge, 2) - Math.Pow(b, 2) - Math.Pow(c, 2)) < Constants.CalculationAccuracy;
        }

        public double GetSquare()
        {
            var halfP = (EdgeA + EdgeB + EdgeC) / 2;
            var square = Math.Sqrt(halfP * (halfP - EdgeA) * (halfP - EdgeB) * (halfP - EdgeC));
            return square;
        }
    }
}