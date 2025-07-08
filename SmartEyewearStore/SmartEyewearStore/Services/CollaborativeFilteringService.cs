using SmartEyewearStore.Models;

namespace SmartEyewearStore.Services
{
    public class CollaborativeFilteringService
    {
        // Build interaction matrix: key is userId or guestId, value is dictionary of glassId -> score
        public Dictionary<string, Dictionary<int, int>> BuildInteractionMatrix(List<UserInteraction> interactions)
        {
            var matrix = new Dictionary<string, Dictionary<int, int>>();
            foreach (var inter in interactions)
            {
                var userKey = inter.UserId?.ToString() ?? inter.GuestId;
                if (string.IsNullOrEmpty(userKey))
                    continue;

                if (!matrix.TryGetValue(userKey, out var cols))
                {
                    cols = new Dictionary<int, int>();
                    matrix[userKey] = cols;
                }

                cols[inter.GlassId] = inter.Score ?? 0;
            }
            return matrix;
        }

        private List<int> BuildVector(Dictionary<int, int> userInteractions, List<int> glassIds)
        {
            var vector = new List<int>(glassIds.Count);
            foreach (var gId in glassIds)
            {
                vector.Add(userInteractions.TryGetValue(gId, out var score) ? score : 0);
            }
            return vector;
        }

        private double CalculateCosineSimilarity(List<int> vectorA, List<int> vectorB)
        {
            double dot = 0;
            double magA = 0;
            double magB = 0;
            for (int i = 0; i < vectorA.Count; i++)
            {
                dot += vectorA[i] * vectorB[i];
                magA += vectorA[i] * vectorA[i];
                magB += vectorB[i] * vectorB[i];
            }
            if (magA == 0 || magB == 0) return 0;
            return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
        }

        public List<string> GetTopSimilarUsers(string targetKey, List<UserInteraction> allInteractions, int topN = 5)
        {
            var matrix = BuildInteractionMatrix(allInteractions);
         
            if (!matrix.TryGetValue(targetKey, out var targetCols))
                return new List<string>();

            var glassIds = allInteractions.Select(i => i.GlassId).Distinct().ToList();
            var targetVector = BuildVector(targetCols, glassIds);

            var similarities = new List<(string userKey, double sim)>();
            foreach (var kv in matrix)
            {
                if (kv.Key == targetKey) continue;
                var vec = BuildVector(kv.Value, glassIds);
                double sim = CalculateCosineSimilarity(targetVector, vec);
                similarities.Add((kv.Key, sim));
            }

            return similarities
                .OrderByDescending(s => s.sim)
                .Take(topN)
                .Select(s => s.userKey)
                .ToList();
        }

        public List<int> GetRecommendedGlassIds(string targetKey, List<UserInteraction> allInteractions, List<string> topSimilarUsers, int topN = 10)
        {
            var targetGlasses = allInteractions
                .Where(i => (i.UserId?.ToString() ?? i.GuestId) == targetKey)
                .Select(i => i.GlassId)
                .ToHashSet();

            var scores = new Dictionary<int, double>();
            foreach (var simUserKey in topSimilarUsers)
            {
                foreach (var inter in allInteractions.Where(i => (i.UserId?.ToString() ?? i.GuestId) == simUserKey))
                {
                    if (targetGlasses.Contains(inter.GlassId))
                        continue;
                    if (!scores.ContainsKey(inter.GlassId))
                        scores[inter.GlassId] = 0;
                    scores[inter.GlassId] += inter.Score ?? 0;
                }
            }

            return scores.OrderByDescending(kv => kv.Value)
                         .Take(topN)
                         .Select(kv => kv.Key)
                         .ToList();
        }
    }
}