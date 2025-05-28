using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AdministratorComander
{
    public partial class Form4 : Form
    {
        private Dictionary<string, Worker> workers = new Dictionary<string, Worker>();
        private readonly string workersFilePath = Path.Combine(Application.StartupPath, "workers.json");

        public Form4()
        {
            InitializeComponent();

            LoadWorkersFromFile();

            comboBoxUsers.Items.Clear();
            comboBoxUsers.Items.AddRange(workers.Keys.ToArray());

            comboBoxUsers.SelectedIndexChanged += ComboBoxUsers_SelectedIndexChanged;
        }

        private void LoadWorkersFromFile()
        {
            try
            {
                if (File.Exists(workersFilePath))
                {
                    string json = File.ReadAllText(workersFilePath);
                    workers = JsonSerializer.Deserialize<Dictionary<string, Worker>>(json) ?? new Dictionary<string, Worker>();
                }
                else
                {
                    workers = new Dictionary<string, Worker>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                workers = new Dictionary<string, Worker>();
            }
        }

        private void ComboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = comboBoxUsers.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedName) || !workers.ContainsKey(selectedName))
                return;

            var worker = workers[selectedName];

            userPanel.Controls.Clear();

            int y = 10;
            void AddLabel(string text)
            {
                Label lbl = new Label
                {
                    Text = text,
                    Location = new Point(10, y),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10)
                };
                userPanel.Controls.Add(lbl);
                y += 30;
            }

            AddLabel($"Имя: {worker.Name}");
            AddLabel($"Возраст: {worker.Age}");
            AddLabel($"Должность: {worker.Position}");
            AddLabel($"Зарплата: {worker.Salary}");
            AddLabel($"Контакты: {worker.Contacts}");
            AddLabel($"Справка: {worker.Info}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 adm = new Form3();
            adm.Show();
            this.Hide();
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

    // Класс Worker должен совпадать с тем, что используется при сохранении в JSON
    public class Worker
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }
        public string Contacts { get; set; }
        public string Salary { get; set; }
    }
}
