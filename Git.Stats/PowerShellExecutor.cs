using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Git.Stats
{
    public sealed class PowerShellExecutor
    {
        public IList<String> Execute(params string[] scripts)
        {
            InitialSessionState state = InitialSessionState.CreateDefault();
            state.Variables.Add(new SessionStateVariableEntry(
                "ErrorActionPreference", "Stop", null));
            using (Runspace runspace = RunspaceFactory.CreateRunspace(state))
            {
                return Execute(runspace, scripts);
            }
        }

        private IList<string> Execute(Runspace runspace, params string[] scripts)
        {
            runspace.Open();
            using (PowerShell shell = PowerShell.Create())
            {
                var result = new List<string>();
                foreach (var script in scripts)
                {
                    shell.Runspace = runspace;
                    shell.AddScript($"Set-PSDebug -Strict{Environment.NewLine}" + script);
                    try
                    {
                       var execResult = shell.Invoke().Where(x => x != null)
                            .Select(x => x.ToString())
                            .Where(x => x != string.Empty)
                            .ToList();
                        result.AddRange(execResult);
                    }
                    catch (RuntimeException psError)
                    {
                        ErrorRecord error = psError.ErrorRecord;
                        result.Add(error.Exception.Message);
                    }
                }

                return result;
            }
        } 
    }
}
