using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lab5.ViewModels;

namespace Lab5.Views
{
    public partial class AddTaskView : Window
    {
        public AddTaskView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}