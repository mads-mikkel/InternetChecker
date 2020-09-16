using System;
using System.Net;
using System.Threading;

namespace InternetChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INTERNET CHECKER");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss")}: ");
                bool hasInternetAccess;
                try
                {
                    hasInternetAccess = EnvironmentHelpers.HasInternetAccess();
                    if (!hasInternetAccess)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(hasInternetAccess);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                Thread.Sleep(5000);
            }
        }
    }

    public static class EnvironmentHelpers
    {
        public const string Google = "http://google.com/generate_204";
        /// <summary>
        /// Determines whether or not he application has internet access.
        /// </summary>
        /// <remarks>Source: https://stackoverflow.com/questions/2031824/what-is-the-best-way-to-check-for-internet-connectivity-using-net </remarks>
        /// <param name="url">The URL to contact. Default is Google.</param>
        /// <returns>True when there is internet access, false otherwise.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Any exception should cause return false.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Constant parameter chosen, new Uri cannot be a constant on the class.")]
        public static bool HasInternetAccess(string url = Google)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Null, empty or whitespace string value provided.", nameof(url));   // TODO: move argument message to a better place.
            }
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(url))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
