using System;
using System.Windows.Forms;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue;

        public LoadingScreen()
        {
            InitializeComponent();
        }


        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            loadingbarTimer.Start();
        }
        private void loadingbarTimer_Tick(object sender, EventArgs e)
        {
            loadingBarValue += 5;

            lblLoadingProdress.Text = loadingBarValue.ToString() + "%";
            loadingProgressbar.Value = loadingBarValue;


            if (loadingBarValue >= loadingProgressbar.Maximum)
            {
                loadingbarTimer.Stop();

                //Finish loading show main menu screen

                MainMenuScreen mainMenuScreen = new MainMenuScreen();
                mainMenuScreen.Show();

                this.Hide();
            }
        }
    }
}
