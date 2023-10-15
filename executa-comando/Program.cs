using System.Diagnostics;

var command = "git";
var arg = "clone https://github.com/eduardoworrel/dotnet-helpers.git";

ProcessStartInfo psi = 
  new ProcessStartInfo
  {
      FileName = command,
      Arguments = arg,
      RedirectStandardOutput = false,
      UseShellExecute = false,
      CreateNoWindow = true,
  };

using var process = new Process { StartInfo = psi };
process.Start();
process.WaitForExit();