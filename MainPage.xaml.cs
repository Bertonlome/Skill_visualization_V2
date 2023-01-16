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

            string[] skillName = collabSkillInput.Text.Split(',');

            DataAccess.AddCollab(myCollab, skillName);

            Output.ItemsSource = DataAccess.GetCollabList();

            collabNameInput.Text = "";
            collabResumeInput.Text = "";
            collabSkillInput.Text = "";
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            string[] collabName = ProjectCollabName.Text.Split(',');
            Project myProject = new Project
            {
                Name = projectNameInput.Text,
                Description = projectDescInput.Text,
            };

            DataAccess.AddProject(myProject, collabName);

            Output.ItemsSource = DataAccess.GetCollabList();

            projectNameInput.Text = "";
            ProjectCollabName.Text = "";
            projectDescInput.Text = "";

        }

        private void AddSkill(object sender, RoutedEventArgs e)
        {
            string[] collabName = skillCollabInput.Text.Split(',');
            string[] projectName = skillProjectInput.Text.Split(',');
            Skill mySkill = new Skill
            {
                Name = skillNameInput.Text,
                Description = skillDescInput.Text
            };

            DataAccess.AddSkill(mySkill, collabName, projectName);

            Output.ItemsSource = DataAccess.GetCollabList();

            skillNameInput.Text = "";
            skillDescInput.Text = "";
            skillCollabInput.Text = "";
            skillProjectInput.Text = "";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WordCloudPage));
        }
    }
}
