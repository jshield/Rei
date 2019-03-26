namespace Rei
{
    public interface IBuffer
    {
        string Read();
        void Replace(string value);
    }

    public interface IOutput
    {
        void Append();
    }
}