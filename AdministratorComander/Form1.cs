using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace AdministratorComander
{
    public partial class Form1 : Form
    {
        private int currentY = 0; // Для позиционирования панелей
        private int currentYforevent = 0; // Для позиционирования панелей
        private Image cadr;
        private List<string> namesList = new List<string>();

        // Словарь для хранения рабочих, ключ — имя рабочего
        private Dictionary<string, Worker> workers = new Dictionary<string, Worker>();

        // Путь к файлу для сохранения данных
        private readonly string workersFilePath = Path.Combine(Application.StartupPath, "workers.json");

        public Form1()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);

            InitializeComponent();

            // Загрузка данных из файла при старте
            LoadWorkersFromFile();

            // Подписка на событие загрузки формы
            this.Load += Form1_Load;
        }

        // Класс для хранения данных о рабочем
        public class Worker
        {
            public string Name { get; set; }
            public string Age { get; set; }
            public string Position { get; set; }
            public string Info { get; set; }
            public string Contacts { get; set; }
            public string Salary { get; set; }
        }

        // Метод создания карточки рабочего
        private void Create_Card(string name, string age, string position, string info, string contacts, string salary)
        {
            namesList.Add(name);

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(namesList.ToArray());

            Panel userPanel = new Panel
            {
                Location = new Point(0, currentY),
                Size = new Size(320, 120), // увеличим высоту для зарплаты
                BorderStyle = BorderStyle.FixedSingle
            };

            Label nameLabel = new Label
            {
                Text = $"Имя: {name}",
                Location = new Point(10, 5),
                AutoSize = true
            };

            Label ageLabel = new Label
            {
                Text = $"Возраст: {age}",
                Location = new Point(10, 25),
                AutoSize = true
            };

            Label positionLabel = new Label
            {
                Text = $"Должность: {position}",
                Location = new Point(150, 5),
                AutoSize = true
            };

            Label salaryLabel = new Label
            {
                Text = $"Зарплата: {salary}",
                Location = new Point(10, 45),
                AutoSize = true
            };

            Label contactsLabel = new Label
            {
                Text = $"Контакты: {contacts}",
                Location = new Point(150, 25),
                Size = new Size(150, 40),
                AutoEllipsis = true
            };

            Label infoLabel = new Label
            {
                Text = $"Справка: {info}",
                Location = new Point(10, 80),
                Size = new Size(300, 30),
                AutoEllipsis = true
            };

            userPanel.Controls.Add(nameLabel);
            userPanel.Controls.Add(ageLabel);
            userPanel.Controls.Add(positionLabel);
            userPanel.Controls.Add(salaryLabel);
            userPanel.Controls.Add(infoLabel);
            userPanel.Controls.Add(contactsLabel);

            outputpn.Controls.Add(userPanel);

            currentY += userPanel.Height + 5;
        }

        // Обработчик загрузки формы — создаёт карточки из словаря
        private void Form1_Load(object sender, EventArgs e)
        {
            File.Delete(workersFilePath);
            currentY = 0; // сброс позиции для корректного отображения
            namesList.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (var worker in workers.Values)
            {
                comboBox2.Items.Add(worker.Name);
            }
            foreach (var worker in workers.Values)
            {
                Create_Card(worker.Name, worker.Age, worker.Position, worker.Info, worker.Contacts, worker.Salary);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (merinfotb.TextLength != 0 && mernametb.TextLength != 0 && datatb.TextLength != 0)
            {
                Panel eventx = new Panel()
                {
                    BackColor = Color.FromArgb(171, 242, 206),
                    Size = new Size(600, 400),
                    Location = new Point(93, currentYforevent),
                    BorderStyle = BorderStyle.FixedSingle
                };
                PictureBox pictureBox = new PictureBox()
                {
                    Size = new Size(600, 300),
                    Location = new Point(0, 0),
                    BackColor = Color.WhiteSmoke,
                    Image = cadr,
                };
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                cadr = null;
                Label namee = new Label()
                {
                    Text = $"Название: {mernametb.Text}",
                    Location = new Point(10, 320),
                    AutoSize = true
                };
                Label otvet = new Label()
                {
                    Text = $"Ответственное лицо: {comboBox1.SelectedItem}",
                    Location = new Point(160, 320),
                    AutoSize = true
                };
                Label Date = new Label()
                {
                    Text = $"Дата: {datatb.Text}",
                    Location = new Point(500, 320),
                    AutoSize = true
                };
                Label infor = new Label()
                {
                    Text = merinfotb.Text,
                    Location = new Point(10, 350),
                    AutoSize = true
                };
                Button deleter = new Button()
                {
                    Location = new Point(570, 295),
                    Size = new Size(20, 20),
                    BackColor = Color.FromArgb(225, 106, 106),
                    Text = "X"
                };
                deleter.Click += (s, d) =>
                {
                    eventpanel.Controls.Remove(eventx);
                    eventx.Dispose();
                    currentYforevent = currentYforevent - 405;
                };
                eventx.Controls.Add(pictureBox);
                eventx.Controls.Add(namee);
                eventx.Controls.Add(otvet);
                eventx.Controls.Add(Date);
                eventx.Controls.Add(infor);
                eventx.Controls.Add(deleter);
                eventpanel.Controls.Add(eventx);
                currentYforevent += 405;
                datatb.Text = "";
                mernametb.Text = "";
                merinfotb.Text = "";
                pb.Image = null;
                statuslb.Text = "Выполнено";
            }
            else
            {
                statuslb.Text = "Не все данные указаны";
            }
        }

        private void fotobt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Файлы изображений|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pb.Image = Image.FromFile(openFileDialog.FileName);
                        cadr = Image.FromFile(openFileDialog.FileName);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                    }
                }
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void acceptbt_Click_1(object sender, EventArgs e)
        {
            // Добавьте проверку salarytb.TextLength != 0
            if (nametb.TextLength != 0 && agetb.TextLength != 0 && worktb.TextLength != 0 && infotb.TextLength != 0 && contactstb.TextLength != 0 && salarytb.TextLength != 0)
            {
                // Создаём объект рабочего
                Worker worker = new Worker
                {
                    Name = nametb.Text,
                    Age = agetb.Text,
                    Position = worktb.Text,
                    Info = infotb.Text,
                    Contacts = contactstb.Text,
                    Salary = salarytb.Text
                };

                // Сохраняем в словарь, ключ — имя рабочего
                workers[worker.Name] = worker;

                // Создаём карточку на форме
                Create_Card(worker.Name, worker.Age, worker.Position, worker.Info, worker.Contacts, worker.Salary);

                // Сохраняем словарь в файл
                SaveWorkersToFile();

                agreelb.Text = "Добавлено";

                // Очищаем поля
                nametb.Text = "";
                agetb.Text = "";
                worktb.Text = "";
                infotb.Text = "";
                contactstb.Text = "";
                salarytb.Text = "";
            }
            else
            {
                agreelb.Text = "Указаны не все данные";
            }
        }

        private void SaveWorkersToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(workers, options);
                File.WriteAllText(workersFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
            }
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

        private void infolblb_Click(object sender, EventArgs e)
        {
            // Пустой обработчик, если нужен — можно реализовать
        }

        private void merinfotb_TextChanged(object sender, EventArgs e)
        {
            // Пустой обработчик, если нужен — можно реализовать
        }

        private void escbt_Click(object sender, EventArgs e)
        {
            Form3 fg = new Form3();
            fg.Show();
            this.Hide();
        }

        private void paybt_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите работника в списке!");
                return;
            }
            string selectedName = comboBox2.SelectedItem.ToString();
            if (!workers.ContainsKey(selectedName))
            {
                MessageBox.Show("Работник не найден!");
                return;
            }
            if (string.IsNullOrWhiteSpace(paytb.Text))
            {
                MessageBox.Show("Введите новую зарплату!");
                return;
            }

            // Меняем зарплату
            workers[selectedName].Salary = paytb.Text;

            // Сохраняем в файл
            SaveWorkersToFile();

            MessageBox.Show($"Зарплата работника {selectedName} изменена!");

            // Обновляем карточки на форме
            outputpn.Controls.Clear();
            currentY = 0;
            namesList.Clear();
            comboBox1.Items.Clear();
            foreach (var worker in workers.Values)
            {
                Create_Card(worker.Name, worker.Age, worker.Position, worker.Info, worker.Contacts, worker.Salary);
            }
        }
    }
}
