using SecurityWebhoook.Lib.Services.SharedServices;
using System.Diagnostics;
using System.Text;

public class SemgrepScanner
{
    private readonly IAPIHandler _apiHandler;

    public SemgrepScanner(IAPIHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    private static string ConvertWindowsPathToWsl(string windowsPath)
    {
        // Convert something like C:\path\to\repo to /mnt/c/path/to/repo for WSL
        string wslPath = windowsPath.Replace(@"\", "/").Replace("C:", "/mnt/c");
        return wslPath;
    }

    public async Task InitializeCode()
    {
        // Define the path to the code directory you want to scan
        string codePath = @"C:\Users\VRUSHAL\source\repos\SecurityWebhookAPI"; // Modify as needed
        string wslCodePath = ConvertWindowsPathToWsl(codePath);

        // Define the path to save the semgrep output as a JSON file
        //string tempJsonFilePath = $"{wslCodePath}/semgrep_output.json";

        // Define the API URL to send results
        string apiUrl = "https://smee.io/ienQpMHjWzcGcQve"; // Modify with your API URL

        // Define the path to store the JSON output temporarily
        string tempJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "semgrep_output.json");

        // Run Semgrep and get the results as JSON
        string jsonOutput = await RunSemgrepAsync(wslCodePath, tempJsonFilePath);

        // If output is not null, send it to the API
        if (!string.IsNullOrEmpty(jsonOutput))
        {
            await SendJsonToApiAsync(apiUrl, jsonOutput);

            // Delete the JSON file after sending the results
            DeleteJsonFile(tempJsonFilePath);
        }

        Console.WriteLine("Semgrep scan completed and results sent to API.");
    }

    private async Task<string> RunSemgrepAsync(string codePath, string jsonFilePath)
    {
        try
        {
            string semgrepCommand = $"cd {codePath} && semgrep ci --json --output=semgrep_output.json";

            // Start the process in PowerShell to run the semgrep command on WSL
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "wsl", // WSL command to execute Linux commands
                Arguments = semgrepCommand, // Semgrep command to run
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process
            using (var process = Process.Start(processStartInfo))
            {
                if (process != null)
                {
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    // Wait for the process to complete
                    await process.WaitForExitAsync();

                    

                    // Return the contents of the JSON file
                    if (File.Exists(jsonFilePath))
                    {
                        return await File.ReadAllTextAsync(jsonFilePath);
                    }
                    else
                    {
                        throw new FileNotFoundException("The JSON output file was not found after scanning.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Failed to start Semgrep process.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running Semgrep: {ex.Message}");
            return null;
        }
    }

    private async Task SendJsonToApiAsync(string apiUrl, string jsonData)
    {
        try
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                Console.WriteLine("No data to send to API.");
                return;
            }

            // Create the JSON content to send in the HTTP POST request
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send the POST request to the API
            var response = await _apiHandler.PostAsync<object>(jsonData, "", apiUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending data to API: {ex.Message}");
        }
    }

    private static void DeleteJsonFile(string jsonFilePath)
    {
        try
        {
            // Check if the file exists before attempting to delete it
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
                Console.WriteLine("JSON file deleted for better performance.");
            }
            else
            {
                Console.WriteLine("JSON file not found to delete.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting JSON file: {ex.Message}");
        }
    }
}
