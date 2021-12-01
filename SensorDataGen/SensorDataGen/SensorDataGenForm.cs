using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using SensorDataGen.Classes;
using SensorDataGen.Classes.Controllers;
using System.Threading;

namespace SensorDataGen
{
    public partial class SensorDataGenForm : Form
    {
        private RabbitMQController rabbitController;
        private SensorsController sensorsController;

        public SensorDataGenForm()
        {
            InitializeComponent();
            InitControllers();
            InitComponents();
        }

        private void InitControllers()
        {
            rabbitController = new RabbitMQController();
            sensorsController = new SensorsController(rabbitController);
        }

        private void InitComponents()
        {
            Dictionary<string, string> sensorTypeDic = new Dictionary<string, string>();
            sensorTypeDic.Add("wind", "Wiatr");
            sensorTypeDic.Add("humidity", "Wilgotność");
            sensorTypeDic.Add("temperature", "Temperatura");
            sensorTypeDic.Add("pressure", "Ciśnienie");
            addSensorTypeCB.DataSource = new BindingSource(sensorTypeDic, null);
            addSensorTypeCB.DisplayMember = "Value";
            addSensorTypeCB.ValueMember = "Key";
        }

        private void stopBt_Click(object sender, EventArgs e)
        {
            startBt.Enabled = true;
            stopBt.Enabled = false;
            addSensorGB.Enabled = true;

            sensorsController.StopGenerator();
        }

        private void startBt_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;

            if (!sensorsController.HasSensors())
            {
                errorLabel.Text = "Błąd - brak sensorów do generowania danych.";
                errorLabel.Visible = true;
            }
            else
            {
                startBt.Enabled = false;
                stopBt.Enabled = true;
                addSensorGB.Enabled = false;

                sensorsController.StartGenerator();
            }
        }

        private void addSensorAdvanceCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (addSensorAdvanceCheck.Checked)
                addSensorAdvancePanel.Enabled = true;
            else
                addSensorAdvancePanel.Enabled = false;
        }

        private void addSensorAddBt_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;

            string sensorType = ((KeyValuePair<string, string>)addSensorTypeCB.SelectedItem).Key;
            object sensor;

            if (addSensorAdvanceCheck.Checked)
            {
                decimal minValue = advanceMinNUD.Value;
                decimal maxValue = advanceMaxNUD.Value;

                int dataPerSec = decimal.ToInt32(advanceDataPerSecNUD.Value);

                if(minValue <= maxValue)
                {
                    sensor = sensorsController.AddSensor(sensorType, minValue, maxValue, dataPerSec);
                    //sensorsLB.Items.Add($"MAC: {sensor}  Typ: {((KeyValuePair<string, string>)addSensorTypeCB.SelectedItem).Value} Min: {minValue} Max: {maxValue} DnS: {dataPerSec}");
                    sensorsLB.Items.Add(sensor);
                }
                else
                {
                    errorLabel.Text = "Błąd - wartość minimalna jest większa od maksymalnej.";
                    errorLabel.Visible = true;
                }
            }
            else
            {
                sensor = sensorsController.AddSensor(sensorType);
                //sensorsLB.Items.Add($"MAC: {sensor}  Typ: {((KeyValuePair<string, string>)addSensorTypeCB.SelectedItem).Value} Ustawienia domyślne");
                sensorsLB.Items.Add(sensor);
            }

        }

        private void sensorLB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            object sensor = sensorsLB.SelectedItem;

            if(sensor != null)
            {
                sensorsController.DeleteSensor(sensor);
                sensorsLB.Items.Remove(sensor);
            }
        }
    }
}
