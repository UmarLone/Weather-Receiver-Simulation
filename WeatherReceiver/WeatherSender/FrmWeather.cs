using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using ClsWeather;




namespace test1
{
    public partial class frmWeather : Form
    {
       private SoundPlayer _SoundPlayer;

    public static int CurrentPressure { get; set; }public ClsWeather.cWeather weatherSender = null;

        public frmWeather()
        {
            InitializeComponent();
           _SoundPlayer=new SoundPlayer("glyndwr.wav");
        }
        int counter = 0;
        double total = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            weatherSender = new cWeather();
            btnExit.Visible = false;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // shorting dll file and assigning tasks



            try
            {

                weatherSender.SetWeather();

                lblTemperature.Text = weatherSender.CurrentTemp.ToString() + " °C";
                counter++;
                // calculating average
                total = (weatherSender.CurrentTemp + total) / counter;

                label20.Text = total.ToString("n1") + " °C";

                lblAir.Text = weatherSender.CurrentPressure.ToString() + " hPa";
                lblRaining.Text = weatherSender.Raining.ToString();
                lblRainfall.Text = weatherSender.Rainfall.ToString() + " mm";
                lblWindD.Text = weatherSender.WindDirection.ToString();
                lblWindS.Text = weatherSender.WindSpeed.ToString() + " Km/h";
                lblWindDV.Text = weatherSender.WindDirectionValue.ToString();
                label10.Text = DateTime.Now.ToString();
                //retrieving max and min temp

                int[] numArray = new int[30];
                int min = weatherSender.CurrentTemp;
                int max = weatherSender.CurrentTemp;

                for (int i = 0; i < numArray.Length; i++)
                {
                    if (numArray[i] <= min)
                        min = numArray[i];
                    else if (numArray[i] > max)
                        max = numArray[i];
                    label21.Text = max.ToString();
                   
                }

                // storing readings in the text file 
                StreamWriter tw = File.AppendText(@"weatherHistory.txt");
                tw.Write(DateTime.Now + ",");
                tw.Write(lblTemperature.Text + ",");
                tw.Write(lblAir.Text + ",");
                tw.Write(lblRaining.Text + ",");
                tw.Write(lblRainfall.Text + ",");
                tw.Write(lblWindD.Text + ",");
                tw.Write(lblWindS.Text + ",");
                tw.Write(lblWindDV.Text);
                tw.WriteLine("");
                tw.Close();

                //calculating humidity 
                double TemperatureInC = ((weatherSender.CurrentTemp - 32.0) * 5.0) / 9.0;
                double DewPointInC = ((DewPointInF - 32.0) * 5.0) / 9.0;
                double RelativeHumidityNumerator = 6.112 * Math.Exp((17.62 * TemperatureInC) / (243.12 + TemperatureInC));
                double RelativeHumidityDenominator = 6.112 * Math.Exp((17.62 * DewPointInC) / (243.12 + DewPointInC));
                double Humidity = Math.Round((double)((RelativeHumidityDenominator / RelativeHumidityNumerator) * 100.0), 0);
                label9.Text = Humidity.ToString() + " %";

                //converting celsius to fahrenheit
                double c = weatherSender.CurrentTemp * 1.8 + 32;
                label13.Text = c.ToString() + " °F";

            //phase 1 of predicted weather
            if (weatherSender.CurrentPressure > 1020)
            {
                pictureBox3.Image = Properties.Resources.sunny;
                lblForeCast.Text = "Good";
            }
            else if (weatherSender.CurrentPressure > 5 & weatherSender.CurrentPressure <= 10)
            {
                pictureBox3.Image = Properties.Resources.Fair;
                lblForeCast.Text = "Fair";
            }
            else if (weatherSender.CurrentPressure > 10)
            {

                pictureBox3.Image = Properties.Resources.clouds;

                lblForeCast.Text = "Clouds";

            }

        //phase2
            else if (weatherSender.CurrentPressure > 1000 & weatherSender.CurrentPressure <= 1020)
            {

                pictureBox3.Image = Properties.Resources.sunny;
                lblForeCast.Text = "Good";
            }
            else if (weatherSender.CurrentPressure > 5 & weatherSender.CurrentPressure <= 10)
            {
                pictureBox3.Image = Properties.Resources.Fair;
                lblForeCast.Text = "Fair";
            }
            else if (weatherSender.CurrentPressure > 10)
            {

                pictureBox3.Image = Properties.Resources.Light_Rain;
                lblForeCast.Text = "Precitation";


            }

            //phase 3

            else if (weatherSender.CurrentPressure < 1000)
            {
                pictureBox3.Image = Properties.Resources.Fair;
                lblForeCast.Text = "Fair";
            }
            else if (weatherSender.CurrentPressure > 5 & weatherSender.CurrentPressure <= 10)
            {
                pictureBox3.Image = Properties.Resources.Light_Rain;
                lblForeCast.Text = "Light Rain";
            }
            else if (weatherSender.CurrentPressure > 10)
            {

                pictureBox3.Image = Properties.Resources.Heavy_Rain;

                lblForeCast.Text = "Storm";

            }
            else if (weatherSender.CurrentPressure < 0)
            {
                pictureBox3.Image = Properties.Resources.snow;

                lblForeCast.Text = "Snow";
            }


            }

            catch (Exception er)
            {
                MessageBox.Show("Some Error has occured. Click OK to restart the application","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                Application.Restart();

            }

            //displaying wind direction through Compass
            if (lblWindD.Text == "North")
            {
                pictureBox1.Image = Properties.Resources.n;

            }

            else if (lblWindD.Text == "North East")
            {
                pictureBox1.Image = Properties.Resources.ne;

            }

            else if (lblWindD.Text == "North West")
            {
                pictureBox1.Image = Properties.Resources.nw;

            }

            else if (lblWindD.Text == "South")
            {
                pictureBox1.Image = Properties.Resources.s;

            }



            else if (lblWindD.Text == "South East")
            {
                pictureBox1.Image = Properties.Resources.se;

            }
            else if (lblWindD.Text == "South West")
            {
                pictureBox1.Image = Properties.Resources.sw;

            }



            else if (lblWindD.Text == "West")
            {
                pictureBox1.Image = Properties.Resources.w;

            }



            else if (lblWindD.Text == "East")
            {
                pictureBox1.Image = Properties.Resources.e;

            }

        }

        


        private void btnWeather_Click(object sender, EventArgs e)

        {

            btnExit.Visible = true;

            // on /off button of the application 


            timer1.Enabled = !timer1.Enabled;
            

            btnWeather.Image = Properties.Resources.on;

          
            if (timer1.Enabled)
            {
                _SoundPlayer.Play();
                btnWeather.Image = Properties.Resources.off;
                label19.Text = " OFF";
              //  _SoundPlayer.Stop();
            }
            else
            {
                btnWeather.Image = Properties.Resources.on;
                label19.Text = " ON";
                _SoundPlayer.Play();
                _SoundPlayer.Stop();

            }
            

            
    }

        private void lblAir_Click(object sender, EventArgs e)
        {

        }

        private void lblTemperature_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void lblWindD_Click(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblRaining_Click(object sender, EventArgs e)
        {

        }

        private void lblWindD_Click_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            

        }

        public double DewPointInF { get; set; }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
        
        }
    
        
        public static double CurrentTemp { get; set; }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
             MessageBox.Show("Thank You for using glyndwr Weather App: Goodbye :)", "Glyndwr Weather App", MessageBoxButtons.OK, MessageBoxIcon.None);
             this.Close();

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    
    }
}
