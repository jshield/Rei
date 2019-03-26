using System.Windows.Controls;

namespace Rei
{
    public class TextBoxBuffer : IBuffer
    {
        private readonly TextBox _textBox;

        private TextBoxBuffer(TextBox textBox)
        {
            _textBox = textBox;
        }

        public string Read()
        {
            return _textBox.SelectionLength > 0 ? _textBox.SelectedText : _textBox.Text;
        }

        public void Replace(string result)
        {
            if (_textBox.SelectionLength > 0) _textBox.SelectedText = result;
            else _textBox.Text = result;
        }

        public static implicit operator TextBoxBuffer(TextBox box)
        {
            return new TextBoxBuffer(box);
        }
    }
}