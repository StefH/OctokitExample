using System;
using Octokit;

namespace Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("Example"));
            client.Credentials = new Credentials("442124b303ddd906e4163b5fddb7caf7bf561d38");

            var miscellaneousRateLimit = client.Miscellaneous.GetRateLimits().GetAwaiter().GetResult();

            var u = client.User.Email.GetAll().GetAwaiter().GetResult();

            var closedIssuesRequest = new RepositoryIssueRequest
            {
                //SortDirection = SortDirection.Ascending,
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed
            };

            var options = new ApiOptions
            {
                // PageSize = 500,
                StartPage = 1,
                //PageCount = 10
            };

            var issues = client.Issue.GetAllForRepository("WireMock-Net", "WireMock.Net", closedIssuesRequest).GetAwaiter().GetResult();
            Console.WriteLine($"{issues.Count} found");

            var info = client.GetLastApiInfo();
            Console.WriteLine("GetLastApiInfo:");
            Console.WriteLine(info.RateLimit.Limit);
            Console.WriteLine(info.RateLimit.Remaining);
            Console.WriteLine(info.RateLimit.Reset);
            Console.WriteLine(info.RateLimit.ResetAsUtcEpochSeconds);
        }
    }
}