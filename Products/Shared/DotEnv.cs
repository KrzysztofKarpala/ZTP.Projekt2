namespace Products.Shared
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            foreach (var line in File.ReadLines(filePath))
            {
                int index = line.IndexOf('=');
                if (index == -1)
                {
                    continue;
                }
                // line.Substring(0, index);
                var name = line[..index];
                // line.Substring(index+1);
                var value = line[(index + 1)..];
                Environment.SetEnvironmentVariable(name, value);
            }
        }
    }
}
