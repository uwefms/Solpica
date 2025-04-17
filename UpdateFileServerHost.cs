using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Path to the .http file
        string httpFilePath = @"PlainBlazor\CallApi_01\CallApi_01\FileServer.http";

        // Path to the WebSrv.txt file
        string webSrvFilePath = @"c:\dev\WebSrv.txt";

        try
        {
            // Read the new host address from WebSrv.txt
            string newHostAddress = File.ReadAllText(webSrvFilePath).Trim();

            // Read the contents of the .http file
            string httpFileContent = File.ReadAllText(httpFilePath);

            // Replace the @FileServer_HostAddress value
            string updatedContent = httpFileContent.Replace(
                "@FileServer_HostAddress = https://xxx.yyy/FileServer",
                $"@FileServer_HostAddress = {newHostAddress}"
            );

            // Write the updated content back to the .http file
            File.WriteAllText(httpFilePath, updatedContent);

            Console.WriteLine("FileServer_HostAddress updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

