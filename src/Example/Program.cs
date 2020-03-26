using System;
using Octokit;

namespace Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("Example"));

            var closedIssuesRequest = new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending,
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed
            };

            var options = new ApiOptions
            {
                PageSize = 10,
                StartPage = 1
            };

            var issues = client.Issue.GetAllForRepository("WireMock-Net", "WireMock.Net", closedIssuesRequest, options).Result;
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