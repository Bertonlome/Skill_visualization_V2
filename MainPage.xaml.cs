using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Skill_visualization
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Output.ItemsSource = DataAccess.GetCollabList();
        }

        private void AddCollab(object sender, RoutedEventArgs e)
        {
            Collab myCollab = new Collab
            {
                Name = collabNameInput.Text,
                Resume = collabResumeInput.Text,
            };

            DataAccess.AddCollab(myCollab);

            Output.ItemsSource = DataAccess.GetCollabList();
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {

        }

        private void AddSkill(object sender, RoutedEventArgs e)
        {

        }

    }
}
