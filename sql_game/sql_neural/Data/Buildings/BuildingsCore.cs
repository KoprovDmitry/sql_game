using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sql_neural.Data.SQLConnection;

namespace sql_neural.Data.Buildings
{
    public partial class BuildingsCore : UserControl
    {
        private static int countBuilders;
        private static List<Task> tasks = new List<Task>();
        private DateTime dataStart;
        private DateTime dataFinish;
        private double? ku;
        private int? timerBuldSeconds;
        public BuildingsCore()
        {
            InitializeComponent();
            countBuilders = 1;
            labelLevel.Text = "1";
            textBoxOglav.Text = "None";
            textBoxOpis.Text = "None";
            ku = 1.5;
            timerBuldSeconds = 10;
        }

        public BuildingsCore(string name)
        {
            InitializeComponent();
            countBuilders = 1;
            labelLevel.Text = "1";
            textBoxOglav.Text = name;
            textBoxOpis.Text = "None";
            ku = 1.5;
            timerBuldSeconds = 10;
        }

        private async void buttonPlus_Click(object sender, EventArgs e)
        {
            if (ku == null || timerBuldSeconds == null || countBuilders == null)
            {
                return;
            }
            int levels = Int32.Parse(labelLevel.Text);
            if (levels > 0 && levels < 1000 && countBuilders > 0)
            {
                countBuilders -= 1;
                if (((Button)sender).Text == "+")
                {
                    labelLevel.Text = (levels + 1).ToString();
                    timeBuilding(powLevels(levels, (int)timerBuldSeconds, (double)ku));
                }
                if (((Button)sender).Text == "-")
                {
                    labelLevel.Text = (levels - 1).ToString();
                    timeBuilding(powLevels(levels, (int)(timerBuldSeconds - 1), (double)ku));
                }

                labelLevel.Visible = false;
                buttonMinus.Visible = false;
                buttonPlus.Visible = false;
                progressBarTime.Visible = true;
                labelTime.Visible = true;
                await updateProgressBar(this.dataStart, this.dataFinish);
            }
        }

        private int powLevels(int levels, int timerSeconds, double ku)
        {
            int seconds = timerSeconds;
            for (int i = 1; i <= levels; i++)
            {
                seconds = (int)Math.Pow(seconds - 1, ku);
            }
            return seconds;
        }

        private void timeBuilding(int seconds)
        {
            Dictionary<string, int> timerData = timerRemaster(seconds);
            dataStart = DateTime.Now;
            dataFinish = DateTime.Now;
            dataFinish = dataFinish.Add(new TimeSpan(timerData["days"], timerData["hours"], timerData["minute"], timerData["seconds"]));
            TimeSpan timeSpan = dataFinish.Subtract(dataStart);
            progressBarTime.Maximum = Convert.ToInt32(timeSpan.TotalSeconds);
            labelTime.Text = $"осталось {timerData["days"]}:{timerData["hours"]}:{timerData["minute"]}:{timerData["seconds"]}";
        }

        private Dictionary<string, int> timerRemaster(int seconds)
        {
            Dictionary<string, int> timeData = new Dictionary<string, int>() {
                {"seconds", seconds },
                {"minute", 0 },
                {"hours", 0 },
                {"days", 0 }
            };
            while (timeData["seconds"] >= 60)
            {
                if (timeData["seconds"] >= 60)
                {
                    timeData["minute"] += 1;
                    timeData["seconds"] -= 60;
                }
                if (timeData["minute"] >= 60)
                {
                    timeData["hours"] += 1;
                    timeData["minute"] -= 60;
                }
                if (timeData["hours"] >= 24)
                {
                    timeData["days"] += 1;
                    timeData["hours"] -= 24;
                }
            }
            return timeData;
        }

        private async Task<string> updateProgressBar(DateTime start, DateTime finish)
        {
            return await Task.Run(() =>
            {
                while (true)
                {
                    DateTime timeNow = DateTime.Now;
                    TimeSpan timeSpan = timeNow.Subtract(start);
                    int seconds = Convert.ToInt32(finish.Subtract(timeNow).TotalSeconds);
                    Dictionary<string, int> timerData = timerRemaster(seconds);
                    if (Convert.ToInt32(timeSpan.TotalSeconds) >= 0 && seconds > 0)
                    {
                        progressBarTime.Value = Convert.ToInt32(timeSpan.TotalSeconds);
                        labelTime.Text = $"осталось {timerData["days"]}:{timerData["hours"]}:{timerData["minute"]}:{timerData["seconds"]}";
                    }
                    else
                    {
                        labelLevel.Visible = true;
                        buttonMinus.Visible = true;
                        buttonPlus.Visible = true;
                        progressBarTime.Visible = false;
                        labelTime.Visible = false;
                        timer1.Stop();
                        countBuilders += 1;
                        break;
                    }
                }
                return "";
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        public static int getBuilders { get { return countBuilders; } set { int values = value; if (values > (countBuilders + tasks.Count)) { countBuilders = values - tasks.Count; } } }
    }
}
