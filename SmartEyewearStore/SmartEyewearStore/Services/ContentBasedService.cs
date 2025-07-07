using SmartEyewearStore.Models;

namespace SmartEyewearStore.Services
{
    public class ContentBasedService
    {
        public List<Glasses> GetRecommendedGlasses(SurveyAnswer userProfile, List<Glasses> allGlasses, int topN = 10)
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

            var userVector = BuildUserVector(userProfile, map);

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