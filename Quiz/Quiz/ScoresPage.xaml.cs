using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoresPage : ContentPage
    {
        public ScoresPage()
        {
            InitializeComponent();
        }

        private async void LoadScores()
        {
            var scores = await App.Database.GetResultsAsync();

            var sortedScores = scores.OrderByDescending(score => score.Score).ThenBy(score => score.TotalTime).ToList();

            var groupedScores = sortedScores.GroupBy(score => score.Score).SelectMany(group => group.OrderBy(score => score.TotalTime).ToList());

            for(int i = 0; i < groupedScores.Count; i++)
            {
                groupedScores[i].RankingPostion = i + 1;
            }

            scoresCollectionView.ItemsSource = groupedScores;
        }
    }
}