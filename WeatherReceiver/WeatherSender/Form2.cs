using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ClsWeather;
namespace test1
{
    public partial class Form2 : Form
    {
        SqlConnection thisConnection = new SqlConnection(@"C:\Users\RAJ\Desktop\WeatherProject\WeatherReceiver\WeatherSender\Database1.sdf");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
             



















        public Form2()
        {
            InitializeComponent();
        }
    }
}
