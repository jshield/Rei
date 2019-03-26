using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Rei
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            OutputBox.Text = Encoding.Default.GetString(Properties.Resources.README) + Environment.NewLine;
            BufferBox.Focus();
            DataContext = this;
            ResizeMode = ResizeMode == ResizeMode.NoResize ? ResizeMode.CanResize : ResizeMode.NoResize;
            WindowStyle = WindowStyle == WindowStyle.None ? WindowStyle.SingleBorderWindow : WindowStyle.None;

            foreach (var hotKey in Config.HotKeys)
                InputBindings.Add(new KeyBinding
                {
                    Key = hotKey.Key,
                    Modifiers = hotKey.ModifierKeys,
                    Command = ExecuteCommandCommand,
                    CommandParameter = hotKey.Command
                });
        }

        private ICommand ExecuteCommandCommand => new Command<string>(async cmd => await ExecuteCommand(cmd));

        private async Task ExecuteCommand(string cmd)
        {
            try
            {
                if (cmd[0] == '*')
                {
                    SwitchWindowState();
                    return;
                }

                await ((TextBoxBuffer) BufferBox).Pipe(cmd);
            }
            catch (Exception ex)
            {
                OutputBox.AppendLine(ex.Message);
            }
        }

        private void UpdateLineBlock()
        {
            LineBlock.Text = (BufferBox.GetLineIndexFromCharacterIndex(BufferBox.SelectionStart) + 1).ToString();
        }

        private void BufferBoxPreviewKeyUp(object sender, KeyEventArgs e)
        {
            UpdateLineBlock();
        }

        private void CommandBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CommandBox.SelectAll();
        }

        private void MainWindow_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle) SwitchWindowChrome();
        }

        public void SwitchWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    ResizeMode = ResizeMode.NoResize;
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                    break;
                case WindowState.Minimized:
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SwitchWindowChrome()
        {
            ResizeMode = ResizeMode == ResizeMode.NoResize ? ResizeMode.CanResize : ResizeMode.NoResize;
            WindowStyle = WindowStyle == WindowStyle.None ? WindowStyle.SingleBorderWindow : WindowStyle.None;
        }

        private async void CommandBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && e.IsDown)
            {
                await ExecuteCommand(CommandBox.Text);
            }
        }
    }
}