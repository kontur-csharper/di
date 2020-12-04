using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class LaunchTest
    {
        [Test]
        public void CheckLaunchingOutput()
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "TagCloud.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var reader = process.StandardOutput;
                var output = reader.ReadToEnd();

                output.Should()
                    .Contain("Result saved to:\n")
                    .And.Contain(".png");

                process.WaitForExit();
            }
            var result = Program.Main(new string[0]);
            result.Should().Be(0);
        }
    }
}