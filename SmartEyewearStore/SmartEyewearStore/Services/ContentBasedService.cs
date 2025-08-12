using System;
using System.Collections.Generic;
using System.Linq;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;
namespace SmartEyewearStore.Services
{
    public class ContentBasedService
    {
        private const double SurveyWeight = 0.7;
        private const double InteractionWeight = 0.3;

        public List<VariantRecommendation> GetRecommendedVariantsWithScores(
            SurveyAnswer userProfile,
            List<ProductVariant> allVariants,
            List<UserInteraction>? userInteractions = null,
            int topN = 10)
        {
            var shapes = new HashSet<string>(allVariants.SelectMany(v => SplitValues(v.Product.FrameSpecs?.Shape?.Name)));
            var colors = new HashSet<string>(allVariants.SelectMany(v => SplitValues(v.Color?.Name)));
            var styles = new HashSet<string>(allVariants.SelectMany(v => v.Product.ProductTags.Select(pt => pt.Tag.Name)));
            var materials = new HashSet<string>(allVariants.SelectMany(v => SplitValues(v.Product.FrameSpecs?.Material?.Name)));
            var headSizes = new HashSet<string>();
            var features = new HashSet<string>(allVariants.SelectMany(v => v.Product.ProductFeatures.Select(pf => pf.Feature.Label)));

            shapes.UnionWith(SplitValues(userProfile.FavoriteShapes));
            colors.UnionWith(SplitValues(userProfile.Colors));
            styles.Add(userProfile.Style);
            materials.UnionWith(SplitValues(userProfile.Materials));
            if (!string.IsNullOrWhiteSpace(userProfile.HeadSize)) headSizes.Add(userProfile.HeadSize);
            features.UnionWith(SplitValues(userProfile.Features));

            var map = new Dictionary<string, int>();
            void AddRange(IEnumerable<string> vals)
            {
                foreach (var v in vals)
                {
                    if (!map.ContainsKey(v)) map[v] = map.Count;
                }
            }
            AddRange(shapes);
            AddRange(colors);
            AddRange(styles);
            AddRange(materials);
            AddRange(headSizes);
            AddRange(features);

            var surveyVector = BuildUserVector(userProfile, map);
            var interactionVector = BuildInteractionVector(userInteractions, map);
            var userVector = CombineVectors(surveyVector, interactionVector);

            var scored = new List<VariantRecommendation>();
            foreach (var v in allVariants)
            {
                var vector = BuildVariantVector(v, map);
                var score = CalculateCosineSimilarity(userVector, vector);
                scored.Add(new VariantRecommendation { Variant = v, Score = score });
            }

            return scored.OrderByDescending(s => s.Score).Take(topN).ToList();
        }

        public List<ProductVariant> GetRecommendedVariants(
            SurveyAnswer userProfile,
            List<ProductVariant> allVariants,
            List<UserInteraction>? userInteractions = null,
            int topN = 10)
        {
            return GetRecommendedVariantsWithScores(userProfile, allVariants, userInteractions, topN)
            .Select(r => r.Variant)
            .ToList();
        }

        private static IEnumerable<string> SplitValues(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) yield break;
            foreach (var v in raw.Split(',', StringSplitOptions.RemoveEmptyEntries))
                yield return v.Trim();
        }

        private List<int> BuildUserVector(SurveyAnswer profile, Dictionary<string, int> map)
        {
            var vec = new int[map.Count];
            void Set(IEnumerable<string> vals)
            {
                foreach (var v in vals)
                    if (map.TryGetValue(v, out int idx)) vec[idx] = 1;
            }
            Set(SplitValues(profile.FavoriteShapes));
            Set(SplitValues(profile.Colors));
            Set(new[] { profile.Style });
            Set(SplitValues(profile.Materials));
            Set(new[] { profile.HeadSize });
            Set(SplitValues(profile.Features));
            return vec.ToList();
        }

        private List<int> BuildInteractionVector(List<UserInteraction>? interactions, Dictionary<string, int> map)
        {
            var vec = new int[map.Count];
            if (interactions == null) return vec.ToList();

            void Set(IEnumerable<string> vals)
            {
                foreach (var v in vals)
                    if (map.TryGetValue(v, out int idx)) vec[idx] = 1;
            }

            foreach (var inter in interactions)
            {
                var v = inter.Variant;
                if (v == null) continue;
                Set(SplitValues(v.Product.FrameSpecs?.Shape?.Name));
                Set(SplitValues(v.Color?.Name));
                Set(v.Product.ProductTags.Select(pt => pt.Tag.Name));
                Set(SplitValues(v.Product.FrameSpecs?.Material?.Name));
                Set(v.Product.ProductFeatures.Select(pf => pf.Feature.Label));
            }

            return vec.ToList();
        }

        private List<int> CombineVectors(List<int> surveyVector, List<int> interactionVector)
        {
            var result = new int[surveyVector.Count];
            for (int i = 0; i < surveyVector.Count; i++)
            {
                double val = SurveyWeight * surveyVector[i] + InteractionWeight * interactionVector[i];
                result[i] = val >= 0.5 ? 1 : 0;
            }
            return result.ToList();
        }

        private List<int> BuildVariantVector(ProductVariant variant, Dictionary<string, int> map)
        {
            var vec = new int[map.Count];
            void Set(IEnumerable<string> vals)
            {
                foreach (var v in vals)
                    if (map.TryGetValue(v, out int idx)) vec[idx] = 1;
            }
            Set(SplitValues(variant.Product.FrameSpecs?.Shape?.Name));
            Set(SplitValues(variant.Color?.Name));
            Set(variant.Product.ProductTags.Select(pt => pt.Tag.Name));
            Set(SplitValues(variant.Product.FrameSpecs?.Material?.Name));
            Set(variant.Product.ProductFeatures.Select(pf => pf.Feature.Label));
            return vec.ToList();
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
    }
}