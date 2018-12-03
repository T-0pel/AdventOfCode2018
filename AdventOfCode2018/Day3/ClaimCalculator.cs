using SharedLibrary;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public static class ClaimCalculator
    {
        private const string DirectoryName = "Day3";
        private static HashSet<int>[,] _fabricArrayWithClaims;

        public static int OverlappingFabricInches()
        {
            var fabricArray = GetFabricArrayWithClaimIds();

            return fabricArray.Cast<HashSet<int>>().Count(i => i.Count > 1);
        }

        public static int GetClaimWithoutOverlap()
        {
            var fabricArray = GetFabricArrayWithClaimIds();

            fabricArray.Cast<HashSet<int>>().Where(i => i.Count > 1);
        }

        private static HashSet<int>[,] GetFabricArrayWithClaimIds()
        {
            if (_fabricArrayWithClaims == null)
            {
                var lines = FileHelper.GetLines(DirectoryName);
                var claims = lines.Select(l => new Claim(l)).ToList();

                var arrayWidth = claims.Max(c => c.LeftEdge + c.Width);
                var arrayHeight = claims.Max(c => c.TopEdge + c.Height);

                var fabricArray = new HashSet<int>[arrayWidth, arrayHeight];

                for (var i = 0; i < arrayWidth; i++)
                {
                    for (var j = 0; j < arrayHeight; j++)
                    {
                        fabricArray[i, j] = new HashSet<int>();
                    }
                }

                foreach (var claim in claims)
                {
                    for (var i = 0; i < claim.Width; i++)
                    {
                        for (var j = 0; j < claim.Height; j++)
                        {
                            fabricArray[i + claim.LeftEdge, j + claim.TopEdge].Add(claim.Id);
                        }
                    }
                }

                _fabricArrayWithClaims = fabricArray;
            }
            
            return _fabricArrayWithClaims;
        }
    }
}