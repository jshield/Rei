using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rei
{
    public static class CommandExtensions
    {
        public static Process ToProcess(this CommandInfo cmd, Action<ProcessStartInfo> configureProcess)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ResolveFullPath(cmd.File),
                Arguments = $"{cmd.Arguments}",
                UseShellExecute = false
            };
            configureProcess(psi);
            return Process.Start(psi);
        }

        public static string ResolveFullPath(this string name)
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            return Path.IsPathRooted(name)
                ? name
                : path
                    ?.Split(Path.PathSeparator)
                    .Select(dir => Path.Combine(dir.Trim('"'), name)).First(File.Exists);
        }

        public static string ExtractArguments(this string[] parts)
        {
            return parts.Skip(1).Aggregate("", (s, a) => s + a + " ");
        }

        public static string[] ToParts(this string cmd)
        {
            var skip = cmd.Substring(1);
            return skip.Split(' ');
        }

        public static async Task Pipe(this IBuffer buffer, string cmd)
        {
            var commandInfo = new CommandInfo(cmd);
            if (commandInfo.IsProcess)
            {
                using (var process = commandInfo.ToProcess(info =>
                {
                    info.CreateNoWindow = true;
                    info.RedirectStandardInput = true;
                    info.RedirectStandardOutput = true;
                }))
                {
                    if (process != null)
                        await process.PipeBuffer(buffer, commandInfo.PipeDirection);
                }
            }
            else
            {
                var fileInfo = new FileInfo(commandInfo.File);
                switch (commandInfo.PipeDirection)
                {
                    case PipeDirection.In:
                        using (var streamReader = fileInfo.OpenText())
                        {
                            await buffer.ReadFromStream(streamReader);
                        }

                        break;
                    case PipeDirection.Out:
                        using (var sw = fileInfo.CreateText())
                        {
                            await buffer.WriteToStream(sw);
                        }

                        break;
                    case PipeDirection.InOut:
                        break;
                    case PipeDirection.Invalid:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static PipeDirection ToPipeDirection(this char character)
        {
            switch (character)
            {
                case '<':
                    return PipeDirection.In;
                case '>':
                    return PipeDirection.Out;
                case '|':
                    return PipeDirection.InOut;
                default:
                    return PipeDirection.Invalid;
            }
        }

        public static FileAccess ToFileAccess(this PipeDirection direction)
        {
            switch (direction)
            {
                case PipeDirection.In:
                    return FileAccess.Read;
                case PipeDirection.Out:
                    return FileAccess.Write;
                case PipeDirection.InOut:
                    return FileAccess.ReadWrite;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static void AppendLine(this TextBlock output, string message)
        {
            output.Text += message + Environment.NewLine;
        }
    }
}