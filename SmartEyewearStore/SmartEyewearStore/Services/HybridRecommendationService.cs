using SmartEyewearStore.Models;

namespace SmartEyewearStore.Services
{
    public class HybridRecommendationService
    {
        public List<GlassRecommendation> GetHybridRecommendationsWithScores(
            SurveyAnswer userProfile,
            List<UserInteraction> allInteractions,
            List<Glasses> allGlasses,
            ContentBasedService contentService,
            CollaborativeFilteringService collabService,
            int topN = 10,
            double alpha = 0.6,
            double beta = 0.4)
        {
            string targetKey = userProfile.UserId.ToString();

            // interactions of target user for content based
            var userInteractions = allInteractions
                .Where(i => (i.UserId?.ToString() ?? i.GuestId) == targetKey)
                .ToList();

            var contentResults = contentService
                .GetRecommendedGlassesWithScores(userProfile, allGlasses, userInteractions, topN);
            var contentDict = contentResults.ToDictionary(r => r.Glass.Id, r => r.Score);
            double maxContent = contentDict.Values.DefaultIfEmpty(0).Max();

            // collaborative filtering
            var topUsers = collabService.GetTopSimilarUsers(targetKey, allInteractions);
            var collabIds = collabService.GetRecommendedGlassIds(targetKey, allInteractions, topUsers, topN);
            var collabScores = new Dictionary<int, double>();
            for (int i = 0; i < collabIds.Count; i++)
            {
                collabScores[collabIds[i]] = collabIds.Count - i;
            }
            double maxCollab = collabScores.Values.DefaultIfEmpty(0).Max();

            var combined = new List<GlassRecommendation>();
            var ids = contentDict.Keys.Union(collabScores.Keys);
            foreach (var id in ids)
            {
                var glass = allGlasses.FirstOrDefault(g => g.Id == id);
                if (glass == null) continue;
                double cScore = contentDict.TryGetValue(id, out var cs) ? cs : 0;
                double normC = maxContent > 0 ? cScore / maxContent : 0;
                double collScore = collabScores.TryGetValue(id, out var cl) ? cl : 0;
                double normCl = maxCollab > 0 ? collScore / maxCollab : 0;
                double final = alpha * normC + beta * normCl;
                combined.Add(new GlassRecommendation { Glass = glass, Score = final });
            }

            return combined
                .OrderByDescending(r => r.Score)
                .Take(topN)
                .ToList();
        }

        public List<Glasses> GetHybridRecommendations(
            SurveyAnswer userProfile,
            List<UserInteraction> allInteractions,
            List<Glasses> allGlasses,
            ContentBasedService contentService,
            CollaborativeFilteringService collabService,
            int topN = 10,
            double alpha = 0.6,
            double beta = 0.4)
        {
            return GetHybridRecommendationsWithScores(userProfile, allInteractions, allGlasses, contentService, collabService, topN, alpha, beta)
                .Select(r => r.Glass)
                .ToList();
        }
    }
}
