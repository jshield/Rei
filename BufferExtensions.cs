using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Rei
{
    public static class BufferExtensions
    {
        public static Action<Action> Dispatch { get; set; }

        public static async Task ReadFromStream(this IBuffer buffer, StreamReader reader)
        {
            var result = await reader.ReadToEndAsync();
            Dispatch(() => { buffer.Replace(result); });
        }

        public static async Task WriteToStream(this IBuffer buffer, StreamWriter writer)
        {
            string result = null;
            Dispatch(() => result = buffer.Read());
            await writer.WriteAsync(result);
        }

        public static async Task PipeBuffer(this Process process, IBuffer buffer,
            PipeDirection direction = PipeDirection.InOut)
        {
            switch (direction)
            {
                case PipeDirection.In:
                    await buffer.ReadFromStream(process.StandardOutput);
                    break;
                case PipeDirection.Out:
                    await buffer.WriteToStream(process.StandardInput);
                    break;
                case PipeDirection.InOut:
                    await Task.WhenAll(buffer.ReadFromStream(process.StandardOutput),
                        buffer.WriteToStream(process.StandardInput));
                    break;
            }
        }
    }
}