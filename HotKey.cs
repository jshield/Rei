using System.Windows.Input;

namespace Rei
{
    public struct HotKey
    {
        public bool Equals(HotKey other)
        {
            return ModifierKeys == other.ModifierKeys && Key == other.Key && string.Equals(Command, other.Command);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is HotKey && Equals((HotKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) ModifierKeys;
                hashCode = (hashCode * 397) ^ (int) Key;
                hashCode = (hashCode * 397) ^ (Command?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static bool operator ==(HotKey left, HotKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HotKey left, HotKey right)
        {
            return !left.Equals(right);
        }

        public ModifierKeys ModifierKeys;
        public Key Key;
        public string Command;
    }
}