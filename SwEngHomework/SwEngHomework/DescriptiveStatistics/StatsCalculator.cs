namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        public Stats Calculate(string semicolonDelimitedContributions)
        {
           var data = semicolonDelimitedContributions.Split(';');
           var contributions = new List<double>();
            foreach (var contributionStr in data) 
            {
                var contributionNumber = contributionStr.Trim().TrimStart('$').Replace(",","").Trim();

                if(double.TryParse(contributionNumber, out double contribution))
                {
                    contributions.Add(contribution);
                }

            }

            var stats = new Stats();
            stats.Median = Median(contributions);
            stats.Average = Average(contributions);
            stats.Range = Range(contributions);

            return stats;
        }

        private double Average(List<double> dataList)
        {
            if(dataList.Count == 0)
            {
                return 0;
            }
            else if (dataList.Count == 1)
            {
                return dataList[0];
            }
            var ave = dataList.Sum(x => x)/dataList.Count;

            return Math.Round(ave, 2);
        }

 
        private double Median(List<double> dataList)
        {
            if (dataList.Count == 0)
            {
                return 0;
            } 
            else if (dataList.Count == 1)
            {
                return dataList[0];
            }
            var xs = dataList.ToArray();
            Array.Sort(xs);

            if(xs.Length % 2== 0)
            {
                var media1 = xs[xs.Length / 2 -1];
                var media2 = xs[xs.Length / 2];
                return Math.Round((media1 + media2)/2, 2);
            } else
            {
                var media = xs[xs.Length / 2];
                return Math.Round(media, 2);
            }
          
        }

        private double Range(List<double> dataList)
        {
            if (dataList.Count == 0 || dataList.Count == 1)
            {
                return 0;
            }

            var min = dataList.Min();
            var max = dataList.Max();

            return max - min;
        }
    }
}
