using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class ResearchCalculator : IResearchCalculator
    {
        public IList<ResearchResult> Calculate(Research research, IList<ResearchTarget> targets)
        {
            if (targets.Count == 0)
            {
                return new List<ResearchResult>();
            }

            IList<ResearchResult> results = CreateResultsFromTargets(targets);

            NormalizeTargets(targets);
            CalculateFrequency(results, targets);
            CalculateScore(results, targets);
            NormalizeResults(results);

            return results;
        }

        private void NormalizeTargets(IList<ResearchTarget> targets)
        {
            int minScore = targets.Min(t => t.Helium10Value);
            int maxScore = targets.Max(t => t.Helium10Value);

            int min = 1;
            int max = 10000;
            double divider = (maxScore - minScore) / (double)max;

            foreach (var target in targets)
            {
                target.Helium10Value = (int)Math.Round((target.Helium10Value - minScore) / divider);

                if (target.Helium10Value <= 0)
                {
                    target.Helium10Value = min;
                }

                if (target.Helium10Value > max)
                {
                    target.Helium10Value = max;
                }
            }
        }

        private void NormalizeResults(IList<ResearchResult> results)
        {
            int minScore = results.Min(r => r.Score);
            int maxScore = results.Max(r => r.Score);

            int min = 1;
            int max = 10000;
            double divider = (maxScore - minScore) / (double)max;

            foreach (var result in results)
            {
                result.Score = (int)Math.Round((result.Score - minScore) / divider);

                if (result.Score <= 0)
                {
                    result.Score = min;
                }

                if (result.Score > max)
                {
                    result.Score = max;
                }
            }
        }

        private IList<ResearchResult> CreateResultsFromTargets(IList<ResearchTarget> targets)
        {
            IList<ResearchResult> results = new List<ResearchResult>();

            foreach (var target in targets)
            {
                string[] words = target.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] phrases = FindPhrases(words);

                foreach (var phrase in phrases)
                {
                    if (string.IsNullOrWhiteSpace(phrase))
                    {
                        continue;
                    }

                    ResearchResult result;

                    if (results.Any(r => r.Phrase == phrase))
                    {
                        result = results.Single(r => r.Phrase == phrase);
                    }
                    else
                    {
                        result = new ResearchResult();
                        result.ResearchId = target.ResearchId;
                        result.Phrase = phrase;
                        result.WordsCount = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;

                        results.Add(result);
                    }

                    if (result.Phrase == target.Name)
                    {
                        result.IsOriginal = true;
                        result.TargetId = target.Id;
                    }
                }
            }

            return results;
        }

        private void CalculateFrequency(IList<ResearchResult> results, IList<ResearchTarget> targets)
        {
            foreach (var result in results)
            {
                result.Frequency = targets.Count(t => t.Name.Contains(result.Phrase));
            }
        }

        private void CalculateScore(IList<ResearchResult> results, IList<ResearchTarget> targets)
        {
            foreach (var result in results)
            {
                result.Score = targets.Where(t => t.Name.Contains(result.Phrase)).Sum(t => t.Helium10Value);
            }

            int maxWordsCount = results.Max(k => k.WordsCount);

            for (int i = 2; i < maxWordsCount + 1; i++)
            {
                IList<ResearchResult> currentResults = results.Where(k => k.WordsCount == i).ToList();

                foreach (var result in currentResults)
                {
                    string[] words = result.Phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] phrases = FindPhrases(words);

                    foreach (var phrase in phrases)
                    {
                        if (phrase == string.Empty || result.Phrase == phrase)
                        {
                            continue;
                        }

                        ResearchResult phraseResult = results.Single(r => r.Phrase == phrase);
                        result.Score += phraseResult.Score;
                    }
                }
            }
        }

        private string[] FindPhrases(params string[] words)
        {
            if (words.Count() == 0)
            {
                return new string[] { "" };
            }
            else
            {
                string[] oldWords = FindPhrases(words.Skip(1).ToArray());
                string[] newWords = oldWords.Where(word => word == "" || word.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] == words[1])
                                            .Select(word => (words[0] + " " + word).Trim()).ToArray();

                return oldWords.Union(newWords).ToArray();
            }
        }
    }
}
