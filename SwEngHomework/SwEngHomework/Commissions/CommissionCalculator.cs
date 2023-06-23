using Newtonsoft.Json;

namespace SwEngHomework.Commissions
{
    public class CommissionCalculator : ICommissionCalculator
    {
        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            var report = new Dictionary<string, double>();
            var data = JsonConvert.DeserializeObject<Data>(jsonInput);

            if(data != null)
            {
                var advisors = data.Advisors;
                var accounts = data.Accounts;

                if(advisors != null && accounts != null)
                {
                    foreach(var advisor in advisors)
                    {
                        var name = advisor.Name;
                        report.Add(name, 0.00);
                        var level = advisor.Level;
                        var theAccounts = accounts.Where(x => x.Advisor == name);
                        if(theAccounts != null)
                        {
                            var presentValues = theAccounts.Select(x => x.PresentValue);
                            foreach (var presentValue in presentValues)
                            {
                                var commission = GetCommission(level, presentValue);
                                if (report.ContainsKey(name))
                                {
                                    var value = report[name];
                                    report[name] = value + commission;
                                }
                               
                            }
                        }
                                      
                       
                    }
                }
            }

            return report;
        }

        private double GetCommission(string level, long presentValue)
        {
            double bps;

            if(presentValue < 50000 )
            {
                bps = 0.05;
            }
            else if (presentValue < 100000)
            {
                bps = 0.06;
            } 
            else
            {
                bps = 0.07;
            }

            double percent;
            switch (level)
            {
                case "Junior":
                    percent = 0.25;
                    break;
                case "Experienced":
                    percent = 0.5;
                    break;
                case "Senior":
                    percent = 1.0;
                    break;
                default:
                    percent = 0.0;
                    break;

            }

            return Math.Round(presentValue * percent * bps/100.0,2);
        }
    }

    public class Advisor
    {
        public string Name { get; set; }
        public string Level { get; set; }
    }

    public enum Level
    {
        Senior,
        Experienced,
        Junior
    }

    public class Account
    {
        public string Advisor { get; set; }
        public long PresentValue { get; set; }
    }

    public class Data
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Advisor> Advisors { get; set; } = new List<Advisor>();
    }

}
