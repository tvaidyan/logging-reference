# logging-reference
A bare-bones example demonstrating logging with Serilog, ApplicationInsights and a global exception handler implemented as an ASP.NET Core filter.

There are two .NET Core projects in this solution:

1. LoggingReference.Web: An ASP.NET application with two endpoints, both generating log entries to a log file and to Application Insights (note: remember to specify your InstrumentationKey in appsettings.json files for logging to ApplicationInsights).

/api/hello-world/generate-error/[abc123] - causes an uncaught exception which is then handled by the global exception filter.
/api/hello-world/generate-log/[abc123] - generates a set of log entries.

Replace "abc123" with any string token that you like.

2. LoggingReference.Worker: A .NET Core Worker Service application to demonstrate how to integrate Serilog and ApplicationInsights in such a project type.