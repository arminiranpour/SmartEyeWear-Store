using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
namespace SmartEyewearStore.Services
{
    public class HybridRecommendationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HybridRecommendationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<VariantRecommendation> GetHybridRecommendationsWithScores(
            SurveyAnswer userProfile,
            List<UserInteraction> allInteractions,
            List<ProductVariant> allVariants,
            ContentBasedService contentService,
            CollaborativeFilteringService collabService,
            int topN = 10,
            double alpha = 0.6,
            double beta = 0.4)
        {
            var guestId = _httpContextAccessor.HttpContext?.Session.GetString("GuestId");
            string targetKey = userProfile.UserId > 0
                                ? userProfile.UserId.ToString()
                                : guestId;

            var userInteractions = allInteractions
                .Where(i => (i.UserId?.ToString() ?? i.GuestId) == targetKey)
                .ToList();

            var contentResults = contentService
                .GetRecommendedVariantsWithScores(userProfile, allVariants, userInteractions, topN);
            var contentDict = contentResults.ToDictionary(r => r.Variant.VariantId, r => r.Score);
            double maxContent = contentDict.Values.DefaultIfEmpty(0).Max();

            var topUsers = collabService.GetTopSimilarUsers(targetKey, allInteractions);
            var collabIds = collabService.GetRecommendedVariantIds(targetKey, allInteractions, topUsers, topN);
            var collabScores = new Dictionary<int, double>();
            for (int i = 0; i < collabIds.Count; i++)
            {
                collabScores[collabIds[i]] = collabIds.Count - i;
            }
            double maxCollab = collabScores.Values.DefaultIfEmpty(0).Max();

            var combined = new List<VariantRecommendation>();
            var ids = contentDict.Keys.Union(collabScores.Keys);
            foreach (var id in ids)
            {
                var variant = allVariants.FirstOrDefault(v => v.VariantId == id);
                if (variant == null) continue;
                double cScore = contentDict.TryGetValue(id, out var cs) ? cs : 0;
                double normC = maxContent > 0 ? cScore / maxContent : 0;
                double collScore = collabScores.TryGetValue(id, out var cl) ? cl : 0;
                double normCl = maxCollab > 0 ? collScore / maxCollab : 0;
                double final = alpha * normC + beta * normCl;
                combined.Add(new VariantRecommendation { Variant = variant, Score = final });
            }

            return combined
                .OrderByDescending(r => r.Score)
                .Take(topN)
                .ToList();
        }

        public List<ProductVariant> GetHybridRecommendations(
            SurveyAnswer userProfile,
            List<UserInteraction> allInteractions,
            List<ProductVariant> allVariants,
            ContentBasedService contentService,
            CollaborativeFilteringService collabService,
            int topN = 10,
            double alpha = 0.6,
            double beta = 0.4)
        {
            return GetHybridRecommendationsWithScores(userProfile, allInteractions, allVariants, contentService, collabService, topN, alpha, beta)
                .Select(r => r.Variant)
                .ToList();
        }
    }
}
