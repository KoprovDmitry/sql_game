using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using sql_neural.Data.SQLConnection;

namespace sql_neural
{
    public partial class MainForm : Form
    {
        private SQLConnections sqlConnections = null;
        public MainForm()
        {
            InitializeComponent();
            this.sqlConnections = new SQLConnections(ConfigurationManager.ConnectionStrings["GameCS"].ConnectionString);
        }
    }
}
