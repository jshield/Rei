using System;
using System.IO;
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
            BufferBox.Text = System.Text.Encoding.Default.GetString(Properties.Resources.README);
            BufferBox.Focus();
            DataContext = this;
            ResizeMode = ResizeMode == ResizeMode.NoResize ? ResizeMode.CanResize : ResizeMode.NoResize;
            WindowStyle = WindowStyle == WindowStyle.None ? WindowStyle.SingleBorderWindow : WindowStyle.None;

            foreach (var hotKey in Config.HotKeys)
            {
                InputBindings.Add(new KeyBinding
                {
                    Key = hotKey.Key,
                    Modifiers = hotKey.ModifierKeys,
                    Command = ExecuteCommandCommand,
                    CommandParameter = hotKey.Command
                });
            }
            InputBindings.Add(new KeyBinding {Key = Key.F11, Command = new Command(SwitchWindowState)});
        }

        public string CurrentFile
        {
            get { return FileBlock.Text; }
            private set { FileBlock.Text = value; }
        }

        internal void ExecuteCommand(string cmd)
        {
            switch (cmd[0])
            {
                case '<':
                    if (BufferBox.SelectedText == "")
                    {
                        var args = cmd.Substring(1).Split(' ');
                        if (args.Length == 1)
                            if (args[0] == "" && CurrentFile != "")
                            {
                                BufferBox.Text = File.ReadAllText(CurrentFile);
                            }
                            else
                            {
                                var path = Path.GetFullPath(Environment.ExpandEnvironmentVariables(args[0]));
                                BufferBox.Text = File.ReadAllText(path);
                                CurrentFile = path;
                            }
                    }
                    break;
                case '>':
                    if (BufferBox.SelectedText == "")
                    {
                        var args = cmd.Substring(1).Split(' ');
                        if (args.Length == 1)
                            if (args[0] == "" && CurrentFile != "")
                            {
                                File.WriteAllText(CurrentFile, BufferBox.Text);
                            }
                            else
                            {
                                var path = Path.GetFullPath(Environment.ExpandEnvironmentVariables(args[0]));
                                File.WriteAllText(path, BufferBox.Text);
                                if (CurrentFile != path)
                                    CurrentFile = path;
                            }
                    }
                    break;
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
            if (e.ChangedButton == MouseButton.Middle)
                SwitchWindowChrome();
        }

        public ICommand ExecuteCommandCommand => new Command<string>(ExecuteCommand);

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
    }
}