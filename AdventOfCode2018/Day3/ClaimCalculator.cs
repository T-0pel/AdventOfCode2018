using SharedLibrary;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    public static class ClaimCalculator
    {
        private const string DirectoryName = "Day3";
        private static HashSet<int>[,] _fabricArrayWithClaimIds;
        private static List<Claim> _claims;

        public static int OverlappingFabricInches()
        {
            var fabricArray = GetFabricArrayWithClaimIds();

            return fabricArray.Cast<HashSet<int>>().Count(i => i != null && i.Count > 1);
        }

        public static int GetClaimWithoutOverlap()
        {
            var fabricArray = GetFabricArrayWithClaimIds();

            var idsWithoutOverlap = fabricArray.Cast<HashSet<int>>().Where(i => i != null && i.Count == 1);

            var uniqueIds = new HashSet<int>();
            foreach (var ids in idsWithoutOverlap)
            {
                uniqueIds.UnionWith(ids);
            }

            foreach (var uniqueId in uniqueIds)
            {
                var claim = _claims.First(c => c.Id == uniqueId);
                var isOverlapped = false;

                for (var i = 0; i < claim.Width; i++)
                {
                    for (var j = 0; j < claim.Height; j++)
                    {
                        isOverlapped = fabricArray[i + claim.LeftEdge, j + claim.TopEdge].Count > 1;
                        if (isOverlapped) break;
                    }

                    if (isOverlapped) break;
                }

                if (!isOverlapped)
                {
                    return claim.Id;
                }
            }

            return -1;
        }

        private static HashSet<int>[,] GetFabricArrayWithClaimIds()
        {
            if (_fabricArrayWithClaimIds == null)
            {
                var lines = FileHelper.GetLines(DirectoryName);
                _claims = lines.Select(l => new Claim(l)).ToList();

                var arrayWidth = _claims.Max(c => c.LeftEdge + c.Width);
                var arrayHeight = _claims.Max(c => c.TopEdge + c.Height);

                var fabricArray = new HashSet<int>[arrayWidth, arrayHeight];

                foreach (var claim in _claims)
                {
                    for (var i = 0; i < claim.Width; i++)
                    {
                        for (var j = 0; j < claim.Height; j++)
                        {
                            var horizontalIndex = i + claim.LeftEdge;
                            var verticalIndex = j + claim.TopEdge;

                            if (fabricArray[horizontalIndex, verticalIndex] == null)
                            {
                                fabricArray[horizontalIndex, verticalIndex] = new HashSet<int>();
                            }

                            fabricArray[horizontalIndex, verticalIndex].Add(claim.Id);
                        }
                    }
                }

                _fabricArrayWithClaimIds = fabricArray;
            }
            
            return _fabricArrayWithClaimIds;
        }
    }
}