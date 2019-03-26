namespace Rei
{
    public class CommandInfo
    {
        public CommandInfo(string cmd)
        {
            PipeDirection = cmd[0].ToPipeDirection();
            var parts = cmd.ToParts();
            if (cmd.StartsWith("!"))
            {
                IsProcess = true;
                File = parts[0];
            }
            else
            {
                File = parts[0];
            }

            Arguments = parts.ExtractArguments();
        }

        public PipeDirection PipeDirection { get; }

        public bool IsProcess { get; }

        public string File { get; }

        public string Arguments { get; }
    }
}