using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Management;
using System.Management.Instrumentation;

namespace WindowsFormsApplication1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        PerformanceCounter c;
        PerformanceCounter r;
        PerformanceCounter d;
        PerformanceCounter ld;
        PerformanceCounter ab;
        PerformanceCounter lcd;
        PerformanceCounter cp;
        PerformanceCounter dtr;
        PerformanceCounter em;
        PerformanceCounter pm;
        PerformanceCounter tm;
        PerformanceCounter adsr;
        PerformanceCounter adsw;
        PerformanceCounter cdql;
        PerformanceCounter ps;

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            UpdateProcessList();

            c = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            r = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
            d = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
           ld = new PerformanceCounter("LogicalDisk", "% Disk Time", "C:");
           cp = new PerformanceCounter("PhysicalDisk", "% Idle Time", "_Total");
            adsr = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");
            adsw = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");
            cdql = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total");
            ab = new PerformanceCounter("Memory", "Available MBytes", null);
            ps = new PerformanceCounter("Memory", "Pages/sec", null);
            lcd = new PerformanceCounter("LogicalDisk", "% Disk Time", "D:");
          dtr = new PerformanceCounter("LogicalDisk", "Disk Transfers/sec", "_Total");

            ManagementObjectSearcher DiskInfo1 = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");

            foreach (ManagementObject X1 in DiskInfo1.Get())
            {
                metroLabel22.Text = ": " + X1["Name"].ToString();
                metroLabel23.Text = ": " + X1["Manufacturer"].ToString();
                metroLabel24.Text = ": " + X1["SerialNumber"].ToString();
            }

            ManagementObjectSearcher DiskInfo2 = new ManagementObjectSearcher("SELECT * FROM Win32_UserAccount");

            foreach (ManagementObject X2 in DiskInfo2.Get())
            {
                metroLabel21.Text = X2["Name"].ToString();
               
            }


        }

        private void UpdateProcessList()
        {

            lstProcesses.Items.Clear();

            foreach (System.Diagnostics.Process p in
            System.Diagnostics.Process.GetProcesses())
            {
                lstProcesses.Items.Add(p.ProcessName + " : " + p.Id);
            }

            tslProcessCount.Text = "Processes running: " +
            lstProcesses.Items.Count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            metroTabControl1.SelectedTab = metroTabPage1;

            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();

            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;

            circularProgressBar2.Value = 0;
            circularProgressBar2.Minimum = 0;
            circularProgressBar2.Maximum = 100;

            circularProgressBar3.Value = 0;
            circularProgressBar3.Minimum = 0;
            circularProgressBar3.Maximum = 1000;

        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            
           
            metroTabControl1.SelectedTab = metroTabPage1;

            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            metroTabControl1.SelectedTab = metroTabPage2;

            panel1.Hide();
            panel2.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
            metroTabControl1.SelectedTab = metroTabPage3;

            panel1.Hide();
            panel2.Hide();
            panel3.Show();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();

            ManagementObjectSearcher disk = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject drive in disk.Get())
            {
                label3.Text = drive["Model"].ToString();
                
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage4;

            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Show();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();

            UpdateProcessList();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage5;

            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
            panel6.Hide();
            panel7.Hide();

            ManagementObjectSearcher DiskInfo = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject X in DiskInfo.Get())
            { 
                metroLabel9.Text  = ": " + X["Name"].ToString();
                metroLabel10.Text = ": " + X["Manufacturer"].ToString();
                metroLabel11.Text = ": " + X["NumberOfCores"].ToString();
                metroLabel12.Text = ": " + X["Description"].ToString();
                metroLabel13.Text = ": " + X["CurrentClockSpeed"].ToString();
            }

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            timer4.Enabled = true;

            metroTabControl1.SelectedTab = metroTabPage6;
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
            panel7.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            float x;
            x = c.NextValue();
            

            //--------------------------------------------------------------------------//

            //Determine the next X value
            double nextX = 1;

            if (chart1.Series["CPU"].Points.Count > 0)
            {
                nextX = chart1.Series["CPU"].Points[chart1.Series["CPU"].Points.Count - 1].XValue + 1;
            }

            //Add a new value to the Series
            //Random rnd = new Random();
            chart1.Series["CPU"].Points.AddXY(nextX, x);

            //Remove Points on the left side after 100
            while (chart1.Series["CPU"].Points.Count > 50)
            {
                chart1.Series["CPU"].Points.RemoveAt(0);
            }

            //Set the Minimum and Maximum values for the X Axis on the Chart
            double min = chart1.Series["CPU"].Points[0].XValue-1;
            chart1.ChartAreas[0].AxisX.Minimum = min;
            chart1.ChartAreas[0].AxisX.Maximum = min + 50;
            chart1.ChartAreas[0].AxisY.Maximum = 150;

            //---------------------------------------------------------------------------------------/

            circularProgressBar1.Value = (int)PCCPU.NextValue();
            circularProgressBar1.Update();

            int a = (int)x;
            label2.Text = a.ToString() + "%";
            

            int lt = (int)PCProcess.NextValue();
            lthreads.Text = lt.ToString();

            double ut = (int)PCSystem.NextValue();
           


            int hour = Convert.ToInt32(Math.Floor( ut/ 3600));
            int minute = Convert.ToInt32(Math.Floor((ut - (hour * 3600)) / 60));
            int sec = Convert.ToInt32(ut) - (hour * 3600) - (minute * 60);

            luptime.Text = hour.ToString() + " H: "+minute.ToString() + " M: "+ sec.ToString() + " S";


            int p = (int)PCnoOfProcess.NextValue();
            lp.Text = p.ToString();



        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            float y;
            y = r.NextValue();
            

            //===============================================================

            
            double nextX1 = 1;

            if (chart2.Series["RAM"].Points.Count > 0)
            {
                nextX1 = chart2.Series["RAM"].Points[chart2.Series["RAM"].Points.Count - 1].XValue + 1;
            }

            
            chart2.Series["RAM"].Points.AddXY(nextX1, y);

            
            while (chart2.Series["RAM"].Points.Count > 50)
            {
                chart2.Series["RAM"].Points.RemoveAt(0);
            }

            
            double min2 = chart2.Series["RAM"].Points[0].XValue - 1;
            chart2.ChartAreas[0].AxisX.Minimum = min2;
            chart2.ChartAreas[0].AxisX.Maximum = min2 + 50;
            chart2.ChartAreas[0].AxisY.Maximum = 100;
            //===============================================================

            circularProgressBar2.Value = (int)r.NextValue();
            circularProgressBar2.Update();

            int b = (int)y;
            label1.Text = b.ToString() + "%";

            float ab2;
            ab2 = ab.NextValue();
            metroLabel16.Text = ab2.ToString() + " MB";

            float ps1 = ps.NextValue();
            int ps2 = (int)ps1;
            metroLabel31.Text = "Pages/sec "+":"+ ps2.ToString();

           
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            float z;
            z = d.NextValue();

            circularProgressBar3.Value = (int)z;
            circularProgressBar3.Update();

            double nextX2 = 1;

            if (chart3.Series["Disk"].Points.Count > 0)
            {
                nextX2 = chart3.Series["Disk"].Points[chart3.Series["Disk"].Points.Count - 1].XValue + 1;
            }


            chart3.Series["Disk"].Points.AddXY(nextX2, z);


            while (chart3.Series["Disk"].Points.Count > 50)
            {
                chart3.Series["Disk"].Points.RemoveAt(0);
            }


            double min3 = chart3.Series["Disk"].Points[0].XValue - 1;
            chart3.ChartAreas[0].AxisX.Minimum = min3;
            chart3.ChartAreas[0].AxisX.Maximum = min3 + 50;

           

            int pb = (int)z;
            label4.Text = pb.ToString() + " %";

            //===================================================================================
            float z1;
            z1 = cp.NextValue();

            double nextX3 = 1;

            if (chart5.Series["CPDisk"].Points.Count > 0)
            {
                nextX3 = chart5.Series["CPDisk"].Points[chart5.Series["CPDisk"].Points.Count - 1].XValue + 1;
            }


            chart5.Series["CPDisk"].Points.AddXY(nextX3, z1);


            while (chart5.Series["CPDisk"].Points.Count > 50)
            {
                chart5.Series["CPDisk"].Points.RemoveAt(0);
            }


            double min4 = chart5.Series["CPDisk"].Points[0].XValue - 1;
            chart5.ChartAreas[0].AxisX.Minimum = min4;
            chart5.ChartAreas[0].AxisX.Maximum = min4 + 50;

         //=========================================================================

            float adsr1;
            adsr1 = adsr.NextValue();

            double nextX11 = 1;

            if (chart8.Series["ADSR"].Points.Count > 0)
            {
                nextX11 = chart8.Series["ADSR"].Points[chart8.Series["ADSR"].Points.Count - 1].XValue + 1;
            }


            chart8.Series["ADSR"].Points.AddXY(nextX11, adsr1);


            while (chart8.Series["ADSR"].Points.Count > 50)
            {
                chart8.Series["ADSR"].Points.RemoveAt(0);
            }


            double min11 = chart8.Series["ADSR"].Points[0].XValue - 1;
            chart8.ChartAreas[0].AxisX.Minimum = min11;
            chart8.ChartAreas[0].AxisX.Maximum = min11 + 50;

            

            //=============================================================================================


            float adsw1 = adsw.NextValue();

            double nextX12 = 1;

            if (chart9.Series["ADSW"].Points.Count > 0)
            {
                nextX12 = chart9.Series["ADSW"].Points[chart9.Series["ADSW"].Points.Count - 1].XValue + 1;
            }


            chart9.Series["ADSW"].Points.AddXY(nextX12, adsw1);


            while (chart9.Series["ADSW"].Points.Count > 50)
            {
                chart9.Series["ADSW"].Points.RemoveAt(0);
            }


            double min12 = chart9.Series["ADSW"].Points[0].XValue - 1;
            chart9.ChartAreas[0].AxisX.Minimum = min12;
            chart9.ChartAreas[0].AxisX.Maximum = min12 + 50;
            

            //==============================================================

            float cdql1 = cdql.NextValue();

            double nextX13 = 1;

            if (chart10.Series["CDQL"].Points.Count > 0)
            {
                nextX13 = chart10.Series["CDQL"].Points[chart10.Series["CDQL"].Points.Count - 1].XValue + 1;
            }


            chart10.Series["CDQL"].Points.AddXY(nextX13, cdql1);


            while (chart10.Series["CDQL"].Points.Count > 50)
            {
                chart10.Series["CDQL"].Points.RemoveAt(0);
            }


            double min13 = chart10.Series["CDQL"].Points[0].XValue - 1;
            chart10.ChartAreas[0].AxisX.Minimum = min13;
            chart10.ChartAreas[0].AxisX.Maximum = min13 + 50;


        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            float x1;
            x1 = ld.NextValue();
            

            double nextX4 = 1;

            if (chart4.Series["LDisk"].Points.Count > 0)
            {
                nextX4 = chart4.Series["LDisk"].Points[chart4.Series["LDisk"].Points.Count - 1].XValue + 1;
            }


            chart4.Series["LDisk"].Points.AddXY(nextX4, x1);


            while (chart4.Series["LDisk"].Points.Count > 50)
            {
                chart4.Series["LDisk"].Points.RemoveAt(0);
            }


            double min5 = chart4.Series["LDisk"].Points[0].XValue - 1;
            chart4.ChartAreas[0].AxisX.Minimum = min5;
            chart4.ChartAreas[0].AxisX.Maximum = min5 + 50;
            chart4.ChartAreas[0].AxisY.Maximum = 150;


            float x2;
            x2 = lcd.NextValue();

            double nextX5 = 1;

            if (chart6.Series["LCDisk"].Points.Count > 0)
            {
                nextX5 = chart6.Series["LCDisk"].Points[chart6.Series["LCDisk"].Points.Count - 1].XValue + 1;
            }


            chart6.Series["LCDisk"].Points.AddXY(nextX5, x2);


            while (chart6.Series["LCDisk"].Points.Count > 50)
            {
                chart6.Series["LCDisk"].Points.RemoveAt(0);
            }


            double min6 = chart6.Series["LCDisk"].Points[0].XValue - 1;
            chart6.ChartAreas[0].AxisX.Minimum = min6;
            chart6.ChartAreas[0].AxisX.Maximum = min6 + 50;
            chart6.ChartAreas[0].AxisY.Maximum = 150;


            float dtr1;
            dtr1 = dtr.NextValue();

            double nextX6 = 1;

            if (chart7.Series["DiskT"].Points.Count > 0)
            {
                nextX6 = chart7.Series["DiskT"].Points[chart7.Series["DiskT"].Points.Count - 1].XValue + 1;
            }


            chart7.Series["DiskT"].Points.AddXY(nextX6, dtr1);


            while (chart7.Series["DiskT"].Points.Count > 50)
            {
                chart7.Series["DiskT"].Points.RemoveAt(0);
            }


            double min7 = chart7.Series["DiskT"].Points[0].XValue - 1;
            chart7.ChartAreas[0].AxisX.Minimum = min7;
            chart7.ChartAreas[0].AxisX.Maximum = min7 + 50;
            chart7.ChartAreas[0].AxisY.Maximum = 150;

            int ldtr1 = (int)dtr1;
            ldtr.Text = ": "+ldtr1.ToString()+"KB/s";

        }
        private void Update_Click(object sender, EventArgs e)
        {
            UpdateProcessList();
        }

        private void kill_Click(object sender, EventArgs e)
        {
            foreach (System.Diagnostics.Process p in
                     System.Diagnostics.Process.GetProcesses())
            {
                string[] arr = lstProcesses.SelectedItem.ToString().Split('-');
                string sProcess = arr[0].Trim();
                int iId = Convert.ToInt32(arr[1].Trim());
                if (p.ProcessName == sProcess && p.Id == iId)
                {
                    p.Kill();
                    
                }
            }
            UpdateProcessList();

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel18_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel22_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel23_Click(object sender, EventArgs e)
        {

        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            timer5.Enabled = true;
            metroTabControl1.SelectedTab = metroTabPage7;
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Show();


        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            

        }
    }
}
