using System.Diagnostics;

namespace SecurityWebhoook.Lib.Services.Instruments
{
    public class SonarScanner
    {

        public void RunSonarScan(string projectKey, string projectName, string projectVersion, string sourceDirectory, string sonarScannerPath, string sonarHostUrl, string sonarToken)
        {
            
            var arguments = $"-Dsonar.projectKey={projectKey} " +
                            $"-Dsonar.projectName={projectName} " +
                            $"-Dsonar.projectVersion={projectVersion} " +
                            $"-Dsonar.sources={sourceDirectory} " +
                            $"-Dsonar.host.url={sonarHostUrl} " +
                            $"-Dsonar.login={sonarToken}";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = sonarScannerPath,   
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                    process.ErrorDataReceived += (sender, e) => Console.WriteLine("ERROR: " + e.Data);

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("SonarQube scan completed successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"SonarQube scan failed with exit code {process.ExitCode}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while running the SonarQube scan: " + ex.Message);
            }
        }




    }

}