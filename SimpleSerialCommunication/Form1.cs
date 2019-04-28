using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SimpleSerialCommunication
{
    public partial class Form1 : Form
    {
        SerialPort serialport = new SerialPort();
        String DataStream ;
        public Form1()
        {
            InitializeComponent();
            FillSerialParam();


        }
        private void FillSerialParam()
        {
            cmbPort.Items.Clear();
            foreach (string serialname in SerialPort.GetPortNames())
            {
                cmbPort.Items.Add(serialname);
            }
            if (cmbPort.Items.Count > 0)
                cmbPort.SelectedIndex = 0;
            cmbBaudrate.Items.Add("1200");
            cmbBaudrate.Items.Add("2400");
            cmbBaudrate.Items.Add("4800");
            cmbBaudrate.Items.Add("9600");
            cmbBaudrate.Items.Add("19200");
            cmbBaudrate.Items.Add("38400");
            cmbBaudrate.Items.Add("57600");
            cmbBaudrate.Items.Add("115200");
            cmbBaudrate.Items.Add("230400");
            cmbBaudrate.SelectedIndex = 3;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TxtConnect_Click(object sender, EventArgs e)
        {
            if (serialport.IsOpen)
            {

                serialport.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);

                serialport.Close();
                
            }
            else
            {
                serialport.PortName = cmbPort.SelectedItem.ToString();
                serialport.BaudRate = Convert.ToInt16(cmbBaudrate.SelectedItem);
                serialport.DataBits = 8;
                serialport.Parity = Parity.None;
                serialport.StopBits = StopBits.One;

                serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                serialport.Open();
            }
            
            
        }

        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //string indata = sp.ReadExisting();
            //string indata = sp.ReadLine();
            this.Invoke(new EventHandler(DoUpDate));
            //Console.WriteLine("Data Received:");
            //Console.Write(indata);
        }

        private void DoUpDate(object s, EventArgs e)
        {
            txtStream.Text = serialport.ReadLine().ToUpper();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialport.IsOpen)
            {
                serialport.Close();
            }
        }
    }
}
