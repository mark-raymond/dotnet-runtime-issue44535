using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CertIssueRepro
{
    internal static class Program
    {
        private static void Main()
        {
            var dir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var cert = new X509Certificate2(Path.Combine(dir, "ewallet.p12"), "issue44535");
            Console.WriteLine(cert.Thumbprint);
        }
    }
}
