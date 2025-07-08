using SmartEyewearStore.Models;

namespace SmartEyewearStore.Services
{
    public class ContentBasedService
    {
        private const double SurveyWeight = 0.7;
        private const double InteractionWeight = 0.3;

        public List<Glasses> GetRecommendedGlasses(
            SurveyAnswer userProfile,
            List<Glasses> allGlasses,
            List<UserInteraction>? userInteractions = null,
            int topN = 10)
        {
            var shapes = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.GlassesInfo?.Shape)));
            var colors = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.Color)));
            var styles = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.GlassesInfo?.Style)));
            var materials = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.GlassesInfo?.Material)));
            var headSizes = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.GlassesInfo?.HeadSize)));
            var features = new HashSet<string>(allGlasses.SelectMany(g => SplitValues(g.GlassesInfo?.Features)));

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

            var scored = new List<(Glasses glass, double score)>();
            foreach (var g in allGlasses)
            {
                var vector = BuildGlassVector(g, map);
                var score = CalculateCosineSimilarity(userVector, vector);
                scored.Add((g, score));
            }

            return scored.OrderByDescending(s => s.score).Take(topN).Select(s => s.glass).ToList();
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
                var g = inter.Glass;
                if (g == null) continue;
                Set(SplitValues(g.GlassesInfo?.Shape));
                Set(SplitValues(g.Color));
                Set(SplitValues(g.GlassesInfo?.Style));
                Set(SplitValues(g.GlassesInfo?.Material));
                Set(SplitValues(g.GlassesInfo?.HeadSize));
                Set(SplitValues(g.GlassesInfo?.Features));
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

        private List<int> BuildGlassVector(Glasses glass, Dictionary<string, int> map)
        {
            var vec = new int[map.Count];
            void Set(IEnumerable<string> vals)
            {
                foreach (var v in vals)
                    if (map.TryGetValue(v, out int idx)) vec[idx] = 1;
            }
            Set(SplitValues(glass.GlassesInfo?.Shape));
            Set(SplitValues(glass.Color));
            Set(SplitValues(glass.GlassesInfo?.Style));
            Set(SplitValues(glass.GlassesInfo?.Material));
            Set(SplitValues(glass.GlassesInfo?.HeadSize));
            Set(SplitValues(glass.GlassesInfo?.Features));
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