using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DevDragon.AV.Client.Tests
{
    [TestFixture]
    public class ScanningTests
    {
        private IConfiguration Configuration { get; set; }
        private AntivirusSettings Settings { get; set; }

        [OneTimeSetUp]
        public void ReadConfiguration()
        {
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .AddJsonFile("testsettings.Development.json", optional: true)
                .Build();

            this.Settings = this.Configuration.GetSection("DevDragon.AV").Get<AntivirusSettings>();
        }

        [Test]
        public async Task PositiveTestScan()
        {
            var client = new AntivirusClient(this.Settings.AccessKey, false);
            var result = await client.ScanFile(GetSampleFilePath());

            Assert.That(result.HasVirus, Is.EqualTo(false));
        }

        [Test]
        public void HandlingInvalidKeyExceptionTest()
        {
            var client = new AntivirusClient("invalidkey", false);
            var ex = Assert.ThrowsAsync<FileScanException>(async () => 
            {
                var result = await client.ScanFile(GetSampleFilePath());
            });

            Assert.That(ex.Message.StartsWith("Authorisation error: "));
        }

        private string GetSampleFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "sampleFile.jpg");
        }
    }
}
