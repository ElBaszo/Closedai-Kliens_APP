using System;
using System.IO;

namespace ClosedAI
{
    internal static class EnvConfig
    {
        private static bool loaded;

        public static string Require(string name)
        {
            Load();

            string? value = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Missing required environment variable: {name}");
            }

            return value.Trim();
        }

        private static void Load()
        {
            if (loaded)
            {
                return;
            }

            loaded = true;

            string? envPath = FindEnvFile(AppContext.BaseDirectory);
            if (envPath == null)
            {
                return;
            }

            foreach (string rawLine in File.ReadAllLines(envPath))
            {
                string line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith("#"))
                {
                    continue;
                }

                int equalsIndex = line.IndexOf('=');
                if (equalsIndex <= 0)
                {
                    continue;
                }

                string name = line[..equalsIndex].Trim();
                string value = line[(equalsIndex + 1)..].Trim();

                if (value.Length >= 2 &&
                    ((value[0] == '"' && value[^1] == '"') ||
                     (value[0] == '\'' && value[^1] == '\'')))
                {
                    value = value[1..^1];
                }

                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                {
                    Environment.SetEnvironmentVariable(name, value);
                }
            }
        }

        private static string? FindEnvFile(string startDirectory)
        {
            DirectoryInfo? directory = new DirectoryInfo(startDirectory);
            while (directory != null)
            {
                string candidate = Path.Combine(directory.FullName, ".env");
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                directory = directory.Parent;
            }

            return null;
        }
    }
}
