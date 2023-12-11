using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    public partial class Crackdown2Page : Page
    {
        public Crackdown2Page()
        {
            InitializeComponent();
        }

        private void GodBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ToggleGodMode();
        }

        private void AmmoBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ToggleInfinteAmmo();
        }

        private void CmdsBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ConsoleCommand(CmdsBox.Text);
        }

        private void DrawOutlines_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.DrawOutlines(DrawOutlinesBtn);
        }

        private void ShowDebugInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.DebugInfo(ShowDebugInfoBtn);
        }

        private void PerfGraphsBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.PerformanceGraphs(PerfGraphsBtn);
        }

        private void RedFpsTextBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.RedFpsText(RedFpsTextBtn);
        }

        private void MaxSkillsBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ConsoleCommand("maxagentskills");
        }

        private void FlyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ToggleFlyMode();
        }

        private void ApocalypseBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ConsoleCommand("apocalypse");
        }

        private void SuicideBtn_Click(object sender, RoutedEventArgs e)
        {
            Crackdown2Helper.ConsoleCommand("suicide");
        }
    }
}
